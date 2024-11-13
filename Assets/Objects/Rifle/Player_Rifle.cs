// Управляет выстрелами игрока

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rifle : MonoBehaviour {
  [SerializeField] private GameObject playerBullet; //префаб пули игрока

  // Пауза выстрела (время между выстрелами винтовки игрока).
  private float time_shot_pause; // Получим из DATA хранилища
  private float TEMP_time_shot_pause; // Для уменьшения в счётчике

  private GameObject clonePlayerBullet; // объект пули игрока
  private bool TimeFireFlag = true;

  // Враг в прицеле.
  // У игрока есть тригерный колаидр, если в него попадает враг,
  // то можно стрелять автоматически
  public bool lookOnEnemy = false;

  //player;
  public GameObject player;
  public player scriptPlayer;
  public Animator animatorPlayer;

  public bool enemyDetection = false; // управляется из вне

  void Init() {
    player = GameObject.FindGameObjectWithTag("Player");
    scriptPlayer = player.GetComponent<player>();
    GameObject playerAnimatorObj = GameObject.FindGameObjectWithTag("PlayerAnimator");
    animatorPlayer = playerAnimatorObj.GetComponent<Animator>();
  }

  // Start is called before the first frame update
  void Start() {
    Init();
    UpdateTimeFireTemp();
  }

  // Update is called once per frame
  void Update() {
    isEnemyDetection();
    FirePeriodPause();
  }

  public void UpdateTimeFireTemp() {
    time_shot_pause = ProgressManager.Instance.YandexDataOBJ.DATA_time_shot_pause;
    TEMP_time_shot_pause = time_shot_pause;
  }

  /* Таймер перезарядки. Запускаем каждый кадр.
  Как только время истекло, то флаг TimeFireFlag = true.
  Временный (убывающий) счётчик сбрасываем. */
  void FirePeriodPause() {
    if (TimeFireFlag == false) {
      TEMP_time_shot_pause -= Time.deltaTime;
      if (TEMP_time_shot_pause <= 0) {
        TimeFireFlag = true;
        TEMP_time_shot_pause = time_shot_pause;
      }
    }
  }

  // стреляет если оружее как-бы перезарядилось
  // и в прицеле есть враг
  public void FirePlayer() {
    if (TimeFireFlag && lookOnEnemy) {
      GenerateBulletPlayer();
      TimeFireFlag = false;
    }
  }

  // генерация пули
  public void GenerateBulletPlayer() {
    clonePlayerBullet = Instantiate(playerBullet, transform.position, transform.rotation);
    Destroy(clonePlayerBullet, 10f); // уничтожение через 10 сек
  }

  // Действие, если есть враги
  // принимает данные от внешнего скрипиа на Rifle
  // изначально принимаем что врагов нет (на случай если враг не вышел а был уничтожен)
  // пока управляет анимацией тгрока
  void isEnemyDetection() {
    if (enemyDetection) {
      lookOnEnemy = true; // враг в прицеле появился
      Debug.Log("Вижу врага");
      scriptPlayer.PlayerModeAttack = true;
      animatorPlayer.SetBool("Attack_mode", true); 
    }
    else {
      lookOnEnemy = false; // враг вышел из прицела
      Debug.Log("ВРАГ не в поле!!!!!!");
      scriptPlayer.PlayerModeAttack = false;
      animatorPlayer.SetBool("Attack_mode", false); 
    }
  }
}
