using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// ����������� �����, ������ ��� ���������� ����� �������� � �� Yandex.
// ��� ����� ��� ������� �������� � JavaScript.
[System.Serializable]
public class YandexData
{
    public int Crystal;
    public float Oxygen;
    public int TechnicalContainer;
    public int GameState; // ��������� ����
    public float DATA_time_shot_pause; // ����� �������� (����� ����� ���������� �������� ������).
    public float DATA_player_speed; // ����������� ��������� ������ (�������� � ��������)
    public int DATA_player_health; // �������� ������

    //public string DeviceInfo;
    //public bool TouchKeyboardActive;
}

/* 
�������� ���������, ��������� ������ ����� ��������.
��������� ������� "����� ���". ���������� �������� ��� ����� ������ ������ ����. 
���� �� �� ���� �� ��� ����� �������. �� ��� ������ � ������ Awake(), 
������ ���������� ����� Start(). ���� ������ �� ����� ��������� � ������ �����.

��������� ���� �������� ��� ����������� ����.
0 - ����� �������� �� ����
1 - ����� ������� 10 ��������� � ��������� ����� ���������
2 - ����� ����� ����
3 - ����� ����� ��������� ������� � �������� � ������� �������
4 - ����� ����� �������������� ������� � �������� ������� � �������
5 - ����� ����� ������� ������ � �������� ���� ������� � �������
6 - ������, ����� ����
*/
public class ProgressManager : MonoBehaviour
{
    public YandexData YandexDataOBJ; // ����� � �������
    public static ProgressManager Instance;

    private void Awake()
    {        
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}