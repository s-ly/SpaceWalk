using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LabBase : MonoBehaviour {
    public GameObject Zone;
    public GameObject store;
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
        if (other.gameObject.CompareTag("Player")) {
            Zone.SetActive(true);
            store.SetActive(true);
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

    // ����� ���������� Canvas (��� ���������� ���������� ���� ����������)
    public void SwitchActive() {
        if (store.activeSelf && Zone.activeSelf) store.SetActive(false);
        else if (!store.activeSelf && Zone.activeSelf) store.SetActive(true);
    }
}
