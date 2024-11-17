using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsBase : MonoBehaviour {
  public GameObject Zone;
  public GameObject store;
  public GameManager SRC_GameManager;
  public bool flag_ui_on = true;
  int GameState;
  PlayerControl scriptPlayerControl;
  // Start is called before the first frame update
  void Start() {
    Zone.SetActive(false);
    store.SetActive(false);

    GameObject playerObj = GameObject.FindWithTag("Player");
    scriptPlayerControl = playerObj.GetComponent<PlayerControl>();
  }

  // Update is called once per frame
  void Update() {

  }
  // в зону что-то входит
  private void OnTriggerEnter(Collider other) {
    // В зону вошёл игрок 
    if (other.gameObject.CompareTag("Player") && flag_ui_on) {
      GameState = ProgressManager.Instance.YandexDataOBJ.GameState;
      SRC_GameManager.Check_GameState("Weapons_Base"); // Проверка состояния игры
      if (GameState != 3) {
        Zone.SetActive(true);
        store.SetActive(true);
        scriptPlayerControl.MouseCursorLock(false);
      }
    }
  }
  // из зоны что-то вышло
  private void OnTriggerExit(Collider other) {
    // В зону вошёл игрок 
    if (other.gameObject.CompareTag("Player")) {
      Zone.SetActive(false);
      store.SetActive(false);
      scriptPlayerControl.MouseCursorLock(true);
    }
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.CompareTag("Player") && flag_ui_on) {
      SRC_GameManager.Check_GameState("Weapons_Base"); // Проверка состояния игры
      Zone.SetActive(true);
      store.SetActive(true);
      scriptPlayerControl.MouseCursorLock(false);
    }
  }

  // менят активность Canvas (для исключение перекрытия окон интерфейса)
  public void SwitchActive() {
    if (store.activeSelf && Zone.activeSelf) store.SetActive(false);
    else if (!store.activeSelf && Zone.activeSelf) store.SetActive(true);
  }
  public void Store_on() {
    Zone.SetActive(true);
    store.SetActive(true);
    flag_ui_on = true;
  }
  public void Store_off() {
    Zone.SetActive(false);
    store.SetActive(false);
    flag_ui_on = false;
  }
}
