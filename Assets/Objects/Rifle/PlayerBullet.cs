// пуля игрока

using UnityEngine;

public class PlayerBullet : MonoBehaviour {
  [SerializeField] private float speedBullet;
  [SerializeField] private GameObject Explosion_Bullet; // взрыв пули

  private GameObject turret;
  private Turret turret_script;
  private GameObject clone_Explosion_Bullet; // клон взрыва пули

  EnergyBattery SCRIPT_EnergyBattery;
  //GameObject EnergyBattery;

  // так как колаидер, по которому стреляет игрок ниже в ирархии робота
  // при попадании в коллидер мы используем его родителя, это будет основной объект робота.
  robot_scout robot_scout_SCRIPT;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    transform.Translate(Vector3.forward * speedBullet * Time.deltaTime, Space.Self);
  }

  // Пуля куда-то попала
  private void OnTriggerEnter(Collider other) {

    // Пуля попала в Enemy_3
    if (other.gameObject.CompareTag("Enemy_3")) {
      Debug.Log("ПУЛЯ ПОПАЛА!!!--------------------------!!!!");
      GameObject missileTurret = other.gameObject;
      MissileTurret SCRIPT_missileTurret = missileTurret.GetComponent<MissileTurret>();
      SCRIPT_missileTurret.fsm(MissileTurret.EventFSM.TakingDamage);
      
      // if (SCRIPT_missileTurret._state != MissileTurret.StateFSM.Destroyed) {
      //   SCRIPT_missileTurret.Damage();
      // }

      // взрыв пули
      clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
      Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек
      Destroy(gameObject); // убиваем пулю игрока
    }

    // Пуля попала в Турель
    if (other.gameObject.CompareTag("Enemy")) {
      Debug.Log("ПУЛЯ ПОПАЛА!!!!!!!");
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

    // Пуля попала в Робота
    if (other.gameObject.CompareTag("Enemy_2")) {

      // получаем родителя, то-есть самого робота
      robot_scout_SCRIPT = other.gameObject.GetComponentInParent<robot_scout>();
      robot_scout_SCRIPT.Damage();


      // взрыв пули
      clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
      Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

      Destroy(gameObject); // убиваем пулю игрока
    }

    // Пуля попала в Робота
    if (other.gameObject.CompareTag("Enemy_battery")) {

      // получаем родителя родителя родителя
      GameObject energy_battery;
      energy_battery = other.gameObject.transform.parent.gameObject;
      energy_battery = energy_battery.transform.parent.gameObject;
      energy_battery = energy_battery.transform.parent.gameObject;
      SCRIPT_EnergyBattery = energy_battery.GetComponent<EnergyBattery>();
      SCRIPT_EnergyBattery.Damage();
      Debug.Log("----Damage>");

      // взрыв пули
      clone_Explosion_Bullet = Instantiate(Explosion_Bullet, transform.position, transform.rotation);
      Destroy(clone_Explosion_Bullet, 2f); // уничтожение через 2 сек

      Destroy(gameObject); // убиваем пулю игрока
    }
  }
}
