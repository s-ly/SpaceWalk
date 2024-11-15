using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
  player scriptPlayer;
  float mouseSensitivity = 200f; // чувствительность мыши
  float xRotation = 0f;
  Transform transPlayer;
  Transform transCamera;
  bool CursorModeLock = false; // блокировка курсора

  bool control = true; // временное отключение управления

  public float mouseX;
  public float mouseY;

  // Start is called before the first frame update
  void Start() {
    Init();
  }

  // Update is called once per frame
  void Update() {
    StopControl();
    if (control) Control();
  }

  void Control() {
    // Получаем ввод мыши
     mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
     mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY; // Поворот камеры по оси X (вверх-вниз)

    // Ограничиваем поворот камеры, чтобы она не могла повернуться слишком далеко
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    // Поворот игрока по оси Y (влево-вправо)
    transPlayer.Rotate(Vector3.up * mouseX);

    transCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // поворот камеры



    if (Input.GetKeyDown(KeyCode.C)) {
      if (!CursorModeLock) {
        Cursor.lockState = CursorLockMode.Locked;
        CursorModeLock = true;
      }
      else {
        Cursor.lockState = CursorLockMode.None;
        CursorModeLock = false;
      }
    }
  }

  void Init() {
    scriptPlayer = GetComponent<player>();
    transPlayer = GetComponent<Transform>();
    GameObject foundObject = GameObject.FindWithTag("MainCamera");
    transCamera = foundObject.transform;
  }

  void StopControl() {
    if (Input.GetKeyDown(KeyCode.X)) {
      control = !control;
    }
  }
}
