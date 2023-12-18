using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryManager : MonoBehaviour {
    public int battery_level = 0; //������� (��������) (�� �������)
    public int battery_size = 0; //������� �������    
    public int battery_charge_units = 0; //������� ������ � �������� (�������)
    public int battery_charge_percent = 0; //������� ������ � ���������
    public int battery_charge_level = 0; //������� ������ � ��������� (0-6)
    public bool battery_charging_status = false; //���������� �� ��������
    public float battery_auto_charging_speed = 0; //�������� ����������� � 1/��� (������ �����)

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

    private bool isCharging = false; // ���������� �� � ������ �������

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
            "Level ������� (��������): " + STR_battery_level + "\n" +
            "������� �������: " + STR_battery_size + "\n" +
            "������� ������ � ��������: " + STR_battery_charge_units + "\n" +
            "������� ������ � ���������: " + STR_battery_charge_percent + "\n" +
            "������� ������ � ��������� (0-6): " + STR_battery_charge_level + "\n" +
            "������� �������: " + STR_battery_charging_status + "\n" +
            "�������� ������� (1/���): " + STR_battery_auto_charging_speed);

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

    // ���������� ������ � ��������� � ����������� ������ �� ���
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

    // ��� ��������� ���� �� ��������� �������� ������� ���� �����.
    // ������� ��� ���� ����, � ���������� ���������� �����.
    // ���� ����� ��������� ���� �������� ��� ����� ��� 0 �� ���� ���������
    // ��������� ���� ���� � ����������� ����� ����� ����. ���� ����� ���� � �����
    // �� ������������ ����������, ��� ����� ���� ���� ����� ������� ����� ���������.
    // ������� ���������� �������������, ������� ���������.    
    public int ActivationProtectiveField(int bulletDamage) {
        int compensation = 0;
        if (battery_charge_units > 0) {
            int remaining_charge = 0; // ������� ������ ����� ���������
            battery_charge_units -= bulletDamage; // �������� ���� ����
            remaining_charge = battery_charge_units;
            if (remaining_charge >= 0) {
                compensation = bulletDamage;
            }
            else {
                compensation = bulletDamage + remaining_charge;
                battery_charge_units = 0; // �� ����� ���� �������������
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
    // ��� ������� ���������� ������ ���, ����� ���-�� ��������, � ���� ������ �����
    // �������� battery_charge_level.
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