using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// ��������� ������, ������� �����������.
public class robot_scout : MonoBehaviour {
    [SerializeField] TextMeshProUGUI robot_scout_canvas_text;
    [SerializeField] GameObject robot_scout_canvas;
    [SerializeField] GameObject Explosion;// �����
    GameObject Explosion_CLONE;// ����� ��� ������
    int robot_scout_health = 100; // ��������
    GameObject ground; // ����
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

    bool robot_destroy = false; // ����� ���������

    // ��������
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject Bullet; //������ ����
    GameObject cloneBullet; // ���� ����
    float TimeFire  = 0.3f; // ����� ����� ����������
    float TimeFireTemp;

    GameObject RADIUS_robot_scout;

    // Start is called before the first frame update
    void Start() {
        RADIUS_robot_scout = transform.parent.gameObject;
        rigid = transform.GetComponent<Rigidbody>();
        ground = GameObject.Find("/ground");
        myCamera = GameObject.FindGameObjectWithTag("MainCamera"); // ������ �� ������
        target_ground = ground.GetComponent<Transform>();
        robot_scout_canvas_text.text = robot_scout_health.ToString(); // ���������� ��������
        robot_scout_canvas_text.enabled = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        script_player = Player.GetComponent<player>();
        Explosion.SetActive(false); // ����� ���� �� �����
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
            rigid.AddForce(transform.forward * speed_walk); // �������� �����
        }
    }

    // ��������
    void Fire() {
        TimeFireTemp = TimeFireTemp - Time.deltaTime;
        if (TimeFireTemp <= 0) {
            GenerateBullet();
            TimeFireTemp = TimeFire;
        }
    }

    // ��������� ����
    void GenerateBullet() {
        cloneBullet = Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
        Destroy(cloneBullet, 10f); // ����������� ����� 10 ���
    }

    void Gravity() {
        // ������������ ������ � ������ �������.
        Vector3 pos = (target_ground.position - transform.position).normalized; // ���������� ������ ����������� � ������� �� ������
        Quaternion rot = Quaternion.FromToRotation(-transform.up, pos);  // ����������, �������������� ������ �� ������� � ������ �������
        transform.rotation = rot * transform.rotation;                   // ��������� ���������� � ������

        rigid.AddForce(pos * gravity);                                     // ���������� ��� ������
    }

    // � ���� ������ ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            player_collider = other.gameObject; // ������ �� ������
            rot_robot_scout = true;
            Debug.Log(debug_obj_name + "����� ����");
            robot_scout_canvas_text.enabled = true;

            // ��������� ������ ������ (���)
            animatorPlayer.SetBool("Attack_mode", true);
            script_player.PlayerModeAttack = true;
        }
    }

    // �� ���� ������ ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            // �������������� �������� ��������� ����� ������� � �������.
            // ������-�� ���� ������ ������������ ��� �� ������������
            Vector3 playerPosition = Player.transform.position;
            Vector3 robotPosition = transform.position;
            float distance = Vector3.Distance(playerPosition, robotPosition);
            if (distance >= 5f) {
                Debug.Log(debug_obj_name + "������� ����");
                rot_robot_scout = false;
                robot_scout_canvas_text.enabled = false;

                // ����������� ������ ������ (���)
                animatorPlayer.SetBool("Attack_mode", false);
                script_player.PlayerModeAttack = false;
            }
        }
    }
    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && (robot_destroy == false)) {
            distance_player = Vector3.Distance(other.transform.position, transform.position); // ��������� ���������� �� ������
                                                                                              // rot_robot_scout = true;

            //Debug.Log(debug_obj_name + "�������� ����, �� ������: " + distance_player.ToString());

            // ��������� ������ ������ (���)
            animatorPlayer.SetBool("Attack_mode", true);
            script_player.PlayerModeAttack = true;
        }
    }
    // ������� ������ � ������
    void Direction_robot_scout() {
        Vector3 pos = (player_collider.transform.position - transform.position).normalized; // ���������� ������ ����������� � ������
        Quaternion rot = Quaternion.FromToRotation(transform.forward, pos);  // ����������, �������������� ������ �� ������� �������
        Quaternion new_rot = rot * transform.rotation;                   // ����� ���������� �������� ������ � ������
        transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, speed_rot_robot_scout * Time.deltaTime); // ������ ������������
    }

    // ����������� ����� ������ �� ������
    private void CanvasTurretLookAt() {
        robot_scout_canvas.transform.LookAt(myCamera.transform);
        float y = robot_scout_canvas.transform.localEulerAngles.y;
        robot_scout_canvas.transform.localEulerAngles = new Vector3(0, y, 0);
    }

    // ���� ������
    public void Damage() {
        robot_scout_health -= 10;
        robot_scout_canvas_text.text = robot_scout_health.ToString(); // ���������� ��������
        if (robot_scout_health <= 0) {            
            Dead();
        }
    }

    // ����� ���������
    public void Dead() {
        robot_destroy = true;
        // ����������� ������ ������ (���)
        animatorPlayer.SetBool("Attack_mode", false);
        script_player.PlayerModeAttack = false;

        // ��������� ������
        Explosion_CLONE = Instantiate(Explosion, transform.position, transform.rotation);
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // ����������� ����� 2 ���
        Destroy(RADIUS_robot_scout);
    }
}