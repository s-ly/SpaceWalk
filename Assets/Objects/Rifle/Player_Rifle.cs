// Управляет выстрелами игрока

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rifle : MonoBehaviour {
    [SerializeField] private GameObject playerBullet; //префаб пули игрока

    // Пауза выстрела (время между выстрелами винтовки игрока).
    private float time_shot_pause; // Получим из DATA хранилища
    private float TEMP_time_shot_pause; // Для уменьшения в счётчике

    private GameObject clonePlayerBullet; // объект пули игрока
    private bool TimeFireFlag = true;

    // Враг в прицеле.
    // У игрока есть тригерный колаидр, если в него попадает враг,
    // то можно стрелять автоматически
    private bool lookOnEnemy = false;

    // Start is called before the first frame update
    void Start() {
        UpdateTimeFireTemp();
    }

    // Update is called once per frame
    void Update() {
        FirePeriodPause();
    }

    public void UpdateTimeFireTemp() {
        time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;
        TEMP_time_shot_pause = time_shot_pause;
    }

    // в зону что-то вошло
    private void OnTriggerEnter(Collider other) {
        // В прицеле игрока появился враг
        if (other.gameObject.CompareTag("Enemy") || 
            other.gameObject.CompareTag("Enemy_2") ||
            other.gameObject.CompareTag("Enemy_3") || 
            other.gameObject.CompareTag("Enemy_battery")) {
            lookOnEnemy = true; // враг в прицеле появился
            Debug.Log("Вижу врага");
        }
    }

    // в зоне что-то находится
    private void OnTriggerStay(Collider other) {
        // В прицеле игрока находится враг
        if (other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Enemy_2") ||
            other.gameObject.CompareTag("Enemy_3") ||
            other.gameObject.CompareTag("Enemy_battery")) {
            lookOnEnemy = true; // враг в прицеле
        }
    }

    // из зоны что-то вышло
    private void OnTriggerExit(Collider other) {
        // Из прицела игрока вышел враг
        if (other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Enemy_2") ||
            other.gameObject.CompareTag("Enemy_3") ||
            other.gameObject.CompareTag("Enemy_battery")) {
            lookOnEnemy = false; // враг вышел из прицела
            Debug.Log("ВРАГ ПОБЕЖДЁН!!!!!!");
        }
    }

    /* Таймер перезарядки. Запускаем каждый кадр.
    Как только время истекло, то флаг TimeFireFlag = true.
    Временный (убывающий) счётчик сбрасываем. */
    void FirePeriodPause() {
        if (TimeFireFlag == false) {
            TEMP_time_shot_pause -= Time.deltaTime;
            if (TEMP_time_shot_pause <= 0) {
                TimeFireFlag = true;
                TEMP_time_shot_pause = time_shot_pause;
            }
        }
    }

    // стреляет если оружее как-бы перезарядилось
    // и в прицеле есть враг
    public void FirePlayer() {
        if (TimeFireFlag && lookOnEnemy) {
            GenerateBulletPlayer();
            TimeFireFlag = false;
        }
    }

    // генерация пули
    public void GenerateBulletPlayer() {
        clonePlayerBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        Destroy(clonePlayerBullet, 10f); // уничтожение через 10 сек

    }
}
