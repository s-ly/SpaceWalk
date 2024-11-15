using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchRotation : MonoBehaviour {
  [SerializeField] Transform camera;
  float sensitivity = 200f; // чувствительность
  private Vector2 initialTouchPosition; // Начальная позиция касания
  private bool isTouch = false;
  public float mouseX; // Значение для оси X
  public float mouseY; // Значение для оси Y

  // Update is called once per frame
  void Update() {
    Control();
  }

  void Control() {
    if (Input.touchCount > 0) {
      foreach (Touch touch in Input.touches) {
        if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2) {
          isTouch = true;
          initialTouchPosition = NormalizeTouchPosition(touch.position);
        }

        if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isTouch && touch.position.x > Screen.width / 2) {
          Vector2 touchDelta = NormalizeTouchPosition(touch.position) - initialTouchPosition;
          MakeForce(touchDelta);
          initialTouchPosition = NormalizeTouchPosition(touch.position);
        }

        if (touch.phase == TouchPhase.Ended) {
          isTouch = false;
          mouseX = 0;
          mouseY = 0;
        }
      }
    }
  }

  // создание конечной силы воздействия
  void MakeForce(Vector2 Delta) {
    // Вычисляем силу воздействия с учетом чувствительности
    mouseX = Delta.x * sensitivity;
    mouseY = Delta.y * sensitivity;

    transform.Rotate(Vector3.up * mouseX);

    mouseY = Mathf.Clamp(mouseY, -90f, 90f);
    // camera.localRotation =  Quaternion.Euler(mouseY, 0f, 0f); // поворот камеры
    camera.Rotate(Vector3.left, mouseY);
  }

    Vector2 NormalizeTouchPosition(Vector2 touchPosition) {
    float normalizedX = touchPosition.x / Screen.width;
    float normalizedY = touchPosition.y / Screen.height;
    return new Vector2(normalizedX, normalizedY);
  }
}