using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRotation : MonoBehaviour {
  float rotationSpeed = 1f; // Скорость поворота
  public Transform Camera; // Ссылка на основную камеру
  Vector2 initialTouchPosition; // Начальная позиция касания

  public bool isRotating = false; // Флаг, указывающий, происходит ли поворот
  public float rotationAngleX = 0f;
  public float rotationAngleY = 0f;

  // Start is called before the first frame update
  void Start() {

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
        if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isRotating && touch.position.x >= Screen.width / 2) {
          // Вычисляем смещение касания
          Vector2 touchDelta = touch.position - initialTouchPosition;

          // Вычисляем угол поворота на основе смещения касания
          rotationAngleX = touchDelta.x * rotationSpeed * Time.deltaTime;
          rotationAngleY = touchDelta.y * rotationSpeed * Time.deltaTime;

          // Поворачиваем игрока и камеру
          RotatePlayerAndCamera(rotationAngleX, rotationAngleY);
        }

        // палец убран
        if (touch.phase == TouchPhase.Ended && touch.position.x >= Screen.width / 2) {
          isRotating = false;
          rotationAngleX = 0f;
          rotationAngleY = 0f;
        }
      }
    }
  }

  // Метод для поворота игрока и камеры
  void RotatePlayerAndCamera(float angleX, float angleY) {
    // Поворачиваем игрока
    transform.Rotate(Vector3.up, angleX);

    // Поворачиваем камеру
    Camera.Rotate(Vector3.left, angleY);
  }
}