using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Скрипт космического челнока (база).
public class SpacePod : MonoBehaviour
{
    [SerializeField] private GameObject GameManager; // ГЛАВНЫЙ АРХИТЕКТОР
    [SerializeField] private GameObject spacePodZone;  // зона пополнения кислорода
    [SerializeField] private GameObject target;
    
    // ссылки на объекты менеджеров (для доступа с скриптам менеджеров)
    [SerializeField] private GameObject CrystalManager; 
    [SerializeField] private GameObject TechnicalContainerManager; 

    [SerializeField] private GameObject StoreUI; // Окно магазина
    [SerializeField] private GameObject ButtnStore; // кнопка активации магазина
    [SerializeField] private GameObject StoreManager; // менеджер магазина
    private GameManager script_GameManager; // скрипт ГЛАВНОГО АРХИТЕКТОРА
    private oxygen actionTarget;
    private crystalManager scriptCrystalManager;
    private StoreManager scriptStoreManager;
    private TechnicalContainerManager SCRIPT_TechnicalContainerManager;

    // Start is called before the first frame update
    void Start()
    {
        // ссылки на скрипты:
        actionTarget = target.GetComponent<oxygen>();
        scriptCrystalManager = CrystalManager.GetComponent<crystalManager>();
        SCRIPT_TechnicalContainerManager = TechnicalContainerManager.GetComponent<TechnicalContainerManager>();
        scriptStoreManager = StoreManager.GetComponent<StoreManager>();    
        script_GameManager = GameManager.GetComponent<GameManager>();

        PlayerExitBase();
    }
    
    // Вызывается когда в тригер объекта что-то попадает.
    // Игрок пополняет кислород.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionTarget.oxygenTimerRestart();
            spacePodZone.SetActive(true);  // включаем зону кислорода                                           
            scriptCrystalManager.StoreCrystal(); // кладём кристаллы в хранилище
            scriptCrystalManager.SaveDataCrystal(); // сохраняем кристаллы между уровнями
            SCRIPT_TechnicalContainerManager.StoreTechnicalContainer(); // кладём Тех-К в хранилище
            SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager(); // сохраняем Тех-К меж-ур
            ButtnStore.SetActive(true); // вкыл кнопка магазина            
            // FindObjectOfType<healthManager>().healthPlayerRestart(); // рестарт здоровья игрока            
            script_GameManager.Check_GameState("PlayerEnterSpacePod"); // Проверка состояния игры
        }
    }

    // Вызывается когда из тригера что-то выходит.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerExitBase();
        }
    }

    // Игрок покинул базу
    private void PlayerExitBase()
    {
        actionTarget.oxygenTimerExit();
        spacePodZone.SetActive(false);  // отключаем зону кислорода
        StoreUI.SetActive(false); // отключаем магазин
        scriptStoreManager.flagStoreUIOn = false; // флаг видимости магазина тоже надо переключить
        ButtnStore.SetActive(false); // выкл кнопка магазина
    }
}
