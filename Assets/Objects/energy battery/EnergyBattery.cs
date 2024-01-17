using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBattery : MonoBehaviour {
    public int num_battery = 0; // порядковый номер (1-3)
    public GameManager SRC_GameManager;

    public GameObject Base_On;
    public GameObject Base_Off;
    public TextMeshProUGUI text;
    public GameObject canvas;

    GameObject Player;
    player SCRIPT_player;
    Animator ANIMATOR_player;

    int health = 200; // здоровье
    public bool destroy = false;
    public bool active = false;

    public GameObject Explosion;// взрыв
    GameObject Explosion_CLONE;// взрыв для робота

    bool DamageOn = false; // получает ли урон

    // Start is called before the first frame update
    void Start() {
        canvas.SetActive(false);
        Base_Off.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        SCRIPT_player = Player.GetComponent<player>();
        ANIMATOR_player = Player.GetComponent<Animator>();
        text.text = health.ToString(); // показываем здоровье

        if (ProgressManager.Instance.YandexDataOBJ.GameState == 9) {
            ActivationBattery();
        }

        if (ProgressManager.Instance.YandexDataOBJ.GameState >= 10) {
            destroy = true;
            active = false;
            Base_Off.SetActive(true);
            Base_On.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void ActivationBattery() {
        Debug.Log("АКТИВАЦИЯ БАТАРЕИ" + num_battery);
        active = true;

    }

    // в зону турели что-то входит
    private void OnTriggerEnter(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(true);
            DamageOn = true;
            // активация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    // из зоны турели что-то вышло
    private void OnTriggerExit(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(false);
            DamageOn = false;
            // ДЕактивация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", false);
            SCRIPT_player.PlayerModeAttack = false;
        }
    }

    // в зоне что-то находится
    private void OnTriggerStay(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(true);
            DamageOn = true;
            // активация режима игрока (бой)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    void Destroy() {
        destroy = true;
        active = false;

        // ДЕактивация режима игрока (бой)
        ANIMATOR_player.SetBool("Attack_mode", false);
        SCRIPT_player.PlayerModeAttack = false;

        // экземпляр взрыва
        Transform child_base;
        child_base = transform.GetChild(0);
        Explosion_CLONE = Instantiate(Explosion, child_base.position, child_base.rotation);   
        Explosion_CLONE.transform.localScale = Vector3.one * 7f;
        Explosion_CLONE.transform.localPosition += Vector3.up * 1f;
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // уничтожение через 2 сек
        Base_On.SetActive(false);
        Base_Off.SetActive(true);
        canvas.SetActive(false);

        SRC_GameManager.CheckDestroyBatteryNum(num_battery);
        SRC_GameManager.Check_GameState("Battery"); // Проверка состояния игры
    }

    public void Damage() {
        if (active && DamageOn) {
            health -= 10;
            text.text = health.ToString(); // показываем здоровье
            if (health <= 0) Destroy();
        }
    }
}
