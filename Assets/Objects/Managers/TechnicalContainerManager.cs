using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class TechnicalContainerManager : MonoBehaviour
{
    [SerializeField] private int numberTechnicalContainerLevel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int TechnicalContainerStore; // кол-во тех-к в хранилище

    // Start is called before the first frame update
    void Start()
    {
        // Загрузка данных о тех-к из межуровнего хранилища
        TechnicalContainerStore = ProgressManager.Instance.YandexDataOBJ.TechnicalContainer;
        UpdateUITechnicalContainer();
    }
    
     //кол-во монет
    public void AddOne()
    {
        numberTechnicalContainerLevel ++;
        UpdateUITechnicalContainer();
    }

    // Перемещает собранные кристалы в хранилище.
    // Должен вызываться, когда игрок приходит на базу.
    public void StoreTechnicalContainer()
    {
        TechnicalContainerStore += numberTechnicalContainerLevel;
        numberTechnicalContainerLevel = 0;
        SaveDataTechnicalContainerManager();
        UpdateUITechnicalContainer();
    }

    // Обновляет данные игрока на экране
    public void UpdateUITechnicalContainer()
    {
        if (numberTechnicalContainerLevel != 0)
        {
            text.text = numberTechnicalContainerLevel.ToString() + " + " + TechnicalContainerStore.ToString();
        }
        else
        {
            text.text = TechnicalContainerStore.ToString();
        }
    }

    // Сохранение данных между уровнями
    public void SaveDataTechnicalContainerManager()
    {
        ProgressManager.Instance.YandexDataOBJ.TechnicalContainer = TechnicalContainerStore;
    }
}
