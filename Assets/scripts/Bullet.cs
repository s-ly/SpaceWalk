// пуля

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // взрыв пули
    private GameObject clone_Explosion_Bullet; // клон взрыва пули
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

    // Пуля куда-то попала
    private void OnTriggerEnter(Collider other)
    {

        // Пуля попала в игрока
        if (other.gameObject.CompareTag("Player"))
        {  
            // в скрипте healthManager вызываем метод урона
            FindObjectOfType<healthManager>().Damage();

            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            clone_Explosion_Bullet.transform.localScale *= 0.5f;
            Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

            Destroy(gameObject);
        }


    }
}
