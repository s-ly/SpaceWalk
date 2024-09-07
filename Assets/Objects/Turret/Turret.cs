// Турель.

using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
  [SerializeField] private Transform turret; // цель поворота
  [SerializeField] private float speedTurretRot; // скорость поворота
  [SerializeField] private GameObject Bullet; //префаб пули
  [SerializeField] private float TimeFire; // время между выстрелами
  [SerializeField] GameObject turretCanvas;
  [SerializeField] private int PauseRestartTurretSecond;
  [SerializeField] private GameObject ButtonFirePlayer; // кнопка стрельбы игрока

  // тех-контейнер
  [SerializeField] private Transform GENERATE_technical_container; // место появления тех-контейнера
  [SerializeField] private Transform GENERATE_2;
  [SerializeField] private Transform GENERATE_3;
  [SerializeField] private Transform GENERATE_4;
  [SerializeField] private GameObject PREFAB_technical_container;  // префаб тех-контейнера
  [SerializeField] private GameObject PREFAB_fuel_balon;
  [SerializeField] private GameObject PREFAB_oxy_balon;
  [SerializeField] private GameObject PREFAB_energy_container;
  [SerializeField] private GameObject PREFAB_aid_container;
  private GameObject CLONE_technical_container;  // клон тех-контейнера
  private GameObject CLONE_fuel_balon;
  private GameObject CLONE_oxy_balon;
  private GameObject CLONE_energy_container;
  private GameObject CLONE_aid_container;

  [SerializeField] private int healthTurret; // здоровье турели
  private int healhTurretTEMP;

  private bool LookAtTurret = false; // активарована ли турель
  private bool LiveTurret = true; // жива ли турель
  private GameObject turretTower; // башня турели
  private GameObject turretBase; // подставка турели
  private GameObject pivotRadiousTurret; //родитель для объекта на котором скрипт
  private GameObject turretCanvasText;
  private GameObject repairBase; // ремонтная база

  // эфекты двигателей
  private GameObject EngineVFX_0;
  private GameObject EngineVFX_1;
  private GameObject EngineVFX_2;
  private GameObject EngineVFX_3;

  private GameObject clone; // объект пули
  private float TimeFireTemp;
  private GameObject Player;
  //private GameObject PlayerPivot;
  [SerializeField] private Animator animatorPlayer;
  private Animator animatorRepairBase;
  private player script_player;
  private GameObject myCamera;

  private GameObject Explosion; // взрыв 
  private GameObject Exploded_turret_base; // exploded_turret_base
  private GameObject Exploded_turret_tower; // exploded_turret_tower

  private GameObject PlayerMesh; // меш игрока и на нём аниматор
  private bool FLAG_generate_technical_Container = true; // можно ли генерировать тех-к. (против дублирования)


  // Start is called before the first frame update
  void Start() {
    ButtonFirePlayer.SetActive(false); // отключаю кнопку стрельбы игрока
    turretTower = this.gameObject.transform.GetChild(0).gameObject; // ищем объект башни турели как дочерний
    pivotRadiousTurret = this.gameObject.transform.parent.gameObject; // находим родителя
    turretBase = pivotRadiousTurret.gameObject.transform.GetChild(1).gameObject; // второй ребёнок (подставка)
    repairBase = pivotRadiousTurret.gameObject.transform.GetChild(3).gameObject; // четвёртый ребёнок (ремонтная база)

    // доступ к двигателям ремонтной базы
    EngineVFX_0 = repairBase.gameObject.transform.GetChild(0).gameObject;
    EngineVFX_1 = repairBase.gameObject.transform.GetChild(1).gameObject;
    EngineVFX_2 = repairBase.gameObject.transform.GetChild(2).gameObject;
    EngineVFX_3 = repairBase.gameObject.transform.GetChild(3).gameObject;

    EngineRepairBaseOn(false); // отключаем двигатели ремонтной базы

    // пятый ребёнок (взрыв)
    Explosion = pivotRadiousTurret.gameObject.transform.GetChild(4).gameObject;
    Explosion.SetActive(false);

    // Взорванная тураль, ссылки и деактивация:
    Exploded_turret_base = pivotRadiousTurret.gameObject.transform.GetChild(5).gameObject; //exploded_turret_base
    Exploded_turret_tower = pivotRadiousTurret.gameObject.transform.GetChild(6).gameObject; //exploded_turret_tower
    Exploded_turret_base.GetComponent<Renderer>().enabled = false;
    Exploded_turret_tower.GetComponent<Renderer>().enabled = false;

    animatorRepairBase = repairBase.GetComponent<Animator>();

    healhTurretTEMP = healthTurret; // работаем с TEMP
    turretCanvasText = turretCanvas.transform.GetChild(0).gameObject; // ссылка на текст
    turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();
    turretCanvasText.SetActive(false); // спрятать текст турели

    myCamera = GameObject.FindGameObjectWithTag("MainCamera"); // ссылка на камеру
    TimeFireTemp = TimeFire;
    Player = GameObject.FindGameObjectWithTag("Player");
    //PlayerPivot = GameObject.FindGameObjectWithTag("PlayerPivot");
    //animatorPlayer = Player.GetComponent<Animator>();
    script_player = Player.GetComponent<player>();
  }

  // Update is called once per frame
  void Update() {
    if (LookAtTurret) {
      DirectionTurret(); // если турель активированна то поворачиваем её за игроком
      Fire(); // стреляем
    }
    CanvasTurretLookAt();
  }

  // в зону турели что-то входит
  private void OnTriggerEnter(Collider other) {
    // В зону вошёл игрок и турель жива
    if (other.gameObject.CompareTag("Player") && LiveTurret) {
      LookAtTurret = true;
      Debug.Log("Вижу цель");

      // активация режима игрока (бой)
      animatorPlayer.SetBool("Attack_mode", true);
      script_player.PlayerModeAttack = true;

      turretCanvasText.SetActive(true); // показать текст турели
                                        //ButtonFirePlayer.SetActive(true); // вкыл кнопку стрельбы игрока
    }
  }

  // из зоны турели что-то вышло
  private void OnTriggerExit(Collider other) {
    // В зону вошёл игрок и турель жива
    if (other.gameObject.CompareTag("Player") && LiveTurret) {
      LookAtTurret = false;
      Debug.Log("Потерял цель");
      //GenerateBullet();

      // ДЕактивация режима игрока (бой)
      animatorPlayer.SetBool("Attack_mode", false);
      script_player.PlayerModeAttack = false;

      turretCanvasText.SetActive(false); // спрятать текст турели
                                         //ButtonFirePlayer.SetActive(false); // отключаю кнопку стрельбы игрока
    }
  }

  // в зоне что-то находится
  private void OnTriggerStay(Collider other) {
    // В зону вошёл игрок и турель жива
    if (other.gameObject.CompareTag("Player") && LiveTurret) {
      LookAtTurret = true;
      // активация режима игрока (бой)
      animatorPlayer.SetBool("Attack_mode", true);
      script_player.PlayerModeAttack = true;

      turretCanvasText.SetActive(true); // показать текст турели
                                        //ButtonFirePlayer.SetActive(true); // вкыл кнопку стрельбы игрока
    }
  }

  // поворот турели
  void DirectionTurret() {
    // плавный поворот к цели
    Vector3 direction = turret.transform.position - transform.position;
    Quaternion rotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedTurretRot * Time.deltaTime);

    // поворот только по оси Y
    float y = transform.localEulerAngles.y; // берём локальную ось Y
    float x = transform.localEulerAngles.x; // берём локальную ось Y
    transform.localEulerAngles = new Vector3(x, y, 0); // обнуляем все повороты кроме оси Y
  }

  // Активация и деактивация двигателей ремонтной базы.
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

  // выстрелы
  void Fire() {
    TimeFireTemp = TimeFireTemp - Time.deltaTime;
    if (TimeFireTemp <= 0) {
      GenerateBullet();
      TimeFireTemp = TimeFire;
    }
  }

  // генерация пули
  void GenerateBullet() {
    clone = Instantiate(Bullet, transform.position, transform.rotation);
    //clone.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    Destroy(clone, 10f); // уничтожение через 10 сек
  }

  // генерация тех-контейнера на месте GENERATE_technical_container
  // на самом деле генерирую все подарки
  void Generate_Technical_Container() {
    int random_slot_2 = 0;
    int random_slot_3 = 0;
    int random_slot_4 = 0;
    random_slot_2 = Random.Range(0, 5);
    random_slot_3 = Random.Range(0, 5);
    random_slot_4 = Random.Range(0, 5);

    CLONE_technical_container = Instantiate(
        PREFAB_technical_container,
        GENERATE_technical_container.transform.position,
        GENERATE_technical_container.transform.rotation);
    if (random_slot_2 != 0) {
      if (random_slot_2 == 1) {
        CLONE_fuel_balon = Instantiate(PREFAB_fuel_balon, GENERATE_2.transform.position, GENERATE_2.transform.rotation);
      }
      if (random_slot_2 == 2) {
        CLONE_fuel_balon = Instantiate(PREFAB_oxy_balon, GENERATE_2.transform.position, GENERATE_2.transform.rotation);
      }
      if (random_slot_2 == 3) {
        CLONE_fuel_balon = Instantiate(PREFAB_aid_container, GENERATE_2.transform.position, GENERATE_2.transform.rotation);
      }
      if (random_slot_2 == 4) {
        CLONE_fuel_balon = Instantiate(PREFAB_energy_container, GENERATE_2.transform.position, GENERATE_2.transform.rotation);
      }
    }
    if (random_slot_3 != 0) {
      if (random_slot_3 == 1) {
        CLONE_fuel_balon = Instantiate(PREFAB_fuel_balon, GENERATE_3.transform.position, GENERATE_3.transform.rotation);
      }
      if (random_slot_3 == 2) {
        CLONE_fuel_balon = Instantiate(PREFAB_oxy_balon, GENERATE_3.transform.position, GENERATE_3.transform.rotation);
      }
      if (random_slot_3 == 3) {
        CLONE_fuel_balon = Instantiate(PREFAB_aid_container, GENERATE_3.transform.position, GENERATE_3.transform.rotation);
      }
      if (random_slot_3 == 4) {
        CLONE_fuel_balon = Instantiate(PREFAB_energy_container, GENERATE_3.transform.position, GENERATE_3.transform.rotation);
      }
    }
    if (random_slot_4 != 0) {
      if (random_slot_4 == 1) {
        CLONE_fuel_balon = Instantiate(PREFAB_fuel_balon, GENERATE_4.transform.position, GENERATE_4.transform.rotation);
      }
      if (random_slot_4 == 2) {
        CLONE_fuel_balon = Instantiate(PREFAB_oxy_balon, GENERATE_4.transform.position, GENERATE_4.transform.rotation);
      }
      if (random_slot_4 == 3) {
        CLONE_fuel_balon = Instantiate(PREFAB_aid_container, GENERATE_4.transform.position, GENERATE_4.transform.rotation);
      }
      if (random_slot_4 == 4) {
        CLONE_fuel_balon = Instantiate(PREFAB_energy_container, GENERATE_4.transform.position, GENERATE_4.transform.rotation);
      }
    }
  }

  // Турель получает урон. Вызывается из скрипта пули игрока
  public void TakesDamage() {
    Debug.Log("в башню попадание");
    healhTurretTEMP -= 10;
    turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();
    if (healhTurretTEMP <= 0) {
      TurretDisabled();
    }
  }

  // Турель подорвана
  public void TurretDisabled() {
    Debug.Log("башня подорвана");
    LookAtTurret = false;
    LiveTurret = false;
    // Generate_Technical_Container(); // генерация тех-контейнера
    Explosion.SetActive(true); // взрыв

    // показываем взорванную турель
    Exploded_turret_base.GetComponent<Renderer>().enabled = true;
    Exploded_turret_tower.GetComponent<Renderer>().enabled = true;

    // Отключаем рендер башни и подставки турели
    turretTower.gameObject.GetComponent<MeshRenderer>().enabled = false;
    turretBase.gameObject.GetComponent<MeshRenderer>().enabled = false;

    // ДЕактивация режима игрока (бой)
    animatorPlayer.SetBool("Attack_mode", false);
    script_player.PlayerModeAttack = false;

    turretCanvasText.SetActive(false); // спрятать текст турели
                                       //ButtonFirePlayer.SetActive(false); // отключаю кнопку стрельбы игрока
    if (FLAG_generate_technical_Container) {
      Generate_Technical_Container(); // генерация тех-контейнера
      FLAG_generate_technical_Container = false;
    }


    StartCoroutine(PauseRestartTurret(PauseRestartTurretSecond)); // Перезапуск Турели после паузы
  }

  // выравнивает холст турели по камере
  private void CanvasTurretLookAt() {
    turretCanvas.transform.LookAt(myCamera.transform);
    float y = turretCanvas.transform.localEulerAngles.y;
    turretCanvas.transform.localEulerAngles = new Vector3(0, y, 0);
  }

  // пауза перед рестартом Турели
  IEnumerator PauseRestartTurret(int sec) {

    yield return new WaitForSeconds(sec);
    FLAG_generate_technical_Container = true; // снова можно генерировать тех-к.
    animatorRepairBase.SetTrigger("activateRepairBase");// тригер переключения анимации ремонтной базы

    EngineRepairBaseOn(true); // включаем двигатели ремонтной базы

    // дополнительный флаг - турель сломанна, можно ремонтировать
    // защита от повторныс срабатований
    animatorRepairBase.SetBool("TurretBroken", true);

    // Допорлнительная задержка. Когда турель уже прилетела, но ещё не взлетела,
    // потом турель "оживает".
    int dop_sec = 13;
    yield return new WaitForSeconds(dop_sec);
    RestartTurret();
    yield return new WaitForSeconds(5); // ждём ещё 5 сек перед отключение двигателей
    EngineRepairBaseOn(false); // отключаем двигатели ремонтной базы
  }

  // Рестарт Турели
  private void RestartTurret() {
    LiveTurret = true;
    Explosion.SetActive(false); // отключаем взрыв
    animatorRepairBase.SetBool("TurretBroken", false); // дополнительный флаг - турель восстановлена, ремонтировать нельзя

    // восстанавливаем здоровье и обновляем канвас
    healhTurretTEMP = healthTurret;
    turretCanvasText.GetComponent<TMPro.TextMeshProUGUI>().text = healhTurretTEMP.ToString();

    // Включаем рендер башни и подставки турели
    turretTower.gameObject.GetComponent<MeshRenderer>().enabled = true;
    turretBase.gameObject.GetComponent<MeshRenderer>().enabled = true;

    // Отключаем рендер взорванной турели
    Exploded_turret_base.GetComponent<Renderer>().enabled = false;
    Exploded_turret_tower.GetComponent<Renderer>().enabled = false;
  }
}
