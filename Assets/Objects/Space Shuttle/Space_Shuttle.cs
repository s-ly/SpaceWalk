using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Управление челноком
public class Space_Shuttle : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;

    // Start вызывается перед обновлением первого кадра
    void Start()
    {
        script_GameManager = GameManager.GetComponent<GameManager>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     // В зону вошёл игрок 
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         script_GameManager.YouWin();
    //     }
    // }
    
    // Вызывается когда в тригер объекта что-то попадает.
    // Игрок попадает в зону Шатла.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {                      
            script_GameManager.Check_GameState("PlayerEnter_Space_Shuttle"); // Проверка состояния игры
        }
    }
}
