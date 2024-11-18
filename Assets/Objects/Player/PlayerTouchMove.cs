// Управляет перемещение игрока с помощью сенсора

// TODO
// Нужно подумать о том, что раз укран горизонтальный
// то перемещение по горизонтали занимает в два раза больше путь
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMove : MonoBehaviour {
  Vector2 initialTouchPosition; // Начальная позиция касания
  float sensitivity = 25f; // чувствительность
  float playerSpeed = 22f;

  public bool isTouch = false;
  public float forceSide = 0;
  public float forceFront = 0;
  int touchId = -1;

  Rigidbody rigPlayer;
  player scriptPlayer;

  // Start is called before the first frame update
  void Start() {
    Init();
  }

  void FixedUpdate() {
    AddPlayerForce();
  }

  // Update is called once per frame
  void Update() {
    // Проверяем, есть ли касания
    if (Input.touchCount > 0) {




      foreach (Touch touch in Input.touches) {
        // Проверяем, началось ли касание и находится ли оно в левой половине экрана
        if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2) {
          isTouch = true;
          touchId = touch.fingerId;

          // Запоминаем начальную позицию касания
          initialTouchPosition = NormalizeTouchPosition(touch.position);
          // Выводим сообщение в консоль с координатами начала касания
          // Debug.Log("Пользователь коснулся экрана в точке: " + initialTouchPosition);
        }

        // Проверяем, перемещается ли палец и isTouch
        if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isTouch && touch.position.x < Screen.width / 2 && touch.fingerId == touchId) {
          // Нормализуем текущую позицию касания
          Vector2 normalizedPosition = NormalizeTouchPosition(touch.position);

          // Вычисляем абсолютное смещение относительно начальной позиции касания
          Vector2 absoluteDelta = normalizedPosition - initialTouchPosition;

          // Выводим сообщение в консоль с координатами текущего положения и абсолютным смещением
          // Debug.Log("Абсолютное смещение: " + absoluteDelta + ". Текущая нормализованная позиция: " + normalizedPosition);

          MakeForce(absoluteDelta);
          PlayerAnimation(true);
        }

        // палец убран
        if (touch.phase == TouchPhase.Ended && touch.position.x < Screen.width / 2 && touch.fingerId == touchId) {
          forceSide = 0;
          forceFront = 0;
          isTouch = false;
          PlayerAnimation(false);
        }
      }
    }
  }

  // Метод для нормализации координат касания
  Vector2 NormalizeTouchPosition(Vector2 touchPosition) {
    float normalizedX = touchPosition.x / Screen.width;
    float normalizedY = touchPosition.y / Screen.height;
    return new Vector2(normalizedX, normalizedY);
  }

  // создание конечной силы воздействия
  void MakeForce(Vector2 Delta) {
    // Вычисляем силу воздействия с учетом чувствительности
    forceSide = Delta.x * sensitivity;
    forceFront = Delta.y * sensitivity;

    // Ограничиваем силу диапазоном от -1 до 1
    forceSide = Mathf.Clamp(forceSide, -1f, 1f);
    forceFront = Mathf.Clamp(forceFront, -1f, 1f);
  }

  void Init() {
    rigPlayer = GetComponent<Rigidbody>();
    scriptPlayer = GetComponent<player>();
  }

  void AddPlayerForce() {
    if (isTouch) {
      rigPlayer.AddForce(transform.forward * playerSpeed * forceFront);
      rigPlayer.AddForce(transform.right * playerSpeed * forceSide);
    }
  }

  void PlayerAnimation(bool on) {
    if (on) {
      scriptPlayer.animator.SetBool("run", true);
    }
    else {
      scriptPlayer.animator.SetBool("run", false);
    }
  }
}





/*

// Проверяем, есть ли касания
        if (Input.touchCount > 0)
        {
            // Выводим количество касаний в консоль
            Debug.Log("Количество касаний: " + Input.touchCount);

            // Перебираем все касания
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Выводим информацию о каждом касании
                Debug.Log("Касание " + (i + 1) + ": " + touch.position);

                // Проверяем фазу касания
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Пользователь коснулся экрана в точке: " + touch.position);
                }
            }
        }

*/