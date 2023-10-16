// ��������� ���������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rifle : MonoBehaviour {
    [SerializeField] private GameObject playerBullet; //������ ���� ������

    // ����� �������� (����� ����� ���������� �������� ������).
    private float time_shot_pause; // ������� �� DATA ���������
    private float TEMP_time_shot_pause; // ��� ���������� � ��������

    private GameObject clonePlayerBullet; // ������ ���� ������
    private bool TimeFireFlag = true;

    // ���� � �������.
    // � ������ ���� ��������� �������, ���� � ���� �������� ����,
    // �� ����� �������� �������������
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

    // � ���� ���-�� �����
    private void OnTriggerEnter(Collider other) {
        // � ������� ������ �������� ����
        if (other.gameObject.CompareTag("Enemy")) {
            lookOnEnemy = true; // ���� � ������� ��������
        }
    }

    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other) {
        // � ������� ������ ��������� ����
        if (other.gameObject.CompareTag("Enemy")) {
            lookOnEnemy = true; // ���� � �������
        }
    }

    // �� ���� ���-�� �����
    private void OnTriggerExit(Collider other) {
        // �� ������� ������ ����� ����
        if (other.gameObject.CompareTag("Enemy")) {
            lookOnEnemy = false; // ���� ����� �� �������
        }
    }

    /* ������ �����������. ��������� ������ ����.
    ��� ������ ����� �������, �� ���� TimeFireFlag = true.
    ��������� (���������) ������� ����������. */
    void FirePeriodPause() {
        if (TimeFireFlag == false) {
            TEMP_time_shot_pause -= Time.deltaTime;
            if (TEMP_time_shot_pause <= 0) {
                TimeFireFlag = true;
                TEMP_time_shot_pause = time_shot_pause;
            }
        }
    }

    // �������� ���� ������ ���-�� ��������������
    // � � ������� ���� ����
    public void FirePlayer() {
        if (TimeFireFlag && lookOnEnemy) {
            GenerateBulletPlayer();
            TimeFireFlag = false;
        }
    }

    // ��������� ����
    public void GenerateBulletPlayer() {
        clonePlayerBullet = Instantiate(playerBullet, transform.position, transform.rotation);
        Destroy(clonePlayerBullet, 10f); // ����������� ����� 10 ���

    }
}
