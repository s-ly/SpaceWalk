// ������.

using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
    [SerializeField] private Transform turret; // ���� ��������
    [SerializeField] private float speedTurretRot; // �������� ��������
    [SerializeField] private GameObject Bullet; //������ ����
    [SerializeField] private float TimeFire; // ����� ����� ����������
    [SerializeField] GameObject turretCanvas;
    [SerializeField] private int PauseRestartTurretSecond;
    [SerializeField] private GameObject ButtonFirePlayer; // ������ �������� ������

    // ���-���������
    [SerializeField] private Transform GENERATE_technical_container; // ����� ��������� ���-����������
    [SerializeField] private GameObject PREFAB_technical_container;  // ������ ���-����������
    private GameObject CLONE_technical_container;  // ���� ���-����������

    [SerializeField] private int healthTurret; // �������� ������
    private int healhTurretTEMP;

    private bool LookAtTurret = false; // ������������ �� ������
    private bool LiveTurret = true; // ���� �� ������
    private GameObject turretTower; // ����� ������
    private GameObject turretBase; // ��������� ������
    private GameObject pivotRadiousTurret; //�������� ��� ������� �� ������� ������
    private GameObject turretCanvasText;
    private GameObject repairBase; // ��������� ����

    // ������ ����������
    private GameObject EngineVFX_0;
    private GameObject EngineVFX_1;
    private GameObject EngineVFX_2;
    private GameObject EngineVFX_3;

    private GameObject clone; // ������ ����
    private float TimeFireTemp;
    private GameObject Player;
    //private GameObject PlayerPivot;
    [SerializeField] private Animator animatorPlayer;
    private Animator animatorRepairBase;
    private player script_player;
    private GameObject myCamera;

    private GameObject Explosion; // ����� 
    private GameObject Exploded_turret_base; // exploded_turret_base
    private GameObject Exploded_turret_tower; // exploded_turret_tower

    private GameObject PlayerMesh; // ��� ������ � �� �� ��������
    private bool FLAG_generate_technical_Container = true; // ����� �� ������������ ���-�. (������ ������������)


    // Start is called before the first frame update
    void Start() {
        ButtonFirePlayer.SetActive(false); // �������� ������ �������� ������
        turretTower = this.gameObject.transform.GetChild(0).gameObject; // ���� ������ ����� ������ ��� ��������
        pivotRadiousTurret = this.gameObject.transform.parent.gameObject; // ������� ��������
        turretBase = pivotRadiousTurret.gameObject.transform.GetChild(1).gameObject; // ������ ������ (���������)
        repairBase = pivotRadiousTurret.gameObject.transform.GetChild(3).gameObject; // �������� ������ (��������� ����)

        // ������ � ���������� ��������� ����
        EngineVFX_0 = repairBase.gameObject.transform.GetChild(0).gameObject;
        EngineVFX_1 = repairBase.gameObject.transform.GetChild(1).gameObject;
        EngineVFX_2 = repairBase.gameObject.transform.GetChild(2).gameObject;
        EngineVFX_3 = repairBase.gameObject.transform.GetChild(3).gameObject;

        EngineRepairBaseOn(false); // ��������� ��������� ��������� ����

        // ����� ������ (�����)
        Explosion = pivotRadiousTurret.gameObject.transform.GetChild(4).gameObject;
        Explosion.SetActive(false);

        // ���������� ������, ������ � �����������:
        Exploded_turret_base = pivotRadiousTurret.gameObject.transform.GetChild(5).gameObject; //exploded_turret_base
        Exploded_turret_tower = pivotRadiousTurret.gameObject.transform.GetChild(6).gameObject; //exploded_turret_tower
        Exploded_turret_base.GetComponent<Renderer>().enabled = false;
        Exploded_turret_tower.GetComponent<Renderer>().enabled = false;

        animatorRepairBase = repairBase.GetComponent<Animator>();

        healhTurretTEMP = healthTurret; // �������� � TEMP
        turretCanvasText = turretCanvas.transform.GetChild(0).gameObject; // ������ �� �����
        turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();
        turretCanvasText.SetActive(false); // �������� ����� ������

        myCamera = GameObject.FindGameObjectWithTag("MainCamera"); // ������ �� ������
        TimeFireTemp = TimeFire;
        Player = GameObject.FindGameObjectWithTag("Player");
        //PlayerPivot = GameObject.FindGameObjectWithTag("PlayerPivot");
        //animatorPlayer = Player.GetComponent<Animator>();
        script_player = Player.GetComponent<player>();
    }

    // Update is called once per frame
    void Update() {
        if (LookAtTurret) {
            DirectionTurret(); // ���� ������ ������������� �� ������������ � �� �������
            Fire(); // ��������
        }
        CanvasTurretLookAt();
    }

    // � ���� ������ ���-�� ������
    private void OnTriggerEnter(Collider other) {
        // � ���� ����� ����� � ������ ����
        if (other.gameObject.CompareTag("Player") && LiveTurret) {
            LookAtTurret = true;
            Debug.Log("���� ����");

            // ��������� ������ ������ (���)
            animatorPlayer.SetBool("Attack_mode", true);           
            script_player.PlayerModeAttack = true;

            turretCanvasText.SetActive(true); // �������� ����� ������
            //ButtonFirePlayer.SetActive(true); // ���� ������ �������� ������
        }
    }

    // �� ���� ������ ���-�� �����
    private void OnTriggerExit(Collider other) {
        // � ���� ����� ����� � ������ ����
        if (other.gameObject.CompareTag("Player") && LiveTurret) {
            LookAtTurret = false;
            Debug.Log("������� ����");
            //GenerateBullet();

            // ����������� ������ ������ (���)
            animatorPlayer.SetBool("Attack_mode", false);
            script_player.PlayerModeAttack = false;

            turretCanvasText.SetActive(false); // �������� ����� ������
            //ButtonFirePlayer.SetActive(false); // �������� ������ �������� ������
        }
    }

    // � ���� ���-�� ���������
    private void OnTriggerStay(Collider other) {
        // � ���� ����� ����� � ������ ����
        if (other.gameObject.CompareTag("Player") && LiveTurret) {
            LookAtTurret = true;
            // ��������� ������ ������ (���)
            animatorPlayer.SetBool("Attack_mode", true);
            script_player.PlayerModeAttack = true;

            turretCanvasText.SetActive(true); // �������� ����� ������
            //ButtonFirePlayer.SetActive(true); // ���� ������ �������� ������
        }
    }

    // ������� ������
    void DirectionTurret() {
        // ������� ������� � ����
        Vector3 direction = turret.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedTurretRot * Time.deltaTime);

        // ������� ������ �� ��� Y
        float y = transform.localEulerAngles.y; // ���� ��������� ��� Y
        float x = transform.localEulerAngles.x; // ���� ��������� ��� Y
        transform.localEulerAngles = new Vector3(x, y, 0); // �������� ��� �������� ����� ��� Y
    }

    // ��������� � ����������� ���������� ��������� ����.
    private void EngineRepairBaseOn(bool On) {
        if (On) {
            EngineVFX_0.SetActive(true);
            EngineVFX_1.SetActive(true);
            EngineVFX_2.SetActive(true);
            EngineVFX_3.SetActive(true);
        }

        else {
            EngineVFX_0.SetActive(false);
            EngineVFX_1.SetActive(false);
            EngineVFX_2.SetActive(false);
            EngineVFX_3.SetActive(false);
        }
    }

    // ��������
    void Fire() {
        TimeFireTemp = TimeFireTemp - Time.deltaTime;
        if (TimeFireTemp <= 0) {
            GenerateBullet();
            TimeFireTemp = TimeFire;
        }
    }

    // ��������� ����
    void GenerateBullet() {
        clone = Instantiate(Bullet, transform.position, transform.rotation);
        //clone.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        Destroy(clone, 10f); // ����������� ����� 10 ���
    }

    // ��������� ���-���������� �� ����� GENERATE_technical_container
    void Generate_Technical_Container() {
        CLONE_technical_container = Instantiate(
            PREFAB_technical_container,
            GENERATE_technical_container.transform.position,
            GENERATE_technical_container.transform.rotation);
    }

    // ������ �������� ����. ���������� �� ������� ���� ������
    public void TakesDamage() {
        Debug.Log("� ����� ���������");
        healhTurretTEMP -= 10;
        turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();
        if (healhTurretTEMP <= 0) {
            TurretDisabled();
        }
    }

    // ������ ���������
    public void TurretDisabled() {
        Debug.Log("����� ���������");
        LookAtTurret = false;
        LiveTurret = false;
        // Generate_Technical_Container(); // ��������� ���-����������
        Explosion.SetActive(true); // �����

        // ���������� ���������� ������
        Exploded_turret_base.GetComponent<Renderer>().enabled = true;
        Exploded_turret_tower.GetComponent<Renderer>().enabled = true;

        // ��������� ������ ����� � ��������� ������
        turretTower.gameObject.GetComponent<MeshRenderer>().enabled = false;
        turretBase.gameObject.GetComponent<MeshRenderer>().enabled = false;

        // ����������� ������ ������ (���)
        animatorPlayer.SetBool("Attack_mode", false);
        script_player.PlayerModeAttack = false;

        turretCanvasText.SetActive(false); // �������� ����� ������
        //ButtonFirePlayer.SetActive(false); // �������� ������ �������� ������
        if (FLAG_generate_technical_Container) {
            Generate_Technical_Container(); // ��������� ���-����������
            FLAG_generate_technical_Container = false;
        }


        StartCoroutine(PauseRestartTurret(PauseRestartTurretSecond)); // ���������� ������ ����� �����
    }

    // ����������� ����� ������ �� ������
    private void CanvasTurretLookAt() {
        turretCanvas.transform.LookAt(myCamera.transform);
        float y = turretCanvas.transform.localEulerAngles.y;
        turretCanvas.transform.localEulerAngles = new Vector3(0, y, 0);
    }

    // ����� ����� ��������� ������
    IEnumerator PauseRestartTurret(int sec) {

        yield return new WaitForSeconds(sec);
        FLAG_generate_technical_Container = true; // ����� ����� ������������ ���-�.
        animatorRepairBase.SetTrigger("activateRepairBase");// ������ ������������ �������� ��������� ����

        EngineRepairBaseOn(true); // �������� ��������� ��������� ����

        // �������������� ���� - ������ ��������, ����� �������������
        // ������ �� ��������� ������������
        animatorRepairBase.SetBool("TurretBroken", true);

        // ��������������� ��������. ����� ������ ��� ���������, �� ��� �� ��������,
        // ����� ������ "�������".
        int dop_sec = 13;
        yield return new WaitForSeconds(dop_sec);
        RestartTurret();
        yield return new WaitForSeconds(5); // ��� ��� 5 ��� ����� ���������� ����������
        EngineRepairBaseOn(false); // ��������� ��������� ��������� ����
    }

    // ������� ������
    private void RestartTurret() {
        LiveTurret = true;
        Explosion.SetActive(false); // ��������� �����
        animatorRepairBase.SetBool("TurretBroken", false); // �������������� ���� - ������ �������������, ������������� ������

        // ��������������� �������� � ��������� ������
        healhTurretTEMP = healthTurret;
        turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();

        // �������� ������ ����� � ��������� ������
        turretTower.gameObject.GetComponent<MeshRenderer>().enabled = true;
        turretBase.gameObject.GetComponent<MeshRenderer>().enabled = true;

        // ��������� ������ ���������� ������
        Exploded_turret_base.GetComponent<Renderer>().enabled = false;
        Exploded_turret_tower.GetComponent<Renderer>().enabled = false;
    }
}
