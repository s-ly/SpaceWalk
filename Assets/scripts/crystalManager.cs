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
    [SerializeField] public int CrystalStore; // ���-�� ��������� � ���������

    // Start is called before the first frame update
    void Start()
    {
        // �������� ������ � ��������� �� ����������� ��������� 
        CrystalStore = ProgressManager.Instance.YandexDataOBJ.Crystal;
        UpdateUICrystal();
    }

    //���-�� �����
    public void AddOne(int crystal_value)
    {
        numberCrystalLevel += crystal_value;
        UpdateUICrystal();
    }

    // ���������� ��������� �������� � ���������.
    // ������ ����������, ����� ����� �������� �� ����.
    public void StoreCrystal()
    {
        CrystalStore = CrystalStore + numberCrystalLevel;
        numberCrystalLevel = 0;
        UpdateUICrystal();
    }

    // ��������� ������ ������ �� ������
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

    // ���������� ������ ����� ��������
    public void SaveDataCrystal()
    {
        ProgressManager.Instance.YandexDataOBJ.Crystal = CrystalStore;
    }
}
