using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Тех-контейнер
public class TechnicalContainer : MonoBehaviour
{   
    // в зону контейнера что-то входит
    private void OnTriggerEnter(Collider other)
    {
        // В зону вошёл игрок
        if (other.gameObject.CompareTag("Player"))
        {
            // в скрипте TechnicalContainerManager вызываем метод добавление тех-к
            FindObjectOfType<TechnicalContainerManager>().AddOne(); 
            Destroy(gameObject); //стираем тех-контейнер
        }
    }
}
