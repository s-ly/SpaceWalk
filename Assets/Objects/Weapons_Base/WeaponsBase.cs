using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsBase : MonoBehaviour {
    public GameObject Zone;
    public GameObject store;
    public GameManager SRC_GameManager;
    public bool flag_ui_on = true;
    int GameState;
    // Start is called before the first frame update
    void Start() {
        Zone.SetActive(false);
        store.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }
    // � ���� ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player") && flag_ui_on) {
            GameState = ProgressManager.Instance.YandexDataOBJ.GameState;
            SRC_GameManager.Check_GameState("Weapons_Base"); // �������� ��������� ����
            if (GameState != 3) {
                Zone.SetActive(true);
                store.SetActive(true);
            }
        }
    }
    // �� ���� ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            Zone.SetActive(false);
            store.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player") && flag_ui_on) {
            SRC_GameManager.Check_GameState("Weapons_Base"); // �������� ��������� ����
            Zone.SetActive(true);
            store.SetActive(true);
        }
    }

    // ����� ���������� Canvas (��� ���������� ���������� ���� ����������)
    public void SwitchActive() {
        if (store.activeSelf && Zone.activeSelf) store.SetActive(false);
        else if (!store.activeSelf && Zone.activeSelf) store.SetActive(true);
    }
    public void Store_on() {
        Zone.SetActive(true);
        store.SetActive(true);
        flag_ui_on = true;
    }
    public void Store_off() {
        Zone.SetActive(false);
        store.SetActive(false);
        flag_ui_on = false;
    }
}
