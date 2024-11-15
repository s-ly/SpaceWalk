using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRotationHorizont : MonoBehaviour {
    float rotationSpeed = 30f; // Скорость поворота (градусов в секунду)
    public Transform Camera; // Ссылка на основную камеру
    Vector2 initialTouchPosition; // Начальная позиция касания

    public bool isRotating = false; // Флаг, указывающий, происходит ли поворот
    public float currentRotationX = 0f; // Текущий угол поворота по оси X
    public float tmpRotx = 0;

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
                if (touch.phase == TouchPhase.Moved && isRotating && touch.position.x >= Screen.width / 2) {
                    // Вычисляем смещение касания
                    Vector2 touchDelta = touch.position - initialTouchPosition;

                    // Вычисляем угол поворота на основе смещения касания
                    float rotationAngleX = touchDelta.x * rotationSpeed * Time.deltaTime;

                    // Обновляем текущий угол поворота
                    currentRotationX += rotationAngleX;

                    // Поворачиваем игрока и камеру
                    RotatePlayerAndCamera(currentRotationX);

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
    void RotatePlayerAndCamera(float angleX) {
      float dir = 1f;
      if (angleX > tmpRotx){
        dir = 1f;
        transform.Rotate(Vector3.up * 1f * dir);
        tmpRotx = angleX;
      }else {
        dir = -1f;
      }
        transform.Rotate(Vector3.up * 1f * dir);
        tmpRotx = angleX;

      //       if (angleX > tmpRotx){
      //   transform.Rotate(Vector3.up * 0.2f);
      //   tmpRotx = angleX;
      // }else {
      //   transform.Rotate(Vector3.up * -0.2f);
      //   tmpRotx = angleX;
      // }
      
      // float tmpX = transform.rotation.x;
      // float tmpY = transform.rotation.y;
      //       float tmpXloc = transform.localEulerAngles.x;
      // float tmpYloc = transform.localEulerAngles.y;
        // Поворачиваем игрока
        // transform.rotation = Quaternion.Euler(tmpXloc, angleX, tmpYloc);
        // transform.Rotate(Vector3.up * angleX);
        // transform.Rotate(Vector3.up * 0.2f);
    }
}