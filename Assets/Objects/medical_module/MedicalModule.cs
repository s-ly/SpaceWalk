using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MedicalModule : MonoBehaviour {
    public GameObject zone;
    healthManager SCR_healthManager;
   
    // Start is called before the first frame update
    void Start() {
        zone.SetActive(false);
        SCR_healthManager = FindObjectOfType<healthManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    // � ���� ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(true);
            SCR_healthManager.healthPlayerRestart(); // рестарт здоровья игрока_1   
        }
    }

    // �� ���� ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(false);
        }
    }
}
