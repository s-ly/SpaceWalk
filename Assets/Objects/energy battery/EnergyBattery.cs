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

    int health = 200; // ��������
    bool destroy = false;

    public GameObject Explosion;// �����
    GameObject Explosion_CLONE;// ����� ��� ������

    // Start is called before the first frame update
    void Start() {
        Base_Off.SetActive(false);
        canvas.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        SCRIPT_player = Player.GetComponent<player>();
        ANIMATOR_player = Player.GetComponent<Animator>();
        text.text = health.ToString(); // ���������� ��������
    }

    // Update is called once per frame
    void Update() {

    }

    // � ���� ������ ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy) {
            canvas.SetActive(true);
            // ��������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    // �� ���� ������ ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy) {
            // �������������� �������� ��������� ����� ������� � �������.
            // ������-�� ���� ������ ������������ ��� �� ������������
            //Vector3 playerPosition = Player.transform.position;
            //Vector3 robotPosition = transform.position;
            //float distance = Vector3.Distance(playerPosition, robotPosition);
            //if (distance >= 5f) {
            //    Debug.Log(debug_obj_name + "������� ����");
            //    rot_robot_scout = false;
            //    robot_scout_canvas_text.enabled = false;

            //    // ����������� ������ ������ (���)
            //    animatorPlayer.SetBool("Attack_mode", false);
            //    SCRIPT_player.PlayerModeAttack = false;
            //}

            canvas.SetActive(false);
            // ����������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", false);
            SCRIPT_player.PlayerModeAttack = false;
        }
    }

    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy) {

            //distance_player = Vector3.Distance(other.transform.position, transform.position); // ��������� ���������� �� ������
            // rot_robot_scout = true;

            //Debug.Log(debug_obj_name + "�������� ����, �� ������: " + distance_player.ToString());

            canvas.SetActive(true);
            // ��������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    void Destroy() {
        destroy = true;

        // ����������� ������ ������ (���)
        ANIMATOR_player.SetBool("Attack_mode", false);
        SCRIPT_player.PlayerModeAttack = false;

        // ��������� ������
        Transform child_base;
        child_base = transform.GetChild(0);
        Explosion_CLONE = Instantiate(Explosion, child_base.position, child_base.rotation);
        Explosion_CLONE.transform.localScale = Vector3.one * 2f;
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // ����������� ����� 2 ���
        Base_On.SetActive(false);
        Base_Off.SetActive(true);
        canvas.SetActive(false);
    }

    public void Damage() {
        health -= 10;
        text.text = health.ToString(); // ���������� ��������
        if (health <= 0) Destroy();
    }
}
