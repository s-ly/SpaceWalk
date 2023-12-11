using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuel_tank : MonoBehaviour {
    public GameObject zone;
    public fuel_manager SCRIPT_fuel_manager;

    // Start is called before the first frame update
    void Start() {
        zone.SetActive(false);
    }

    // ���������� ����� � ������ ������� ���-�� ��������.
    // ����� ��������� �������.
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(true);
            SCRIPT_fuel_manager.Refueling(); // ��������
        }
    }

    // ���������� ����� �� ������� ���-�� �������.
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(false);
        }
    }
}
