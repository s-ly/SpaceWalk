using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel_Ballon : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� �����
        if (other.gameObject.CompareTag("Player")) {
            FindObjectOfType<fuel_manager>().FuilingBalon();
            Destroy(gameObject); //������� �����
        }
    }
}
