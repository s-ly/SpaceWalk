using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTouch : MonoBehaviour {
  // public player scriptPlayer;
  // float mouseSensitivity = 200f; // чувствительность мыши
  // float xRotation = 0f;
  // public Transform transPlayer;
  // Transform transCamera;
  // bool CursorModeLock = false; // блокировка курсора

  float moveSpeed = 22.0f; // Скорость перемещения персонажа
  public Vector2 touchStartPos; // Начальная позиция касания
  public bool isTouching = false; // Флаг, указывающий, касается ли игрок экрана
  public Rigidbody rig;

  // bool control = true; // временное отключение управления

  // Start is called before the first frame update
  void Start() {
    Init();
  }

  // Update is called once per frame
  void FixedUpdate() {

    HandleTouchInput();
    // HandleMouseInput();
  }

  void HandleTouchInput() {
    if (Input.touchCount > 0) {
      Touch touch = Input.GetTouch(0);

      // Проверяем, касается ли игрок левой половины экрана
      if (touch.position.x < Screen.width / 2) {
        switch (touch.phase) {
          case TouchPhase.Began:
            // Запоминаем начальную позицию касания
            touchStartPos = touch.position;
            isTouching = true;
            break;

          case TouchPhase.Moved:
            if (isTouching) {
              // Вычисляем смещение касания
              Vector2 touchDelta = touch.position - touchStartPos;

              // Перемещаем персонажа в зависимости от направления касания
              MoveCharacter(touchDelta);
            }
            break;

          case TouchPhase.Ended:
            // Сбрасываем флаг касания
            isTouching = false;
            break;
        }
      }
    }
  }


  // void HandleMouseInput() {
  //   if (Input.GetMouseButtonDown(0)) {
  //     // Проверяем, касается ли игрок левой половины экрана
  //     if (Input.mousePosition.x < Screen.width / 2) {
  //       // Запоминаем начальную позицию касания
  //       touchStartPos = Input.mousePosition;
  //       isTouching = true;
  //     }
  //   }
  //   else if (Input.GetMouseButton(0)) {
  //     if (isTouching) {
  //       // Вычисляем смещение касания
  //       Vector2 touchDelta = (Vector2)Input.mousePosition - touchStartPos;

  //       // Перемещаем персонажа в зависимости от направления касания
  //       MoveCharacter(touchDelta);
  //     }
  //   }
  //   else if (Input.GetMouseButtonUp(0)) {
  //     // Сбрасываем флаг касания
  //     isTouching = false;
  //     // animator.SetBool("run", false); // Останавливаем анимацию бега
  //   }
  // }

  void MoveCharacter(Vector2 touchDelta) {
    Debug.Log("FIX");

    // Вычисляем направление движения на основе смещения касания
    Vector3 moveDirection = new Vector3(touchDelta.x, 0, touchDelta.y).normalized;

    // Применяем силу к Rigidbody для перемещения персонажа
    Debug.Log("FIX MOVE " + moveDirection);
    Debug.Log("FIX MOVE X " + (moveDirection.x * moveSpeed ));
    rig.AddForce(moveDirection * moveSpeed );
    // rig.AddForce(transform.forward * moveDirection.x * moveSpeed);
  }



  void Init() {
    // scriptPlayer = GetComponent<player>();
    // transPlayer = GetComponent<Transform>();
    // GameObject foundObject = GameObject.FindWithTag("MainCamera");
    // transCamera = foundObject.transform;
    rig = GetComponent<Rigidbody>();
  }
}
