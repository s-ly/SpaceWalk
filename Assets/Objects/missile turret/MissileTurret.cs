using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.InteropServices;

public class MissileTurret : MonoBehaviour {
  [SerializeField] TextMeshProUGUI textDebug;
  [SerializeField] TextMeshProUGUI UIHealth;
  [SerializeField] GameObject UIPoint;
  [SerializeField] bool GizmosOn = false;
  [SerializeField] GameObject mesh_Active;
  [SerializeField] GameObject mesh_Destroy;
  [SerializeField] GameObject _canvas;
  [SerializeField] GameObject CanvasHealth;
  [SerializeField] GameObject _explosion; // взрыв
  [SerializeField] GameObject Bullet; //префаб пули
  [SerializeField] GameObject GeneratorRocket; //префаб пули
  GameObject cloneRocket; // объект новой ракеты


  // работа с дистанцией
  DistanceDetectorGlobal SCRIPT_dist;
  bool distanceFlag = false;

  Camera mainCamera;
  bool recharge = false; // процесс перезарядки
  bool charged = false; // заряжен

  ///////////////////// КОЛХОЗ!!!
  // мы не должны перекладывать ответсьвеннойть на переход игрока в режим боя,
  // но пока так.
  GameObject player;
  GameObject mesh_player;
  player SCRIPT_player;
  Animator animator_player;

  // состояния Ракетной турели
  public enum StateFSM {
    Start,
    Active,
    Attack,
    Destroyed
  }

  // события Ракетной ткрели
  public enum EventFSM {
    Default,
    PlayerEnter,
    PlayerExit,
    EnemyEnter,
    EnemyExit,
    DistanceTrue,
    DistanceFalse,
    TakingDamage,
    Explosion
  }

  GameObject _camera;
  GameObject _mssileTurret_tower;
  GameObject _mssileTurret_trunk;
  public StateFSM _state;
  int _healthl = 100; // здоровье турели
  float _speed_rot = 1.5f; // поворот башни
  TextMeshProUGUI _text_canvas;
  // GameObject _text;
  // public EventFSM _event;

  // Start is called before the first frame update
  void Start() {
    mainCamera = Camera.main; // Получаем основную камеру
    _camera = GameObject.FindGameObjectWithTag("MainCamera"); // ссылка на камеру

    Transform _mssileTurret_tower_TRANS = mesh_Active.transform.GetChild(0);
    _mssileTurret_tower = _mssileTurret_tower_TRANS.gameObject;

    // init trunk and rot x -0.01
    Transform _mssileTurret_trunk_TRANS = _mssileTurret_tower.transform.GetChild(0);
    _mssileTurret_trunk = _mssileTurret_trunk_TRANS.gameObject;
    // Vector3 rotTrunk = _mssileTurret_trunk.transform.eulerAngles;
    // rotTrunk.x = -0.01f;
    // _mssileTurret_trunk.transform.rotation = Quaternion.Euler(rotTrunk);


    SCRIPT_dist = transform.GetComponent<DistanceDetectorGlobal>();
    // _text_canvas = _canvas.GetComponentInChildren<>
    _text_canvas = _canvas.GetComponentInChildren<TextMeshProUGUI>();
    init_player_source();
    fsm(EventFSM.Default);
    CanvasUpdate(); _explosion.SetActive(false); // взрыв пока не нужен
    CanvasHealth.SetActive(false);
  }

  // Update is called once per frame
  void Update() {
    CheckDistance();
    CanvasLookAt();
    DirectionTower();
    DirectionTrunkOn();
    UIPosition();
    AttackActivator();
  }

  void CanvasUpdate() {
    _text_canvas.text = _healthl.ToString();
    UIHealth.text = _healthl.ToString();
  }

  // урон
  public void Damage() {
    if (_healthl > 0) { _healthl -= 10; }
    CanvasUpdate();
    if (_healthl <= 0) {
      fsm(EventFSM.Explosion);
    }
  }

