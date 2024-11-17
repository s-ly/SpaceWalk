using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
  bool isDebug = false;
  player scriptPlayer;
  float mouseSensitivity = 200f; // чувствительность мыши
  float slowDown = 1f; // замедление чувствительность мыши
  float xRotation = 0f;
  Transform transPlayer;
  Transform transCamera;
  public bool CursorModeLock = false; // блокировка курсора

  bool control = true; // временное отключение управления

  public float mouseX;
  public float mouseY;

  [SerializeField] GameObject aimPlayer; // прицел в UI

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
    mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * slowDown * Time.deltaTime;
    mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * slowDown * Time.deltaTime;

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

  // закрывает/возвращает курсор
  // и замедляет чувствительность обхора
  public void MouseCursorLock(bool flag) {
    if (flag) {
      Cursor.lockState = CursorLockMode.Locked;
      CursorModeLock = true;
      slowDown = 1f;
      aimPlayer.SetActive(true);
    }
    else {
      Cursor.lockState = CursorLockMode.None;
      CursorModeLock = false;
      slowDown = 0.2f;
      aimPlayer.SetActive(false);
    }
    if (isDebug) Debug.Log("MouseCursorLock " + flag);
  }
}
