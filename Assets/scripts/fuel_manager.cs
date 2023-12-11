using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fuel_manager : MonoBehaviour {
    int fuel_max; // �������� �����
    int fuel; // ������� �������� � ����
    int jump_price = 10; // ���� ������ � ��������

    [SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start() {
        fuel_max = ProgressManager.Instance.YandexDataOBJ.DATA_fuel;
        fuel = fuel_max;
        UpdateUIFuel();
    }

    // Update is called once per frame
    void Update() {

    }

    // ������ �� ������, ���������� true ���� ������ �������
    public bool JumpRequest() {
        bool flag = false;

        if (fuel >= jump_price) {
            fuel = fuel - jump_price;
            flag = true;
            UpdateUIFuel();
        }
        return flag;
    }

    public void UpdateUIFuel() {
        text.text = fuel.ToString();
    }

    // ��������
    public void Refueling() {
        fuel = fuel_max;
        UpdateUIFuel();
    }
}
