using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour {
    [SerializeField] fuel_manager script_fuel_manager;
    [SerializeField] private Rigidbody rig;
    [SerializeField] private Transform Target;

    //[SerializeField] private float speed = 22.0f; // �������� ����������� ������
    float speed = 22.0f; // �������� ����������� ������
    float player_speed = 1.0f; // ����������� ��������� ������ (�������� � ��������)

    [SerializeField] private float rotSpeed = 6.0f;
    [SerializeField] private float gravity = 2.0f;
    [SerializeField] private float jamp = 2.0f;
    float forward_jamp = 50.0f;

    [SerializeField] private float speedOld;
    [SerializeField] private float rot;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject PlayerRifle;
    private Player_Rifle script_PlayerRifle;

    public bool PlayerModeAttack = false; // ����� ����� ������    

    // ������
    bool PlayerOnGround = false; //Player ��������� �� ����������� � ����� �������
    bool PlayerJumpBase_enter = false; // Player �������� ������� ������
    bool PlayerJumpEngine_enter = false; // Player �������� ���������� ������
    bool PlayerJumpEngineForward_enter = false; // Player �������� ���������� ������ ������

    [SerializeField] private GameObject button_move_jamp_engine;

    [SerializeField] GameObject Engines; // ���������
    [SerializeField] GameObject engines_forward; // ��������� ������

    private bool pressA = false;
    private bool pressS = false;
    private bool pressD = false;
    private bool pressW = false;
    private bool pressQ = false;
    private bool pressE = false;
    private bool pressSpace = false;

    // Start is called before the first frame update
    void Start() {
        script_PlayerRifle = PlayerRifle.GetComponent<Player_Rifle>();
        Engines.SetActive(false);
        engines_forward.SetActive(false);
        player_speed = ProgressManager.Instance.YandexDataOBJ.DATA_player_speed;
        button_move_jamp_engine.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        /* ������. ���-��� �� ������ ��������� ������ � ������ ���������� �������, 
       * �� ������� ��� � Update(). �����, ����� �� ������ ���������. */
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            jumpPlayer();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveJumpPlayer();
        }

        //if (((Input.GetKey(KeyCode.Space)) || pressSpace) && (PlayerModeAttack))
        if (PlayerModeAttack) // ���� �������� ��� �������
        {
            script_PlayerRifle.FirePlayer();
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            animator.SetBool("run", false);
        }

        if (Input.GetKeyUp(KeyCode.S)) {
            animator.SetBool("run", false);
        }

        if (Input.GetKeyUp(KeyCode.A)) {
            animator.SetBool("run", false);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            animator.SetBool("run", false);
        }
    }

    private void FixedUpdate() {
        // ������������ ������ � ������ �������.
        Vector3 pos = (Target.position - transform.position).normalized; // ���������� ������ ����������� � ������� �� ������
        Quaternion rot = Quaternion.FromToRotation(-transform.up, pos);  // ����������, �������������� ������ �� ������� � ������ �������
        transform.rotation = rot * transform.rotation;                   // ��������� ���������� � ������

        rig.AddForce(pos * gravity);                                     // ���������� ��� ������

        // ���������� �������
        if ((Input.GetKey(KeyCode.W)) || pressW)
        //if ((Input.GetKey(KeyCode.UpArrow)) || pressW)
        {
            rig.AddForce(transform.forward * speed * player_speed);
            animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.S) || pressS) {
            rig.AddForce(-transform.forward * speed * player_speed);
            animator.SetBool("run", true);
        }

        if ((Input.GetKey(KeyCode.LeftArrow)) || pressA) {
            rig.AddTorque(-transform.up * rotSpeed);
        }

        if ((Input.GetKey(KeyCode.RightArrow)) || pressD) {
            rig.AddTorque(transform.up * rotSpeed);
        }

        if ((Input.GetKey(KeyCode.A)) || pressQ) {
            rig.AddForce(-transform.right * speed * player_speed);
            animator.SetBool("run", true);
        }



        if ((Input.GetKey(KeyCode.D)) || pressE) {
            rig.AddForce(transform.right * speed * player_speed);
            animator.SetBool("run", true);
        }
    }

    // ������ ������
    public void jumpPlayer() {
        if (PlayerOnGround && PlayerJumpBase_enter == false && PlayerJumpEngine_enter == false) {
            rig.AddForce(transform.up * speed * jamp);
            animator.SetTrigger("jump");
            PlayerJumpBase_enter = true;
            PlayerJumpEngine_enter = false;
            PlayerJumpEngineForward_enter = false;
            button_move_jamp_engine.SetActive(true);
        }
        else if (PlayerJumpBase_enter == true && PlayerJumpEngine_enter == false) {
            if (script_fuel_manager.JumpRequest()) {
                rig.AddForce(transform.up * speed * (jamp * 1.0f));
                PlayerJumpBase_enter = true;
                PlayerJumpEngine_enter = true;
                Engines.SetActive(true);
            }
        }
    }
    public void MoveJumpPlayer() {
        if (PlayerJumpBase_enter == true && PlayerJumpEngineForward_enter == false) {
            if (script_fuel_manager.JumpRequest()) {
                rig.AddForce(transform.forward * speed * forward_jamp);
                PlayerJumpBase_enter = true;
                PlayerJumpEngineForward_enter = true;
                engines_forward.SetActive(true);
                button_move_jamp_engine.SetActive(false);
            }
        }
    }

    public void pressSpaceTrue() {
        pressSpace = true;
    }
    public void pressSpaceFalse() {
        pressSpace = false;
    }
    public void PressATrue() {
        pressA = true;
    }
    public void PressAFalse() {
        pressA = false;
    }
    public void PressSTrue() {
        pressS = true;
    }
    public void PressSFalse() {
        pressS = false;
        animator.SetBool("run", false);
    }
    public void PressDTrue() {
        pressD = true;
    }
    public void PressDFalse() {
        pressD = false;
    }
    public void PressWTrue() {
        pressW = true;
    }
    public void PressWFalse() {
        pressW = false;
        animator.SetBool("run", false);
    }
    public void PressQTrue() {
        pressQ = true;
    }
    public void PressQFalse() {
        pressQ = false;
        animator.SetBool("run", false);
    }
    public void PressETrue() {
        pressE = true;
    }
    public void PressEFalse() {
        pressE = false;
        animator.SetBool("run", false);
    }

    ///////////////////////////// ����������� ������������� � �¨����� ������ /////////////////////
    void OnCollisionEnter(Collision collision) {
        print("--------------------------------- Collision detected");
        Engines.SetActive(false);
        engines_forward.SetActive(false);
        PlayerJumpBase_enter = false;
        PlayerJumpEngine_enter = false;
        PlayerJumpEngineForward_enter = false;
        button_move_jamp_engine.SetActive(false);
    }
    void OnCollisionStay(Collision collision) {
        PlayerOnGround = true;
    }
    void OnCollisionExit(Collision collision) {
        PlayerOnGround = false;
        //Engines.SetActive(true);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
}