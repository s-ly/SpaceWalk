using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy_container : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� �����
        if (other.gameObject.CompareTag("Player")) {
            FindObjectOfType<BatteryManager>().EnergyAdd();
            Destroy(gameObject); //������� �����
        }
    }
}
