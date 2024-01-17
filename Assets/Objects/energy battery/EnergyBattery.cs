using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyBattery : MonoBehaviour {
    public int num_battery = 0; // ���������� ����� (1-3)
    public GameManager SRC_GameManager;

    public GameObject Base_On;
    public GameObject Base_Off;
    public TextMeshProUGUI text;
    public GameObject canvas;

    GameObject Player;
    player SCRIPT_player;
    Animator ANIMATOR_player;

    int health = 200; // ��������
    public bool destroy = false;
    public bool active = false;

    public GameObject Explosion;// �����
    GameObject Explosion_CLONE;// ����� ��� ������

    bool DamageOn = false; // �������� �� ����

    // Start is called before the first frame update
    void Start() {
        canvas.SetActive(false);
        Base_Off.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        SCRIPT_player = Player.GetComponent<player>();
        ANIMATOR_player = Player.GetComponent<Animator>();
        text.text = health.ToString(); // ���������� ��������

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
        Debug.Log("��������� �������" + num_battery);
        active = true;

    }

    // � ���� ������ ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(true);
            DamageOn = true;
            // ��������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    // �� ���� ������ ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(false);
            DamageOn = false;
            // ����������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", false);
            SCRIPT_player.PlayerModeAttack = false;
        }
    }

    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && !destroy && active) {
            canvas.SetActive(true);
            DamageOn = true;
            // ��������� ������ ������ (���)
            ANIMATOR_player.SetBool("Attack_mode", true);
            SCRIPT_player.PlayerModeAttack = true;
        }
    }

    void Destroy() {
        destroy = true;
        active = false;

        // ����������� ������ ������ (���)
        ANIMATOR_player.SetBool("Attack_mode", false);
        SCRIPT_player.PlayerModeAttack = false;

        // ��������� ������
        Transform child_base;
        child_base = transform.GetChild(0);
        Explosion_CLONE = Instantiate(Explosion, child_base.position, child_base.rotation);   
        Explosion_CLONE.transform.localScale = Vector3.one * 7f;
        Explosion_CLONE.transform.localPosition += Vector3.up * 1f;
        Explosion_CLONE.SetActive(true);
        Destroy(Explosion_CLONE, 1.18f); // ����������� ����� 2 ���
        Base_On.SetActive(false);
        Base_Off.SetActive(true);
        canvas.SetActive(false);

        SRC_GameManager.CheckDestroyBatteryNum(num_battery);
        SRC_GameManager.Check_GameState("Battery"); // �������� ��������� ����
    }

    public void Damage() {
        if (active && DamageOn) {
            health -= 10;
            text.text = health.ToString(); // ���������� ��������
            if (health <= 0) Destroy();
        }
    }
}
