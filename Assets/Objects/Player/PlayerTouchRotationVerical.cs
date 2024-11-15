using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRotationVerical : MonoBehaviour {
  float rotationSpeed = 30f; // Скорость поворота (градусов в секунду)
  public Transform Camera; // Ссылка на основную камеру
  Vector2 initialTouchPosition; // Начальная позиция касания

  public bool isRotating = false; // Флаг, указывающий, происходит ли поворот
  public float currentRotationY = 0f; // Текущий угол поворота по оси Y

  // Start is called before the first frame update
  void Start() {
currentRotationY = Camera.transform.rotation.y;
  }

  // Update is called once per frame
  void Update() {
    // Проверяем, есть ли касания
    if (Input.touchCount > 0) {
      foreach (Touch touch in Input.touches) {
        // Проверяем, началось ли касание и находится ли оно в правой половине экрана
        if (touch.phase == TouchPhase.Began && touch.position.x >= Screen.width / 2) {
          isRotating = true;

          // Запоминаем начальную позицию касания
          initialTouchPosition = touch.position;
          // Выводим сообщение в консоль с координатами начала касания
          Debug.Log("Пользователь коснулся правой части экрана в точке: " + initialTouchPosition);
        }

        // Проверяем, перемещается ли палец и происходит ли поворот
        if (touch.phase == TouchPhase.Moved && isRotating && touch.position.x >= Screen.width / 2) {
          // Вычисляем смещение касания
          Vector2 touchDelta = touch.position - initialTouchPosition;

          // Вычисляем угол поворота на основе смещения касания
          float rotationAngleY = touchDelta.y * rotationSpeed * Time.deltaTime;

          // Обновляем текущий угол поворота
          currentRotationY += rotationAngleY;

          // Поворачиваем игрока и камеру
          RotatePlayerAndCamera(currentRotationY);

          // Обновляем начальную позицию касания
          initialTouchPosition = touch.position;
        }

        // палец убран
        if (touch.phase == TouchPhase.Ended && touch.position.x >= Screen.width / 2) {
          isRotating = false;
        }
      }
    }
  }

  // Метод для поворота игрока и камеры
  void RotatePlayerAndCamera(float angleY) {
    // Поворачиваем камеру
    Camera.localRotation = Quaternion.Euler(-angleY, 0f, 0f);
  }
}