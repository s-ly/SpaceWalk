using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���������� ����.
public class Flag : MonoBehaviour
{
    [SerializeField] private GameObject GameManager;
    private GameManager script_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        script_GameManager = GameManager.GetComponent<GameManager>();
    }    

    // � ���� ����� ���-�� ������
    private void OnTriggerEnter(Collider other)
    {
        // � ���� ����� �����
        if (other.gameObject.CompareTag("Player"))
        {
            script_GameManager.YouWin();
        }
    }
}
