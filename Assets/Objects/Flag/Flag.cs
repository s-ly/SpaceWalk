using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Флаг завершения игры.
public class Flag : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        script_GameManager = GameManager.GetComponent<GameManager>();
    }    

    // в зону флага что-то входит
    private void OnTriggerEnter(Collider other)
    {
        // В зону вошёл игрок
        if (other.gameObject.CompareTag("Player"))
        {
            script_GameManager.YouWin();
        }
    }
}
