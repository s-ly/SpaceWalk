using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// Собственный класс, данные для сохранения между уровнями и на Yandex.
// Это нужно для удобной передачи в JavaScript.
[System.Serializable]
public class YandexData
{
    public int Crystal;
    public float Oxygen;
    public int TechnicalContainer;
    public int GameState; // Состояние игры
    public float DATA_time_shot_pause; // Пауза выстрела (время между выстрелами винтовки игрока).
    public float DATA_player_speed; // коэффициент ускорения игрока (прибавка к скорости)
    public int DATA_player_health; // здоровье игрока

    //public string DeviceInfo;
    //public bool TouchKeyboardActive;
}

/* 
Менеджер прогресса, сохраняет данные между уровнями.
Реализует паттерн "Сингл тон". Необходимо убедится что такой объект только один. 
Если он не один то его нужно удалить. Всё это делаем в методе Awake(), 
которы вызывается перед Start(). Этот объект не нужно добавлять в разные сцены.

Состояние игры кодирует этп прохождения игры.
0 - нужно добежать до базы
1 - нужно собрать 10 кристалов и расширить запас кислорода
2 - нужно найти шатл
3 - нужно найти топливную станцию и принести к кораблю топливо
4 - нужно найти энергетическую станцию и принести энергию к кораблю
5 - нужно найти станцию охраны и принести ключ доступа к кораблю
6 - Победа, конец игры
*/
public class ProgressManager : MonoBehaviour
{
    public YandexData YandexDataOBJ; // класс с данными
    public static ProgressManager Instance;

    private void Awake()
    {        
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}