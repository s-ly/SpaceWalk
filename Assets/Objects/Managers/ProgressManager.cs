using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.Port;

// Собственный класс, данные для сохранения между уровнями и на Yandex.
// Это нужно для удобной передачи в JavaScript.
[System.Serializable]
public class YandexData {
  public int Crystal;
  public float Oxygen;
  public int TechnicalContainer;
  public int GameState; // Состояние игры
  public float DATA_time_shot_pause; // Пауза выстрела (время между выстрелами винтовки игрока).
  public float DATA_player_speed; // коэффициент ускорения игрока (прибавка к скорости)
  public int DATA_player_health; // здоровье игрока
  public int DATA_fuel;
  public int DATA_battary_level; // защитное поле (уровень)
}

/* 
Менеджер прогресса, сохраняет данные между уровнями.
Реализует паттерн "Сингл тон". Необходимо убедится что такой объект только один. 
Если он не один то его нужно удалить. Всё это делаем в методе Awake(), 
которы вызывается перед Start(). Этот объект не нужно добавлять в разные сцены.

Состояние игры кодирует этп прохождения игры.
0 = "Доберитесь до базы.";
1 = "Собери 10 Кристаллов, увеличь объём кислорода.";
2 = "Кратер с кристаллами (северный кратер).";
3 = "Найди станцию оружия.";
4 = "Найди Шаттл.";
5 = "Найди станцию двигателей.";
6 = "Найти топливную станцию и принести к Шаттлу.";
7 = "Найди станцию поля.";
8 = "Найти энергетическую станцию и принести к Шаттлу.";
9 = "Уничтожить 3 реактора.";
10 = "Найти станцию охраны и принести к Шаттлу.";
11 = "Вы уже выиграли.";
*/
public class ProgressManager : MonoBehaviour {
  public YandexData YandexDataOBJ;
  public static ProgressManager Instance;

  private void Awake() {
    if (Instance == null) {
      transform.parent = null;
      DontDestroyOnLoad(gameObject);
      Instance = this;
    }
    else {
      Destroy(gameObject);
    }
  }
}