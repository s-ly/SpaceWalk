using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Тригер-терминал key
public class Trigger_Terminal : MonoBehaviour
{
    [SerializeField] private GameObject GameManager; // ГЛАВНЫЙ АРХИТЕКТОР
    private GameManager script_GameManager; // скрипт ГЛАВНОГО АРХИТЕКТОРА
    [SerializeField] ArrowManager arrowManager; // управляет стрелками

    // какой именно терминал
    [SerializeField] bool on_key = false;
    [SerializeField] bool on_fuel = false;
    [SerializeField] bool on_energy = false;

    GameObject Terminal_DISPLAY;
    GameObject Terminal_Zone;

    bool ActiveTerminal = false; // активен ли терминал

    // Start is called before the first frame update
    void Start()
    {
        // ссылки на скрипты:
        script_GameManager = GameManager.GetComponent<GameManager>();

        // дочернии объекты терминала
        Terminal_DISPLAY = transform.GetChild(0).gameObject;
        Terminal_Zone = transform.GetChild(1).gameObject;

        ActiveTermonal(false);
    }
    
    // Вызывается когда в тригер объекта что-то попадает.
    // Игрок попадает в зону тринера терминала.
    // Дополнительно проверим, включен ли терминал.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ActiveTerminal)
        {
            script_GameManager.BAG_Player(on_key, on_fuel, on_energy); // кладём в сумку игрока
            arrowManager.ArrowControl(4); // переключение вспомогательных стрелок
            ActiveTermonal(false);
        }
    }

    // активирует или деактивирует терминал
    public void ActiveTermonal(bool active)
    {
        ActiveTerminal = active;
        Terminal_DISPLAY.SetActive(active);
        Terminal_Zone.SetActive(active);
    }
}