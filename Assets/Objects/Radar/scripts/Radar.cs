using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class Radar : MonoBehaviour
{
    GameObject RadarMap;
    GameObject map_point_player;
    GameObject map_point_player_orient;
    GameObject map_point_current_mission;
    GameObject Player;

    GameObject GameManager;
    GameManager script_GameManager;

    float Player_x;
    float Player_y;
    float Player_z;

    int current_mission;
    float current_mission_x;
    float current_mission_y;
    float current_mission_z;
    GameObject current_mission_base;


    public GameObject Canvas;
    public Button button_radar_1;
    public Button button_radar_2;

    public float speed_rot = 45f;

    bool map_incline = false;

    public GameObject map_point_mission_battery;
    [SerializeField] AudioSource ping;

    // Orient system
    public GameObject vector_target; // выравнивается к цели на карте
    public GameObject vector_target_child; // выравнивается к глобусу
    public GameObject target_current_misson;
    public GameObject target_battery_mission;
    GameObject CameraPlayer;
    bool Locked_Orient_system = false;


    void Start()
    {
        map_point_mission_battery.SetActive(false);
        Player = GameObject.FindWithTag("Player");
        CameraPlayer = GameObject.FindWithTag("MainCamera");
        RadarMap = transform.GetChild(0).gameObject;
        map_point_player = RadarMap.transform.GetChild(0).gameObject;
        map_point_player_orient = map_point_player.transform.GetChild(0).gameObject;
        map_point_current_mission = RadarMap.transform.GetChild(1).gameObject;
        target_current_misson = map_point_current_mission.transform.GetChild(0).gameObject;
        RadarMap.SetActive(false);

        GameManager = GameObject.Find("/GameManager");
        script_GameManager = GameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (RadarMap.activeSelf)
        {

            FindPlayer();
            RadarMap.transform.Rotate(0.0f, speed_rot * Time.deltaTime, 0.0f, Space.Self);

            if (map_incline)
            {
                RadarMap.transform.Rotate(60f * Time.deltaTime, 0.0f, 0.0f, Space.World);
            }
            if (Locked_Orient_system)
            {
                Orient_rotate_to_camera();
                Orient_CopyRot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ping.Play(); // звук
            RadarMap.SetActive(true);
            Canvas.SetActive(true);
            speed_rot = 45f;
            script_GameManager.Dialog_current_mission.SetActive(true);
            map_point_current_mission.SetActive(true);
            current_mission_activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Locked_Orient_system = false; // отключаю систему наведения на камеру цели
            RadarMap.SetActive(false);
            Canvas.SetActive(false);
            script_GameManager.Dialog_current_mission.SetActive(false);

        }
    }

    void FindPlayer()
    {
        Player_x = Player.transform.localEulerAngles.x;
        Player_y = Player.transform.localEulerAngles.y;
        Player_z = Player.transform.localEulerAngles.z;

        Vector3 newRotation = new Vector3(Player_x, Player_y, Player_z);
        map_point_player.transform.localEulerAngles = newRotation;

        map_point_player_orient.transform.rotation = Quaternion.identity;
        Quaternion Player_orient = Player.transform.rotation; // Получить кватернион ориентации исходного объекта
        map_point_player_orient.transform.rotation = Player_orient; // Применить кватернион к целевому объекту
    }

    // показывает на радаре текущую миссию
    void current_mission_activate()
    {
        current_mission = ProgressManager.Instance.YandexDataOBJ.GameState;

        string base_name_SpacePod = "pivot (SpacePod)";
        string base_name_shuttle = "space_shuttle_POINT";
        string base_name_Fuel = "BASES/Fuel_Base";
        string base_name_Energy = "BASES/Energy_Base";
        string base_name_Military_Base = "BASES/Military_Base";

        string base_name_crater = "crater_POINT (1)";
        string base_name_Weapons_Base = "BASES/Weapons_Base";
        string base_name_Engine_base = "BASES/Engine_base";
        string base_name_Сabernitic_lab = "BASES/Сabernitic_lab";
        string current_mission_base_name = "pivot (SpacePod)";

        bool[] bag_plaer = new bool[3];
        bag_plaer = script_GameManager.BAG_Player_curent();
        bool bag_key = bag_plaer[0];
        bool bag_fuel = bag_plaer[1];
        bool bag_energy = bag_plaer[2];

        if (current_mission == 0 && current_mission == 1) current_mission_base_name = base_name_SpacePod;
        if (current_mission == 2) current_mission_base_name = base_name_crater;
        if (current_mission == 3) current_mission_base_name = base_name_Weapons_Base;
        if (current_mission == 4) current_mission_base_name = base_name_shuttle;
        if (current_mission == 5) current_mission_base_name = base_name_Engine_base;
        if (current_mission == 6 && !bag_fuel) current_mission_base_name = base_name_Fuel;
        if (current_mission == 6 && bag_fuel) current_mission_base_name = base_name_shuttle;
        if (current_mission == 7) current_mission_base_name = base_name_Сabernitic_lab;
        if (current_mission == 8 && !bag_energy) current_mission_base_name = base_name_Energy;
        if (current_mission == 8 && bag_energy) current_mission_base_name = base_name_shuttle;

        if (current_mission == 9)
        {
            map_point_current_mission.SetActive(false);
            map_point_mission_battery.SetActive(true);
        }
        if (current_mission == 10)
        {
            Debug.Log("----!!!!!!!!!!!!--------m->" + base_name_Military_Base);
            map_point_current_mission.SetActive(true);
            map_point_mission_battery.SetActive(false);
            if (!bag_key) { current_mission_base_name = base_name_Military_Base; }
            else
            {
                current_mission_base_name = base_name_shuttle;
            }
        }
        if (current_mission == 11) map_point_current_mission.SetActive(false); // цели больше нет

        current_mission_base = GameObject.Find("/" + current_mission_base_name); // текущая база помиссии
        Debug.Log("-----> " + current_mission_base.name);

        current_mission_x = current_mission_base.transform.localEulerAngles.x;
        current_mission_y = current_mission_base.transform.localEulerAngles.y;
        current_mission_z = current_mission_base.transform.localEulerAngles.z;
        Vector3 newRotation = new Vector3(current_mission_x, current_mission_y, current_mission_z);
        map_point_current_mission.transform.localEulerAngles = newRotation;
    }
    public void map_pause()
    {
        if (speed_rot != 0)
        {
            speed_rot = 0f;
        }
        else
        {
            Locked_Orient_system = false; // отключаем наведение
            speed_rot = 45f;
        }
    }
    public void map_incline_on()
    {
        Locked_Orient_system = false;
        map_incline = true;
    }
    public void map_incline_off()
    {
        map_incline = false;
    }

    /////////////////// Система наведения на цель ///////////////////
    // останавливает глобус
    public void Orient_stop_map()
    {
        speed_rot = 0f;
        Locked_Orient_system = false;
    }
    // выравнивает вектор_к_цели к цели (игрок) (глобус не крутится)
    public void Orient_vector_target()
    {
        if (!Locked_Orient_system)
        {
            Vector3 direction = map_point_player_orient.transform.position - vector_target.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            vector_target.transform.rotation = targetRotation;
        }
    }
    // выравнивает вектор_к_цели к миссии (глобус не крутится)
    // миссия 9 (реакторы) - особый случай
    public void Orient_vector_target_mission()
    {
        if (!Locked_Orient_system)
        {
            if (current_mission != 9)
            {
                Vector3 direction = target_current_misson.transform.position - vector_target.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                vector_target.transform.rotation = targetRotation;
            }
            else
            {
                Vector3 direction = target_battery_mission.transform.position - vector_target.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                vector_target.transform.rotation = targetRotation;
            }
        }
    }
    // выравнивает дочерний вектор по отношению к глобусу
    public void Orien_child_vector()
    {
        if (!Locked_Orient_system)
        {
            vector_target_child.transform.rotation = vector_target.transform.rotation;
            Quaternion target_to_radarMap = RadarMap.transform.rotation * Quaternion.Inverse(vector_target.transform.rotation);
            vector_target_child.transform.rotation = target_to_radarMap * vector_target_child.transform.rotation;
            Locked_Orient_system = true;
        }
    }
    // Переносит ориентацию на глобус
    void Orient_CopyRot()
    {
        RadarMap.transform.rotation = vector_target_child.transform.rotation;
    }
    // наводит вектор_к_цели на камеру 
    void Orient_rotate_to_camera()
    {
        float navigation_speed = 0.5f;
        Vector3 direction = CameraPlayer.transform.position - vector_target.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        vector_target.transform.rotation = Quaternion.Slerp(vector_target.transform.rotation, targetRotation, Time.deltaTime * navigation_speed);
    }
    public void Orient_Where_I_am()
    {
        Orient_stop_map();
        Orient_vector_target();
        Orien_child_vector();
    }
    public void Orient_Where_is_the_target()
    {
        Orient_stop_map();
        Orient_vector_target_mission();
        Orien_child_vector();
    }
    /////////////////////////////////////////////////////////
}
