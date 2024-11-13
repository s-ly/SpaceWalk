using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
  Player_Rifle scriptRifle;
  public bool isEnemyLocator = false;
  public float locatorRadius = 28f;
  public bool iSeeEnemy = false;

  // Start is called before the first frame update
  void Start() {
    InIt();
  }

  // Update is called once per frame
  void Update() {
    isEnemyLocator = EnemyLicator();
  }

  void InIt() {
    scriptRifle = GetComponent<Player_Rifle>();
  }

  //получить все коллайдеры, находящиеся в определенной области
  bool EnemyLicator() {
    bool enemyDetect = false;
    // Получаем все коллайдеры в радиусе зоны
    Collider[] colliders = Physics.OverlapSphere(transform.position, locatorRadius);

    // Проверяем, есть ли объекты в зоне
    if (colliders.Length == 0) {
      // Debug.Log("В зоне нет объектов!");
    }
    else {
      // Debug.Log("В зоне находятся объекты: " + colliders.Length);
      // Перебираем все объекты в зоне
      foreach (Collider collider in colliders) {
        // Debug.Log("Объект в зоне: " + collider.name);
        if (collider.CompareTag("Enemy") ||
        collider.CompareTag("Enemy_2") ||
        collider.CompareTag("Enemy_3") ||
        collider.CompareTag("Enemy_battery")) {
          enemyDetect = true;
          Debug.Log("зоне Enemy: " + collider.name);
        }
      }
    }
    return enemyDetect;
  }

  // рисуем локатор
  private void OnDrawGizmosSelected() {
    // Визуализация зоны для удобства
    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(transform.position, locatorRadius);
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.CompareTag("Enemy") ||
        other.gameObject.CompareTag("Enemy_2") ||
        other.gameObject.CompareTag("Enemy_3") ||
        other.gameObject.CompareTag("Enemy_battery")) {
      iSeeEnemy = true;
    }
    if (iSeeEnemy && isEnemyLocator) {
      scriptRifle.enemyDetection = true;
    }
    else {
      iSeeEnemy = false;
      scriptRifle.enemyDetection = false;
    }

  }
  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Enemy") ||
        other.gameObject.CompareTag("Enemy_2") ||
        other.gameObject.CompareTag("Enemy_3") ||
        other.gameObject.CompareTag("Enemy_battery")) {
      iSeeEnemy = false;
      scriptRifle.enemyDetection = false;
    }
  }
}

