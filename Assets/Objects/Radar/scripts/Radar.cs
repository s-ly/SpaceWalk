using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.XR;

public class Radar : MonoBehaviour {
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



    void Start() {
        map_point_mission_battery.SetActive(false);
        Player = GameObject.FindWithTag("Player");
        RadarMap = transform.GetChild(0).gameObject;
        map_point_player = RadarMap.transform.GetChild(0).gameObject;
        map_point_player_orient = map_point_player.transform.GetChild(0).gameObject;
        map_point_current_mission = RadarMap.transform.GetChild(1).gameObject;
        RadarMap.SetActive(false);

        GameManager = GameObject.Find("/GameManager");
        script_GameManager = GameManager.GetComponent<GameManager>();
    }

    void Update() {
        if (RadarMap.activeSelf) {
            FindPlayer();
            RadarMap.transform.Rotate(0.0f, speed_rot * Time.deltaTime, 0.0f, Space.Self);

            if (map_incline) {
                //RadarMap.transform.Rotate(60f * Time.deltaTime, 0.0f, 0.0f, Space.Self);
                RadarMap.transform.Rotate(60f * Time.deltaTime, 0.0f, 0.0f, Space.World);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            RadarMap.SetActive(true);
            Canvas.SetActive(true);
            speed_rot = 45f;
            script_GameManager.Dialog_current_mission.SetActive(true);
            map_point_current_mission.SetActive(true);
            current_mission_activate();
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            RadarMap.SetActive(false);
            Canvas.SetActive(false);
            script_GameManager.Dialog_current_mission.SetActive(false);
        }
    }

    void FindPlayer() {
        Player_x = Player.transform.localEulerAngles.x;
        Player_y = Player.transform.localEulerAngles.y;
        Player_z = Player.transform.localEulerAngles.z;

        Vector3 newRotation = new Vector3(Player_x, Player_y, Player_z);
        map_point_player.transform.localEulerAngles = newRotation;

        map_point_player_orient.transform.rotation = Quaternion.identity;
        Quaternion Player_orient = Player.transform.rotation; // Получить кватернион ориентации исходного объекта
        map_point_player_orient.transform.rotation = Player_orient; // Применить кватернион к целевому объекту

        //float orient_y = map_point_player_orient.transform.localEulerAngles.y;
        //Vector3 newRotation_orient_player = new Vector3(0f, orient_y, 0f);
        //map_point_player_orient.transform.localEulerAngles = newRotation_orient_player;


        // Применить кватернион к целевому объекту
        //map_point_player_orient.transform.rotation = Quaternion.Euler(0, Player_orient.eulerAngles.y, 0); 


    }

    // показывает на радаре текущую миссию
    void current_mission_activate() {
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
        

        if (current_mission == 0 && current_mission == 1) current_mission_base_name = base_name_SpacePod;
        if (current_mission == 2) current_mission_base_name = base_name_crater;        
        if (current_mission == 3) current_mission_base_name = base_name_Weapons_Base;
        if (current_mission == 4) current_mission_base_name = base_name_shuttle;
        if (current_mission == 5) current_mission_base_name = base_name_Engine_base;
        if (current_mission == 6) current_mission_base_name = base_name_Fuel;
        if (current_mission == 7) current_mission_base_name = base_name_Сabernitic_lab;
        if (current_mission == 8) current_mission_base_name = base_name_Energy;

        if (current_mission == 9) {
            map_point_current_mission.SetActive(false);
            map_point_mission_battery.SetActive(true);
        }
        if (current_mission == 10) {
            Debug.Log("----!!!!!!!!!!!!--------m->" + base_name_Military_Base);
            map_point_current_mission.SetActive(true);
            map_point_mission_battery.SetActive(false);
            current_mission_base_name = base_name_Military_Base;
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
    public void map_pause() {
        if (speed_rot != 0) {
            speed_rot = 0f;
        }
        else { speed_rot = 45f; }
    }
    public void map_incline_on() {
        map_incline = true;
    }
    public void map_incline_off() {
        map_incline = false;
    }
}
