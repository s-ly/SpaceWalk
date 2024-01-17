using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_aid_kit : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // В зону вошёл игрок
        if (other.gameObject.CompareTag("Player")) {
            FindObjectOfType<healthManager>().healthAdd();
            Destroy(gameObject); //стираем балон
        }
    }
}
