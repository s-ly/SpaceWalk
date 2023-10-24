// ���� ������

using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // ����� ����

    private GameObject turret;
    private Turret turret_script;
    private GameObject clone_Explosion_Bullet; // ���� ������ ����

    // ��� ��� ��������, �� �������� �������� ����� ���� � ������� ������
    // ��� ��������� � �������� �� ���������� ��� ��������, ��� ����� �������� ������ ������.
    robot_scout robot_scout_SCRIPT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime, Space.Self);
    }

    // ���� ����-�� ������
    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ � ������
        if (other.gameObject.CompareTag("Enemy"))
        {
            /* ������� ������ ������, ������� ���� �� �������,
            �������� ��� ������ � �������� ����� ��������� �����. */
            turret = other.gameObject.transform.GetChild(0).gameObject;
            turret_script = turret.GetComponent<Turret>();
            turret_script.TakesDamage();

            // ����� ����
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            Destroy(clone_Explosion_Bullet, 2f); // ����������� ����� 2 ���

            Destroy(gameObject); // ������� ���� ������
        }

        // ���� ������ � ������
        if (other.gameObject.CompareTag("Enemy_2")) {

            // �������� ��������, ��-���� ������ ������
            robot_scout_SCRIPT = other.gameObject.GetComponentInParent<robot_scout>();
            robot_scout_SCRIPT.Damage();


            // ����� ����
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            Destroy(clone_Explosion_Bullet, 2f); // ����������� ����� 2 ���

            Destroy(gameObject); // ������� ���� ������
        }
    }
}
