using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy_container : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // В зону вошёл игрок
        if (other.gameObject.CompareTag("Player")) {
            FindObjectOfType<BatteryManager>().EnergyAdd();
            Destroy(gameObject); //стираем балон
        }
    }
}