  void UIPosition() {
    Vector3 worldPosition = UIPoint.transform.position; // Получаем позицию точки привязки в мировых координатах
    Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition); // Преобразуем мировые координаты в экранные
    UIHealth.transform.position = screenPosition; // Устанавливаем позицию текста на экране
  }


  // проверяет дистанцию до игрока, не само растояние а готовое состояние
  // и вызывает соответствующее событие
  void CheckDistance() {
    if (SCRIPT_dist.distanceFlag && !distanceFlag) {
      distanceFlag = true;
      fsm(EventFSM.DistanceTrue);
    } else if (!SCRIPT_dist.distanceFlag && distanceFlag) {
      distanceFlag = false;
      fsm(EventFSM.DistanceFalse);
    }
  }

  void OnDrawGizmos() {
    if (GizmosOn) {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, 2f);
    }
  }

  // выравнивает холст робота по камере
  private void CanvasLookAt() {
    _canvas.transform.LookAt(_camera.transform);
    float y = _canvas.transform.localEulerAngles.y;
    _canvas.transform.localEulerAngles = new Vector3(0, y, 0);
  }

  void AttackActivator() {
    if (_state == StateFSM.Active) {
      if (charged && !recharge) {
        Attack();
        charged = false;
      }
      if (!charged && !recharge) {
        StartCoroutine(TimerCoroutine(0.5f));
      }
    }
  }


  // таймер перезарядки
  IEnumerator TimerCoroutine(float delay) {
    recharge = true; // началась перезарядка    
    yield return new WaitForSeconds(delay); // Ждем заданное время
    recharge = false; // перезарядка завершилась
    charged = true;
  }

  void Attack() {
    Debug.Log("РАКЕТНАЯ АТАКА");
    cloneRocket = Instantiate(Bullet, GeneratorRocket.transform.position, GeneratorRocket.transform.rotation);
    Destroy(cloneRocket, 10f);
  }


  // в зону что-то входит
  // private void OnTriggerEnter(Collider other) {
  //   if (other.gameObject.CompareTag("Player")) {
  //     fsm(EventFSM.PlayerEnter);
  //   }
  //   else if (other.gameObject.CompareTag("Enemy")) {
  //     fsm(EventFSM.EnemyEnter);
  //   }
  // }

  // // из зоны что-то выходит
  // private void OnTriggerExit(Collider other) {
  //   if (other.gameObject.CompareTag("Player")) {
  //     fsm(EventFSM.PlayerExit);
  //   }
  //   else if (other.gameObject.CompareTag("Enemy")) {
  //     fsm(EventFSM.EnemyExit);
  //   }
  // }

  public void fsm(EventFSM eventFSM) {
    switch (eventFSM) {
      case EventFSM.Default:
        if (_state != StateFSM.Destroyed) {
          _state = StateFSM.Start;
          mesh_Active.SetActive(true);
          mesh_Destroy.SetActive(false);
        }
        break;
      case EventFSM.DistanceTrue:
        if (_state != StateFSM.Destroyed) {
          _state = StateFSM.Active;
          CanvasHealth.SetActive(true);
          combat_mode_player(true);
        }
        break;
      case EventFSM.DistanceFalse:
        if (_state != StateFSM.Destroyed) {
          _state = StateFSM.Start;
          CanvasHealth.SetActive(false);
          combat_mode_player(false);
        }
        break;
      case EventFSM.EnemyEnter:
        if (_state != StateFSM.Destroyed) {
          _state = StateFSM.Destroyed;
          mesh_Active.SetActive(false);
          mesh_Destroy.SetActive(true);
        }
        break;
      case EventFSM.TakingDamage:
        if (_state != StateFSM.Destroyed) {
          Damage();
        }
        break;
      case EventFSM.Explosion:
        if (_state != StateFSM.Destroyed) {
          mesh_Active.SetActive(false);
          mesh_Destroy.SetActive(true);
          _canvas.SetActive(false); // старая версия
          CanvasHealth.SetActive(false); //новая
          _state = StateFSM.Destroyed;
          combat_mode_player(false);

          // взрыв
          GameObject explosion_INST = Instantiate(_explosion, transform.position, transform.rotation);
          explosion_INST.SetActive(true);
          Destroy(explosion_INST, 6.0f);
        }
        break;
      default:
        break;
    }
    textDebug.text = _state.ToString();
    Debug.Log(_state);
  }

  // собирает ресурсы игрока, для управления ими,
  // это не правильно, но пока так.
  // например, для перевода игрока в боевой режим.
  void init_player_source() {
    player = GameObject.FindGameObjectWithTag("Player");
    SCRIPT_player = player.GetComponent<player>();
    //MyComponent childComponent = GetComponentInChildren<MyComponent>();
    // animator_player = GetComponentInChildren<Animator>();
    // animator_player = player.GetComponent<Animator>();
    mesh_player = player.transform.Find("astro_035").gameObject;
    animator_player = mesh_player.GetComponent<Animator>();
  }

  // управляет режимом боя игрока
  void combat_mode_player(bool active) {
    SCRIPT_player.PlayerModeAttack = active;
    animator_player.SetBool("Attack_mode", active);
  }

  // поворот башни к игроку
  // 1 - нормальный вектор направление к игроку
  // 2 - кватернион, поворачивающий башню по вектору кигроку
  // 3 - новый кватернион поворота башни к игроку
  // 4 - плавно поворачиваем
  void DirectionTower() {
    if (_state == StateFSM.Active) {
      float x = _mssileTurret_tower.transform.localEulerAngles.x;
      float z = _mssileTurret_tower.transform.localEulerAngles.z;
      Vector3 pos = (player.transform.position - _mssileTurret_tower.transform.position).normalized; // 1
      Quaternion rot = Quaternion.FromToRotation(_mssileTurret_tower.transform.forward, pos); //2      
      Quaternion new_rot = rot * _mssileTurret_tower.transform.rotation; //3      
      _mssileTurret_tower.transform.rotation = Quaternion.Lerp(_mssileTurret_tower.transform.rotation, new_rot, _speed_rot * Time.deltaTime); //4      
      float y = _mssileTurret_tower.transform.localEulerAngles.y;
      _mssileTurret_tower.transform.localEulerAngles = new Vector3(x, y, z);
    }
  }

  // поворот ствола
  void DirectionTrunkOn() {
    if (_state == StateFSM.Active) {
      float speed = 30f;
      float angle_stop = -50f;

      float currentAngleX = _mssileTurret_trunk.transform.localEulerAngles.x;
      // Нормализуем угол в диапазоне от -180 до 180 градусов
      float normalizedAngleX = currentAngleX > 180 ? currentAngleX - 360 : currentAngleX;
      if (normalizedAngleX > angle_stop) {
        _mssileTurret_trunk.transform.Rotate(-speed * Time.deltaTime, 0, 0, Space.Self);
        // Debug.Log(normalizedAngleX);
      }
    }
    if (_state == StateFSM.Start) {
      float speed = 30f;
      float angle_stop = 0f;

      float currentAngleX = _mssileTurret_trunk.transform.localEulerAngles.x;
      // Нормализуем угол в диапазоне от -180 до 180 градусов
      float normalizedAngleX = currentAngleX > 180 ? currentAngleX - 360 : currentAngleX;
      if (normalizedAngleX < angle_stop) {
        _mssileTurret_trunk.transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
        // Debug.Log(normalizedAngleX);
      }
    }
  }
}
