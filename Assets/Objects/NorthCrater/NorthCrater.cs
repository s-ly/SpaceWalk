using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NorthCrater : MonoBehaviour
{
    public GameManager SRC_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // � ���� ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� 
        if (other.gameObject.CompareTag("Player")) {
            //Debug.Log("����� � �������");
            SRC_GameManager.Check_GameState("NorthCrater"); // �������� ��������� ����
        }
    }
}
