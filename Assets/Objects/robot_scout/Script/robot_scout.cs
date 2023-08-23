using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// ��������� ������, ������� �����������.
public class robot_scout : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI robot_scout_canvas_text;
    [SerializeField] GameObject robot_scout_canvas;
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
    bool rot_robot_scout = false;
    GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        ground = GameObject.Find("/ground");
        myCamera = GameObject.FindGameObjectWithTag("MainCamera"); // ������ �� ������
        target_ground = ground.GetComponent<Transform>();
        robot_scout_canvas_text.text = robot_scout_health.ToString(); // ���������� ��������
        robot_scout_canvas_text.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (rot_robot_scout)
        {
            Direction_robot_scout();
            CanvasTurretLookAt();
        }
    }

    void FixedUpdate()
    {
        Gravity();
        if (rot_robot_scout)
        {
            rigid.AddForce(transform.forward * speed_walk); // �������� �����
        }
    }

    void Gravity()
    {
        // ������������ ������ � ������ �������.
        Vector3 pos = (target_ground.position - transform.position).normalized; // ���������� ������ ����������� � ������� �� ������
        Quaternion rot = Quaternion.FromToRotation(-transform.up, pos);  // ����������, �������������� ������ �� ������� � ������ �������
        transform.rotation = rot * transform.rotation;                   // ��������� ���������� � ������

        rigid.AddForce(pos * gravity);                                     // ���������� ��� ������
    }
    
    // � ���� ������ ���-�� ������
    private void OnTriggerEnter(Collider other)
    {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player"))
        {
            player_collider = other.gameObject; // ������ �� ������
            rot_robot_scout = true;
            Debug.Log(debug_obj_name + "����� ����");
            robot_scout_canvas_text.enabled = true;
        }
    }

    // �� ���� ������ ���-�� �����
    private void OnTriggerExit(Collider other)
    {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(debug_obj_name + "������� ����");
            rot_robot_scout = false;
            robot_scout_canvas_text.enabled = false;
        }
    }
    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other)
    {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player"))
        {            
            distance_player = Vector3.Distance(other.transform.position, transform.position); // ��������� ���������� �� ������
            // rot_robot_scout = true;
            Debug.Log(debug_obj_name + "�������� ����, �� ������: " + distance_player.ToString());
        }
    }
    // ������� ������ � ������
    void Direction_robot_scout()
    {
        Vector3 pos = (player_collider.transform.position - transform.position).normalized; // ���������� ������ ����������� � ������
        Quaternion rot = Quaternion.FromToRotation(transform.forward, pos);  // ����������, �������������� ������ �� ������� �������
        Quaternion new_rot = rot * transform.rotation;                   // ����� ���������� �������� ������ � ������
        transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, speed_rot_robot_scout * Time.deltaTime); // ������ ������������
    }
    
    // ����������� ����� ������ �� ������
    private void CanvasTurretLookAt()
    {
        robot_scout_canvas.transform.LookAt(myCamera.transform);
        float y = robot_scout_canvas.transform.localEulerAngles.y;
        robot_scout_canvas.transform.localEulerAngles = new Vector3(0, y, 0);
    }
}