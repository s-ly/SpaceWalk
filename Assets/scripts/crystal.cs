using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.TextCore.Text;
using UnityEngine;

// ������ ���������, ��������� ��������� ���������.
public class crystal : MonoBehaviour
{
    [SerializeField] private float restartTime; // ����� ����������� ���������
    [SerializeField] int crystal_value = 1; // �������� ��������
    private float restartTimeTimer; // ������ ��������
    private bool hide = false; // ���� ������� ���������
    
    bool aniEnable = false; // ���� �� �������� ��������
    float speedAni = 0.2f; // �������� ����� (����� � Vector3)

    GameObject LOD_0;
    GameObject LOD_1;
    GameObject LOD_2;
    //GameObject LOD_3;

    [SerializeField] AudioSource Crystal_pick; // ���� ��������� ���������

    // Start is called before the first frame update
    void Start()
    {
        restartTimeTimer = restartTime;

        // ������ �� ��� LOD-��
        LOD_0 = transform.GetChild(0).gameObject;
        LOD_1 = transform.GetChild(1).gameObject;
        LOD_2 = transform.GetChild(2).gameObject;
        //LOD_3 = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        waitEnable();
        if (aniEnable) AniEnable();
    }

    // ���������� ����� � ������ ������� ���-�� ��������.
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (hide == false)
            {
                // � ������� crystalManager �������� ����� ���������� ���������
                FindObjectOfType<crystalManager>().AddOne(crystal_value);
                Crystal_pick.Play(); // ����
            }
            transform.localScale = new Vector3(0, 0, 0); // ������ ��������� = 0
            HideLODs(false); // �������� �������
            hide = true;
        }        
    }    

    // �������� ��� ���������� ��������� ���������
    private void waitEnable()
    {
        if (restartTimeTimer > 0 && hide == true)
        {
            restartTimeTimer = restartTimeTimer - Time.deltaTime;
        }

        if (restartTimeTimer <= 0 && hide == true)
        {
            HideLODs(true); // ���������� �������
            hide = false;
            aniEnable = true; // ���� �� �������� �������� (��)
            restartTimeTimer = restartTime; // ���������� ������� �������� ���������
        }
    }

    // �������� ��� ���������� �������, ��� LOD ������
    private void HideLODs(bool show_crystal)
    {        
        LOD_0.SetActive(show_crystal);
        LOD_1.SetActive(show_crystal);
        LOD_2.SetActive(show_crystal);
        //LOD_3.SetActive(show_crystal);
    }

    // �������� ��������� ���������
    void AniEnable()
    {
        if (transform.localScale.x < 0.5f)
        {
            transform.localScale += new Vector3(speedAni, speedAni, speedAni) * Time.deltaTime;
        }
        else
        {
            aniEnable = false; // ���� �� �������� �������� (���)
        }
    }
}
