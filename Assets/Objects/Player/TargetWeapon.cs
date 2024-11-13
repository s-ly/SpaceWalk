using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour {
  Transform transCamera;
  public Transform target;
  public GameObject hitObject; // Объект, на который упал луч
  public Transform weapon; // это оружие нацеливаем
  float rayDist = 35f; // Максимальная дистанция, для Raycast

  // Start is called before the first frame update
  void Start() {
    Init();
  }

  // Update is called once per frame
  void Update() {
    RayWeapon();
    LooAtWeapon();
  }

  void Init() {
    GameObject foundObject = GameObject.FindWithTag("MainCamera");
    transCamera = foundObject.transform;
  }

  void RayWeapon() {
    // рисует лучь для Debug
    Debug.DrawRay(transCamera.position, transCamera.forward * rayDist, Color.red);

    Ray ray = new Ray(transCamera.position, transCamera.forward);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, rayDist)) {
      target.position = hit.point; // ставлю цель
      hitObject = hit.collider.gameObject; // смотрю куда упал луч
    }
    else {
      // Если луч никуда не попал, ставим target на кончик луча
      target.position = transCamera.position + transCamera.forward * rayDist;
      hitObject = null;
    }
  }

  // Нацеливаем weapon на target
  void LooAtWeapon() {
    weapon.LookAt(target);
  }
}
