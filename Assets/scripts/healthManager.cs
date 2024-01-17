// �������� �������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    int healthPlayer; // ���-�� �������� ������
    [SerializeField] private int bulletDamage; // ���� �� ����
    [SerializeField] private TextMeshProUGUI textHealthPlayer; // ����� ���-�� �������� ������
    [SerializeField] private GameObject DamageRedImage; // ������� �������� �����������

    public int healthPlayerTEMP; // ���-�� �������� ������ (��� ����������)

    // �������� ����
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;
    public BatteryManager SCRIPT_BatteryManager;



    // Start is called before the first frame update
    void Start()
    {
        healthPlayer = ProgressManager.Instance.YandexDataOBJ.DATA_player_health;
        healthPlayerRestart();        
        DamageRedImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

        script_GameManager = GameManager.GetComponent<GameManager>(); // �������� ����

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��������� ������ ������ �� ������ (��������)
    public void UpdateUIHealthPlayer()
    {
        textHealthPlayer.text = healthPlayerTEMP.ToString();
    }

    // ���� ������. ���� ������ � ������
    public void Damage()
    {
        int compensation_protective_field = 0;
        compensation_protective_field = SCRIPT_BatteryManager.ActivationProtectiveField(bulletDamage);
        healthPlayerTEMP += compensation_protective_field; // ����������� ��������� ����
        healthPlayerTEMP -= bulletDamage; // �������� ����
        if (healthPlayerTEMP < 0) healthPlayerTEMP = 0; // ���-�� �� ������� � �����
        UpdateUIHealthPlayer();


        // ��������� ��������
        if (healthPlayerTEMP <= 0)
        {            
            script_GameManager.GameOwer(); // �������� GAME OVER
        }
    }

    // ������� �������� ������
    public void healthPlayerRestart()
    {
        healthPlayerTEMP = healthPlayer;
        UpdateUIHealthPlayer();
    }

    public void healthAdd() {
        int aid_kit_size = 10; // ������� ������
        for (int i = 0; i < aid_kit_size; i++) {
            if (healthPlayerTEMP < healthPlayer) {
                healthPlayerTEMP++;
            }
        }
        UpdateUIHealthPlayer();
    }


}
