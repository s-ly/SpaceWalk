// ����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // ����� ����
    private GameObject clone_Explosion_Bullet; // ���� ������ ����
    //[SerializeField] private healthManager scriptHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.forward * inputZoom, Space.Self);
        //transform.position += new Vector3(0, 0, 0);
    }

    // ���� ����-�� ������
    private void OnTriggerEnter(Collider other)
    {

        // ���� ������ � ������
        if (other.gameObject.CompareTag("Player"))
        {  
            // � ������� healthManager �������� ����� �����
            FindObjectOfType<healthManager>().Damage();

            // ����� ����
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            clone_Explosion_Bullet.transform.localScale *= 0.5f;
            Destroy(clone_Explosion_Bullet, 2f); // ����������� ����� 2 ���

            Destroy(gameObject);
        }


    }
}
