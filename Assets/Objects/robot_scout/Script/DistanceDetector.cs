using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// определяет дистанцию между объектом и игроком и активирует объект.
public class DistanceDetector : MonoBehaviour {
  public Transform obj;
  GameObject playerObj;
  [SerializeField] float minDistance = 80f;
  [SerializeField] bool GizmosOn = false;

  // Start is called before the first frame update
  void Start() {
    obj.gameObject.SetActive(false);
    playerObj = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update() {
    float distance = Vector3.Distance(obj.position, playerObj.transform.position);
    if ((distance <= minDistance) && (obj.gameObject.activeSelf == false)) obj.gameObject.SetActive(true);
    else if ((distance > minDistance) && (obj.gameObject.activeSelf == true)) obj.gameObject.SetActive(false);
  }

  void OnDrawGizmos() {

    // // Устанавливаем цвет линий
    // Gizmos.color = Color.red;
    // // Рисуем линию от текущей позиции объекта до заданной точки
    // Vector3 targetPosition = transform.position + transform.forward * 5f;
    // Gizmos.DrawLine(transform.position, targetPosition);
    // // Рисуем сферу на конце линии
    // // Gizmos.DrawSphere(targetPosition, 0.5f);
    // Gizmos.DrawWireSphere(targetPosition, 0.5f);

    if (GizmosOn) {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(obj.position, minDistance);
    }
  }
}
