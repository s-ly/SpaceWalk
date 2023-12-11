using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fuel_manager : MonoBehaviour {
    int fuel_max; // ёммкость баков
    int fuel; // сколько осталось в баке
    int jump_price = 10; // цена прыжка в топливах

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

    // запрос на прыжок, возвращает true если прыжок оплачен
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

    // заправка
    public void Refueling() {
        fuel = fuel_max;
        UpdateUIFuel();
    }
}
