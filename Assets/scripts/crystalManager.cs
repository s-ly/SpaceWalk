using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;
using Newtonsoft.Json.Linq;

public class crystalManager : MonoBehaviour
{
    [SerializeField] private int numberCrystalLevel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int CrystalStore; // кол-во кристалов в хранилище

    // Start is called before the first frame update
    void Start()
    {
        // «агрузка данных о кристалах из межуровнего хранилища 
        CrystalStore = ProgressManager.Instance.YandexDataOBJ.Crystal;
        UpdateUICrystal();
    }

    //кол-во монет
    public void AddOne(int crystal_value)
    {
        numberCrystalLevel += crystal_value;
        UpdateUICrystal();
    }

    // ѕеремещает собранные кристалы в хранилище.
    // ƒолжен вызыватьс€, когда игрок приходит на базу.
    public void StoreCrystal()
    {
        CrystalStore = CrystalStore + numberCrystalLevel;
        numberCrystalLevel = 0;
        UpdateUICrystal();
    }

    // ќбновл€ет данные игрока на экране
    public void UpdateUICrystal()
    {
        if (numberCrystalLevel != 0)
        {
            text.text = numberCrystalLevel.ToString() + " + " + CrystalStore.ToString();
        }
        else
        {
            text.text = CrystalStore.ToString();
        }
    }

    // —охранение данных между уровн€ми
    public void SaveDataCrystal()
    {
        ProgressManager.Instance.YandexDataOBJ.Crystal = CrystalStore;
    }
}
