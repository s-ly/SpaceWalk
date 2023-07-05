using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class TechnicalContainerManager : MonoBehaviour
{
    [SerializeField] private int numberTechnicalContainerLevel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] public int TechnicalContainerStore; // ���-�� ���-� � ���������

    // Start is called before the first frame update
    void Start()
    {
        // �������� ������ � ���-� �� ����������� ���������
        TechnicalContainerStore = ProgressManager.Instance.YandexDataOBJ.TechnicalContainer;
        UpdateUITechnicalContainer();
    }
    
     //���-�� �����
    public void AddOne()
    {
        numberTechnicalContainerLevel ++;
        UpdateUITechnicalContainer();
    }

    // ���������� ��������� �������� � ���������.
    // ������ ����������, ����� ����� �������� �� ����.
    public void StoreTechnicalContainer()
    {
        TechnicalContainerStore += numberTechnicalContainerLevel;
        numberTechnicalContainerLevel = 0;
        SaveDataTechnicalContainerManager();
        UpdateUITechnicalContainer();
    }

    // ��������� ������ ������ �� ������
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

    // ���������� ������ ����� ��������
    public void SaveDataTechnicalContainerManager()
    {
        ProgressManager.Instance.YandexDataOBJ.TechnicalContainer = TechnicalContainerStore;
    }
}
