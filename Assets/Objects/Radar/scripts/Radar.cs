using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start() {
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
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            RadarMap.SetActive(true);
            // FindPlayer();
            script_GameManager.Dialog_current_mission.SetActive(true);
            map_point_current_mission.SetActive(true);
            current_mission_activate();
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            RadarMap.SetActive(false);
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

        string base_name_1 = "pivot (SpacePod)";
        string base_name_2 = "space_shuttle_POINT";
        string base_name_3 = "BASES/Fuel_Base";
        string base_name_4 = "BASES/Energy_Base";
        string base_name_5 = "Trigger_Terminal_POINT (key)";
        string current_mission_base_name = "pivot (SpacePod)";

        if (current_mission == 0 && current_mission == 1) current_mission_base_name = base_name_1;
        if (current_mission == 2) current_mission_base_name = base_name_2;
        if (current_mission == 3) current_mission_base_name = base_name_3;
        if (current_mission == 4) current_mission_base_name = base_name_4;
        if (current_mission == 5) current_mission_base_name = base_name_5;
        if (current_mission == 6) map_point_current_mission.SetActive(false); // цели больше нет

        current_mission_base = GameObject.Find("/" + current_mission_base_name); // текущая база помиссии
        Debug.Log("-----> " + current_mission_base.name);

        current_mission_x = current_mission_base.transform.localEulerAngles.x;
        current_mission_y = current_mission_base.transform.localEulerAngles.y;
        current_mission_z = current_mission_base.transform.localEulerAngles.z;
        Vector3 newRotation = new Vector3(current_mission_x, current_mission_y, current_mission_z);
        map_point_current_mission.transform.localEulerAngles = newRotation;
    }
}
