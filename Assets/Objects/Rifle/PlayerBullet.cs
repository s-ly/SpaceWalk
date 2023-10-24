// пул€ игрока

using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] private GameObject Explosion_Bullet; // взрыв пули

    private GameObject turret;
    private Turret turret_script;
    private GameObject clone_Explosion_Bullet; // клон взрыва пули

    // так как колаидер, по которому стрел€ет игрок ниже в ирархии робота
    // при попадании в коллидер мы используем его родител€, это будет основной объект робота.
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

    // ѕул€ куда-то попала
    private void OnTriggerEnter(Collider other)
    {
        // ѕул€ попала в “урель
        if (other.gameObject.CompareTag("Enemy"))
        {
            /* находим объект турели, который ниже по ирархии,
            получаем его скрипт и вызываем метод получени€ урона. */
            turret = other.gameObject.transform.GetChild(0).gameObject;
            turret_script = turret.GetComponent<Turret>();
            turret_script.TakesDamage();

            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

            Destroy(gameObject); // убиваем пулю игрока
        }

        // ѕул€ попала в –обота
        if (other.gameObject.CompareTag("Enemy_2")) {

            // получаем родител€, то-есть самого робота
            robot_scout_SCRIPT = other.gameObject.GetComponentInParent<robot_scout>();
            robot_scout_SCRIPT.Damage();


            // взрыв пули
            clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
            Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

            Destroy(gameObject); // убиваем пулю игрока
        }
    }
}
