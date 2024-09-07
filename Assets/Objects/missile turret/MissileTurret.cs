using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissileTurret : MonoBehaviour {
  [SerializeField] TextMeshProUGUI textDebug;

  public enum StateFSM {
    st_1,
    st_2
  }

  public StateFSM _state;

  // Start is called before the first frame update
  void Start() {
    Debug.Log(_state);
  }

  // Update is called once per frame
  void Update() {

  }

  // в зону что-то входит
  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      Debug.Log("OnTriggerEnter");
      fsm();
    }
  }

  // из зоны что-то выходит
  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      Debug.Log("OnTriggerExit");
      fsm();
    }
  }

  private void fsm() {
    if (_state == StateFSM.st_1) {
      _state = StateFSM.st_2;
    }
    else {
      _state = StateFSM.st_1;
    }
    Debug.Log(_state);
    textDebug.text = _state.ToString();
  }
}
