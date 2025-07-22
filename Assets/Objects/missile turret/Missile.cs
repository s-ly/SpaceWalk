using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Рокета
public class Missile : MonoBehaviour {
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // взрыв пули
    private GameObject clone_Explosion_Bullet; // клон взрыва пули

    public float rotationSpeed = 90f;  // Скорость поворота в градусах в секунду

    // Для нацеливания на игрока
    GameObject player;
    GameObject playerMesh;
    Transform platerTransform;

    // Start is called before the first frame update
    void Start() {
        // player = GameObject.FindGameObjectWithTag("Player");
        // playerMesh = player.transform.Find("astro_035").gameObject;
        // platerTransform = playerMesh.transform;

        player = GameObject.FindGameObjectWithTag("PlayerHead"); // целимся в голову игрока
        // playerMesh = player.transform.Find("astro_035").gameObject;
        platerTransform = player.transform;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime, Space.Self);

        /////////////// Нацеливание на игрока
        // Текущая и целевая ротации
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(platerTransform.position - transform.position);

        // Плавный поворот с постоянной скоростью
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        /////////////////////////////////////

        //transform.Translate(Vector3.forward * inputZoom, Space.Self);
        //transform.position += new Vector3(0, 0, 0);
    }

    // Пуля куда-то попала
    private void OnTriggerEnter(Collider other) {

        // Пуля попала в игрока
        if (other.gameObject.CompareTag("Player")) {
            // в скрипте healthManager вызываем метод урона
            FindObjectOfType<healthManager>().Damage();

            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            clone_Explosion_Bullet.transform.localScale *= 1.8f;
            Destroy(clone_Explosion_Bullet, 1.0f); // уничтожение через 2 сек

            Destroy(gameObject);
        }

        // Пуля попала в землю
        if (other.gameObject.CompareTag("Ground")) {
            Debug.Log("В землю!");

            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            clone_Explosion_Bullet.transform.localScale *= 1.8f;
            Destroy(clone_Explosion_Bullet, 1.0f); // уничтожение через 2 сек

            Destroy(gameObject);
        }
    }
}
