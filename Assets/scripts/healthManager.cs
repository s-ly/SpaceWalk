// �������� �������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    [SerializeField] public int healthPlayer; // ���-�� �������� ������
    [SerializeField] private int bulletDamage; // ���� �� ����
    [SerializeField] private TextMeshProUGUI textHealthPlayer; // ����� ���-�� �������� ������
    [SerializeField] private GameObject DamageRedImage; // ������� �������� �����������

    public int healthPlayerTEMP; // ���-�� �������� ������ (��� ����������)

    // �������� ����
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;



    // Start is called before the first frame update
    void Start()
    {
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

    // ���� ������
    public void Damage()
    {
        healthPlayerTEMP -= bulletDamage; // �������� ����
        if (healthPlayerTEMP < 0) healthPlayerTEMP = 0; // ���-�� �� ������� � �����
        UpdateUIHealthPlayer();
        //Debug.Log("���� ������ � ������");

        



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

    //private void DamageIndicator()
    //{
    //    float timerDamageRedImage = 1f;
        
    //    if (timerDamageRedImage > 0)
    //    {
    //        timerDamageRedImage -= Time.deltaTime;
    //    }
    //    else
    //}


}
