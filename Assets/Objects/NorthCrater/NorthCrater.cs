using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NorthCrater : MonoBehaviour
{
    public GameManager SRC_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // в зону что-то входит
    private void OnTriggerEnter(Collider other) {
        // В зону вошёл игрок 
        if (other.gameObject.CompareTag("Player")) {
            //Debug.Log("Игрок в кратере");
            SRC_GameManager.Check_GameState("NorthCrater"); // Проверка состояния игры
        }
    }
}
