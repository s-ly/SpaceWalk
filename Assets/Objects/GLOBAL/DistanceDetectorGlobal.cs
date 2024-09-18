using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// глобальный детектор растояния
// определяет дистанцию между объектом и игроком.
// если дистанция пройдена, но меняет свой переключатель.
// по лигике, к distanceFlag нужно обращаться из вне/
public class DistanceDetectorGlobal : MonoBehaviour {
  public bool distanceFlag = false;
  
  // public Transform obj;
  GameObject playerObj;
  [SerializeField] float minDistance = 15f;
  [SerializeField] bool GizmosOn = false;

  // Start is called before the first frame update
  void Start() {
    // obj.gameObject.SetActive(false);
    playerObj = GameObject.FindGameObjectWithTag("Player");
  }

  // Update is called once per frame
  void Update() {
    float distance = Vector3.Distance(transform.position, playerObj.transform.position);
    if ((distance <= minDistance) && (distanceFlag == false)) distanceFlag = true;
    else if ((distance > minDistance) && (distanceFlag == true)) distanceFlag = false;
  }

  void OnDrawGizmos() {
    if (GizmosOn) {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, minDistance);
    }
  }
}
