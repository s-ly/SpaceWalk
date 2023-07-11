using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.TextCore.Text;
using UnityEngine;

// Скрипт кристалов, управляет поведение кристалов.
public class crystal : MonoBehaviour
{
    [SerializeField] private float restartTime; // время перезапуска кристалла
    [SerializeField] int crystal_value = 1; // ценность кристала
    private float restartTimeTimer; // таймер ожидания
    private bool hide = false; // флаг скрытие кристалла
    
    bool aniEnable = false; // надо ли включать анимацию
    float speedAni = 0.2f; // скорость роста (пойдёт в Vector3)

    GameObject LOD_0;
    GameObject LOD_1;
    GameObject LOD_2;
    //GameObject LOD_3;

    [SerializeField] AudioSource Crystal_pick; // звук забирания кристалла

    // Start is called before the first frame update
    void Start()
    {
        restartTimeTimer = restartTime;

        // доступ ко все LOD-ам
        LOD_0 = transform.GetChild(0).gameObject;
        LOD_1 = transform.GetChild(1).gameObject;
        LOD_2 = transform.GetChild(2).gameObject;
        //LOD_3 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        waitEnable();
        if (aniEnable) AniEnable();
    }

    // Вызывается когда в тригер объекта что-то попадает.
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (hide == false)
            {
                // в скрипте crystalManager вызываем метод добавление кристалов
                FindObjectOfType<crystalManager>().AddOne(crystal_value);
                Crystal_pick.Play(); // звук
            }
            transform.localScale = new Vector3(0, 0, 0); // размер кристалла = 0
            HideLODs(false); // скрываем кристал
            hide = true;
        }        
    }    

    // ожидание для повторного появления кристалла
    private void waitEnable()
    {
        if (restartTimeTimer > 0 && hide == true)
        {
            restartTimeTimer = restartTimeTimer - Time.deltaTime;
        }

        if (restartTimeTimer <= 0 && hide == true)
        {
            HideLODs(true); // показываем кристал
            hide = false;
            aniEnable = true; // надо ли включать анимацию (да)
            restartTimeTimer = restartTime; // сбрасываем счётчик ожидания появления
        }
    }

    // скрывает или показывает кристал, все LOD группы
    private void HideLODs(bool show_crystal)
    {        
        LOD_0.SetActive(show_crystal);
        LOD_1.SetActive(show_crystal);
        LOD_2.SetActive(show_crystal);
        //LOD_3.SetActive(show_crystal);
    }

    // анимация появления кристалла
    void AniEnable()
    {
        if (transform.localScale.x < 0.5f)
        {
            transform.localScale += new Vector3(speedAni, speedAni, speedAni) * Time.deltaTime;
        }
        else
        {
            aniEnable = false; // надо ли включать анимацию (нет)
        }
    }
}
