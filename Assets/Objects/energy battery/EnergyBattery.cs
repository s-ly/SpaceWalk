using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBattery : MonoBehaviour {
    public GameObject Base_On;
    public GameObject Base_Off;
    public TextMeshProUGUI text;
    public GameObject canvas;

    GameObject Player;
    player SCRIPT_player;
    Animator ANIMATOR_player;

    int health = 200; // здоровье
    bool destroy = false;

    public GameObject Explosion;// взрыв
    GameObject Explosion_CLONE;// взрыв для робота

    // Start is called before the first frame update
    void Start() {
        Base_Off.SetActive(false);
        canvas.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        SCRIPT_player = Player.GetComponent<player>();
        ANIMATOR_player = Player.GetComponent<Animator>();
        text.text = health.ToString(); // показываем здоровье
    }

    // Update is called once per frame
    void Update() {

    }

    // в зону турели что-то входит
    private void OnTriggerEnter(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy) {
            canvas.SetActive(true);
            // активация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    // из зоны турели что-то вышло
    private void OnTriggerExit(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy) {
            // дополнительная проверка растояния между игроком и роботом.
            // потому-то были ложные срабатывания при их столкновении
            //Vector3 playerPosition = Player.transform.position;
            //Vector3 robotPosition = transform.position;
            //float distance = Vector3.Distance(playerPosition, robotPosition);
            //if (distance >= 5f) {
            //    Debug.Log(debug_obj_name + "Потерял цель");
            //    rot_robot_scout = false;
            //    robot_scout_canvas_text.enabled = false;

            //    // ДЕактивация режима игрока (бой)
            //    animatorPlayer.SetBool("Attack_mode", false);
            //    SCRIPT_player.PlayerModeAttack = false;
            //}

            canvas.SetActive(false);
            // ДЕактивация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", false);
            SCRIPT_player.PlayerModeAttack = false;
        }
    }

    // в зоне что-то находится
    private void OnTriggerStay(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy) {

            //distance_player = Vector3.Distance(other.transform.position, transform.position); // определяю расстояние до игрока
            // rot_robot_scout = true;

            //Debug.Log(debug_obj_name + "Наблюдаю цель, до игрока: " + distance_player.ToString());

            canvas.SetActive(true);
            // активация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    void Destroy() {
        destroy = true;

        // ДЕактивация режима игрока (бой)
        ANIMATOR_player.SetBool("Attack_mode", false);
        SCRIPT_player.PlayerModeAttack = false;

        // экземпляр взрыва
        Transform child_base;
        child_base = transform.GetChild(0);
        Explosion_CLONE = Instantiate(Explosion, child_base.position, child_base.rotation);
        Explosion_CLONE.transform.localScale = Vector3.one * 2f;
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // уничтожение через 2 сек
        Base_On.SetActive(false);
        Base_Off.SetActive(true);
        canvas.SetActive(false);
    }

    public void Damage() {
        health -= 10;
        text.text = health.ToString(); // показываем здоровье
        if (health <= 0) Destroy();
    }
}
