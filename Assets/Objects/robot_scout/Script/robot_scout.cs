using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// ”правл€ет врагом, роботом разведчиком.
public class robot_scout : MonoBehaviour {
    [SerializeField] TextMeshProUGUI robot_scout_canvas_text;
    [SerializeField] GameObject robot_scout_canvas;
    [SerializeField] GameObject Explosion;// взрыв
    GameObject Explosion_CLONE;// взрыв дл€ робота
    int robot_scout_health = 100; // здоровье
    GameObject ground; // Ћуна
    Transform target_ground;
    Rigidbody rigid;
    float gravity = 2.0f;
    string debug_obj_name = "bot_scout: ";
    float distance_player;
    GameObject player_collider;
    float speed_rot_robot_scout = 1.5f;
    float speed_walk = 10f;
    public bool rot_robot_scout = false;
    GameObject myCamera;

    //Player 
    private GameObject Player;
    private player script_player;
    [SerializeField] private Animator animatorPlayer;

    bool robot_destroy = false; // робот уничтожен

    // стрельба
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject Bullet; //префаб пули
    GameObject cloneBullet; // клон пули
    float TimeFire  = 0.3f; // врем€ между выстрелами
    float TimeFireTemp;

    GameObject RADIUS_robot_scout;

    // Start is called before the first frame update
    void Start() {
        RADIUS_robot_scout = transform.parent.gameObject;
        rigid = transform.GetComponent<Rigidbody>();
        ground = GameObject.Find("/ground");
        myCamera = GameObject.FindGameObjectWithTag("MainCamera"); // ссылка на камеру
        target_ground = ground.GetComponent<Transform>();
        robot_scout_canvas_text.text = robot_scout_health.ToString(); // показываем здоровье
        robot_scout_canvas_text.enabled = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        script_player = Player.GetComponent<player>();
        Explosion.SetActive(false); // взрыв пока не нужен
        TimeFireTemp = TimeFire;

    }

    // Update is called once per frame
    void Update() {
        if (rot_robot_scout) {
            Direction_robot_scout();
            CanvasTurretLookAt();
            Fire();
        }
    }

    void FixedUpdate() {
        Gravity();
        if (rot_robot_scout) {
            rigid.AddForce(transform.forward * speed_walk); // движение вперЄд
        }
    }

    // выстрелы
    void Fire() {
        TimeFireTemp = TimeFireTemp - Time.deltaTime;
        if (TimeFireTemp <= 0) {
            GenerateBullet();
            TimeFireTemp = TimeFire;
        }
    }

    // генераци€ пули
    void GenerateBullet() {
        cloneBullet = Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        Destroy(cloneBullet, 10f); // уничтожение через 10 сек
    }

    void Gravity() {
        // ¬ыравнивание игрока к центру планеты.
        Vector3 pos = (target_ground.position - transform.position).normalized; // нормальный вектор направление к планете от игрока
        Quaternion rot = Quaternion.FromToRotation(-transform.up, pos);  // кватернион, поворачивающий игрока по вектору к центру планеты
        transform.rotation = rot * transform.rotation;                   // ѕримен€ем кватернион к игроку

        rigid.AddForce(pos * gravity);                                     // гравитаци€ дл€ игрока
    }

    // в зону турели что-то входит
    private void OnTriggerEnter(Collider other) {
        // ¬ зону вошЄл игрок 
        if (other.gameObject.CompareTag("Player")) {
            player_collider = other.gameObject; // ссылка на игрока
            rot_robot_scout = true;
            Debug.Log(debug_obj_name + "ЌашЄл цель");
            robot_scout_canvas_text.enabled = true;

            // активаци€ режима игрока (бой)
            animatorPlayer.SetBool("Attack_mode", true);
            script_player.PlayerModeAttack = true;
        }
    }

    // из зоны турели что-то вышло
    private void OnTriggerExit(Collider other) {
        // ¬ зону вошЄл игрок 
        if (other.gameObject.CompareTag("Player")) {
            // дополнительна€ проверка расто€ни€ между игроком и роботом.
            // потому-то были ложные срабатывани€ при их столкновении
            Vector3 playerPosition = Player.transform.position;
            Vector3 robotPosition = transform.position;
            float distance = Vector3.Distance(playerPosition, robotPosition);
            if (distance >= 5f) {
                Debug.Log(debug_obj_name + "ѕотер€л цель");
                rot_robot_scout = false;
                robot_scout_canvas_text.enabled = false;

                // ƒ≈активаци€ режима игрока (бой)
                animatorPlayer.SetBool("Attack_mode", false);
                script_player.PlayerModeAttack = false;
            }
        }
    }
    // в зоне что-то находитс€
    private void OnTriggerStay(Collider other) {
        // ¬ зону вошЄл игрок 
        if (other.gameObject.CompareTag("Player") && (robot_destroy == false)) {
            distance_player = Vector3.Distance(other.transform.position, transform.position); // определ€ю рассто€ние до игрока
                                                                                              // rot_robot_scout = true;

            //Debug.Log(debug_obj_name + "Ќаблюдаю цель, до игрока: " + distance_player.ToString());

            // активаци€ режима игрока (бой)
            animatorPlayer.SetBool("Attack_mode", true);
            script_player.PlayerModeAttack = true;
        }
    }
    // поворот робота к игроку
    void Direction_robot_scout() {
        Vector3 pos = (player_collider.transform.position - transform.position).normalized; // нормальный вектор направление к игроку
        Quaternion rot = Quaternion.FromToRotation(transform.forward, pos);  // кватернион, поворачивающий робота по вектору кигроку
        Quaternion new_rot = rot * transform.rotation;                   // новый кватернион поворота робота к игроку
        transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, speed_rot_robot_scout * Time.deltaTime); // плавно поворачиваем
    }

    // выравнивает холст робота по камере
    private void CanvasTurretLookAt() {
        robot_scout_canvas.transform.LookAt(myCamera.transform);
        float y = robot_scout_canvas.transform.localEulerAngles.y;
        robot_scout_canvas.transform.localEulerAngles = new Vector3(0, y, 0);
    }

    // урон робота
    public void Damage() {
        robot_scout_health -= 10;
        robot_scout_canvas_text.text = robot_scout_health.ToString(); // показываем здоровье
        if (robot_scout_health <= 0) {            
            Dead();
        }
    }

    // робот иничтожен
    public void Dead() {
        robot_destroy = true;
        // ƒ≈активаци€ режима игрока (бой)
        animatorPlayer.SetBool("Attack_mode", false);
        script_player.PlayerModeAttack = false;

        // экземпл€р взрыва
        Explosion_CLONE = Instantiate(Explosion, transform.position, transform.rotation);
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // уничтожение через 2 сек
        Destroy(RADIUS_robot_scout);
    }
}