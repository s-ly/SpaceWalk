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

    // Вызывается когда в тригер объекта что-то попадает.
    // Игрок пополняет топливо.
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(true);
            SCRIPT_fuel_manager.Refueling(); // заправка
        }
    }

    // Вызывается когда из тригера что-то выходит.
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            zone.SetActive(false);
        }
    }
}
