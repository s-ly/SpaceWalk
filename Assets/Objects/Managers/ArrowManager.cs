using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Менеджер стрелок, помогает игроку находить цели
public class ArrowManager : MonoBehaviour {
  public GameObject[] _missionsArray;

  // Start is called before the first frame update
  void Start() {
    HideAllArrows();
  }

  // управляет стрелками
  // скорее всего вызывается в GameManager
  public void ArrowControl(int gameState) {
    HideAllArrows();
    _missionsArray[gameState].SetActive(true);
  }

  // скрывает все стрелки
  void HideAllArrows() {
    foreach (GameObject i in _missionsArray) {
      if (i != null) {
        i.SetActive(false);
      }
    }
  }
}