// пуля игрока

using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // взрыв пули

    private GameObject turret;
    private Turret turret_script;
    private GameObject clone_Explosion_Bullet; // клон взрыва пули

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime, Space.Self);
    }

    // Пуля куда-то попала
    private void OnTriggerEnter(Collider other)
    {
        // Пуля попала в Турель
        if (other.gameObject.CompareTag("Enemy"))
        {
            /* находим объект турели, который ниже по ирархии,
            получаем его скрипт и вызываем метод получения урона. */
            turret = other.gameObject.transform.GetChild(0).gameObject;
            turret_script = turret.GetComponent<Turret>();
            turret_script.TakesDamage();

            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

            Destroy(gameObject); // убиваем пулю игрока
        }
    }
}
