using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxy_container : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� �����
        if (other.gameObject.CompareTag("Player")) {
            FindObjectOfType<oxygen>().oxygenAdd();
            Destroy(gameObject); //������� �����
        }
    }
}
