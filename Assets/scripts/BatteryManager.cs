using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryManager : MonoBehaviour {
    public int battery_level = 0; //уровень (прокачка) (из Яндекса)
    public int battery_size = 0; //емкость батареи    
    public int battery_charge_units = 0; //уровень заряда в единицах (текущий)
    public int battery_charge_percent = 0; //уровень заряда в процентах
    public int battery_charge_level = 0; //уровень заряда в состоянии (0-6)
    public bool battery_charging_status = false; //заряжается ли бабатеря
    public float battery_auto_charging_speed = 0; //скорость самозарядки в 1/сек (меньше лучше)

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] player SCRIPT_player;
    [SerializeField] ForceField SCRIPT_ForceField;
    [SerializeField] Material player_material;
    [SerializeField] Texture battery_0;
    [SerializeField] Texture battery_1;
    [SerializeField] Texture battery_2;
    [SerializeField] Texture battery_3;
    [SerializeField] Texture battery_4;
    [SerializeField] Texture battery_5;
    [SerializeField] Texture battery_6;

    private bool isCharging = false; // заряжается ли в данную секундк

    // Start is called before the first frame update
    void Start() {
        battery_level = ProgressManager.Instance.YandexDataOBJ.DATA_battary_level;
        BatteryInIt();
        BatteryRecharge();
        BtteryChargeMath();
        UpdateDevUIBattery();
    }

    // Update is called once per frame
    void Update() {
        if (battery_charging_status != !SCRIPT_player.PlayerModeAttack) {
            battery_charging_status = !SCRIPT_player.PlayerModeAttack;
            UpdateDevUIBattery();
        }
        if (battery_charging_status && battery_charge_units != battery_size && !isCharging) StartCoroutine(ChargerTime());
    }

    public void SaveDataLevelBattery() {
        ProgressManager.Instance.YandexDataOBJ.DATA_battary_level = battery_level;
    }

    public void UpdateDevUIBattery() {
        string STR_battery_level = battery_level.ToString();
        string STR_battery_size = battery_size.ToString();
        string STR_battery_charge_units = battery_charge_units.ToString();
        string STR_battery_charge_percent = battery_charge_percent.ToString();
        string STR_battery_charge_level = battery_charge_level.ToString();
        string STR_battery_charging_status = battery_charging_status.ToString();
        string STR_battery_auto_charging_speed = battery_auto_charging_speed.ToString();

        string STR_BatteryDevData = (
            "Level батареи (прокачка): " + STR_battery_level + "\n" +
            "Ёмкость батареи: " + STR_battery_size + "\n" +
            "Уровень заряда в единицах: " + STR_battery_charge_units + "\n" +
            "Уровень заряда в процентах: " + STR_battery_charge_percent + "\n" +
            "Уровень заряда в состоянии (0-6): " + STR_battery_charge_level + "\n" +
            "Статутс зарядки: " + STR_battery_charging_status + "\n" +
            "Скорость зарядки (1/сек): " + STR_battery_auto_charging_speed);

        text.text = STR_BatteryDevData;
        SetTextures();
    }

    public void BatteryInIt() {

        switch (battery_level) {
            case 0: {
                    battery_size = 60;
                    battery_auto_charging_speed = 1.0f;
                    break;
                }
            case 1: {
                    battery_size = 70;
                    battery_auto_charging_speed = 0.95f;
                    break;
                }
            case 2: {
                    battery_size = 80;
                    battery_auto_charging_speed = 0.9f;
                    break;
                }
            case 3: {
                    battery_size = 90;
                    battery_auto_charging_speed = 0.85f;
                    break;
                }
            case 4: {
                    battery_size = 100;
                    battery_auto_charging_speed = 0.8f;
                    break;
                }

        }

    }

    public void BatteryRecharge() {
        battery_charge_units = battery_size;
    }

    // вычисление заряда в процентак и выставление уровня по ним
    public void BtteryChargeMath() {
        float temp_battery_size = (float)battery_size;
        float temp_battery_charge_units = (float)battery_charge_units;
        float temp_battery_charge_percent = (float)battery_charge_percent;

        temp_battery_charge_percent = ((temp_battery_charge_units * 100.0f) / temp_battery_size);
        battery_charge_percent = (int)Math.Round(temp_battery_charge_percent);

        if (battery_charge_percent <= 0) battery_charge_level = 0;
        else if (battery_charge_percent >= 1 && battery_charge_percent <= 17) battery_charge_level = 1;
        else if (battery_charge_percent >= 18 && battery_charge_percent <= 33) battery_charge_level = 2;
        else if (battery_charge_percent >= 34 && battery_charge_percent <= 50) battery_charge_level = 3;
        else if (battery_charge_percent >= 51 && battery_charge_percent <= 67) battery_charge_level = 4;
        else if (battery_charge_percent >= 68 && battery_charge_percent <= 83) battery_charge_level = 5;
        else if (battery_charge_percent >= 84) battery_charge_level = 6;
    }

    // При попадании пули из менеджера здоровья взывает этот метод.
    // Передаём ему силу пули, а возвращает компенсацю урона.
    // Если после попадания пули остается ещё заряд или 0 то поле полностью
    // поглатило урон пули и компенсация равна урону поли. Если заряд ушёл в минус
    // то момпенчсация уменшается, она равна урон пули минус остаток после вычмтания.
    // Остаток становится отрицательным, поэтому складываю.    
    public int ActivationProtectiveField(int bulletDamage) {
        int compensation = 0;
        if (battery_charge_units > 0) {
            int remaining_charge = 0; // остаток заряда после попадания
            battery_charge_units -= bulletDamage; // вычитаем урон пули
            remaining_charge = battery_charge_units;
            if (remaining_charge >= 0) {
                compensation = bulletDamage;
            }
            else {
                compensation = bulletDamage + remaining_charge;
                battery_charge_units = 0; // не может быть отрицательным
            }
            BtteryChargeMath();
            UpdateDevUIBattery();
            SCRIPT_ForceField.FieldActive();
        }
        return compensation;
    }

    void Charger() {
        if (battery_charge_units != battery_size) {
            battery_charge_units++;
            BtteryChargeMath();
            UpdateDevUIBattery();
        }
    }
    IEnumerator ChargerTime() {
        isCharging = true;
        yield return new WaitForSecondsRealtime(battery_auto_charging_speed);
        Charger();
        isCharging = false;

    }

    // TODO
    // Эта функция вызывается каждый раз, когда что-то меняется, а надо только когда
    // меняется battery_charge_level.
    void SetTextures() {
        if (battery_charge_level == 0) player_material.SetTexture("_MainTex", battery_0);
        else if (battery_charge_level == 1) player_material.SetTexture("_MainTex", battery_1);
        else if (battery_charge_level == 2) player_material.SetTexture("_MainTex", battery_2);
        else if (battery_charge_level == 3) player_material.SetTexture("_MainTex", battery_3);
        else if (battery_charge_level == 4) player_material.SetTexture("_MainTex", battery_4);
        else if (battery_charge_level == 5) player_material.SetTexture("_MainTex", battery_5);
        else if (battery_charge_level == 6) player_material.SetTexture("_MainTex", battery_6);
    }
}