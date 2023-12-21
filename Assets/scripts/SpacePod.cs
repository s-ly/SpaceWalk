using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������������ ������� (����).
public class SpacePod : MonoBehaviour
{
    [SerializeField] private GameObject GameManager; // ������� ����������
    [SerializeField] private GameObject spacePodZone;  // ���� ���������� ���������
    [SerializeField] private GameObject target;
    
    // ������ �� ������� ���������� (��� ������� � �������� ����������)
    [SerializeField] private GameObject CrystalManager; 
    [SerializeField] private GameObject TechnicalContainerManager; 

    [SerializeField] private GameObject StoreUI; // ���� ��������
    [SerializeField] private GameObject ButtnStore; // ������ ��������� ��������
    [SerializeField] private GameObject StoreManager; // �������� ��������
    private GameManager script_GameManager; // ������ �������� �����������
    private oxygen actionTarget;
    private crystalManager scriptCrystalManager;
    private StoreManager scriptStoreManager;
    private TechnicalContainerManager SCRIPT_TechnicalContainerManager;

    // Start is called before the first frame update
    void Start()
    {
        // ������ �� �������:
        actionTarget = target.GetComponent<oxygen>();
        scriptCrystalManager = CrystalManager.GetComponent<crystalManager>();
        SCRIPT_TechnicalContainerManager = TechnicalContainerManager.GetComponent<TechnicalContainerManager>();
        scriptStoreManager = StoreManager.GetComponent<StoreManager>();    
        script_GameManager = GameManager.GetComponent<GameManager>();

        PlayerExitBase();
    }
    
    // ���������� ����� � ������ ������� ���-�� ��������.
    // ����� ��������� ��������.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionTarget.oxygenTimerRestart();
            spacePodZone.SetActive(true);  // �������� ���� ���������                                           
            scriptCrystalManager.StoreCrystal(); // ����� ��������� � ���������
            scriptCrystalManager.SaveDataCrystal(); // ��������� ��������� ����� ��������
            SCRIPT_TechnicalContainerManager.StoreTechnicalContainer(); // ����� ���-� � ���������
            SCRIPT_TechnicalContainerManager.SaveDataTechnicalContainerManager(); // ��������� ���-� ���-��
            ButtnStore.SetActive(true); // ���� ������ ��������            
            // FindObjectOfType<healthManager>().healthPlayerRestart(); // ������� �������� ������            
            script_GameManager.Check_GameState("PlayerEnterSpacePod"); // �������� ��������� ����
        }
    }

    // ���������� ����� �� ������� ���-�� �������.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerExitBase();
        }
    }

    // ����� ������� ����
    private void PlayerExitBase()
    {
        actionTarget.oxygenTimerExit();
        spacePodZone.SetActive(false);  // ��������� ���� ���������
        StoreUI.SetActive(false); // ��������� �������
        scriptStoreManager.flagStoreUIOn = false; // ���� ��������� �������� ���� ���� �����������
        ButtnStore.SetActive(false); // ���� ������ ��������
    }
}
