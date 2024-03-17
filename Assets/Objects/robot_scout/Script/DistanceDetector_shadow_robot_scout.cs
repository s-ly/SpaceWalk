using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// определяет дистанцию между объектом и игроком и активирует объект.
public class DistanceDetector_shadow_robot_scout : MonoBehaviour {
    public Transform obj;
    GameObject playerObj;
    public float minDistance = 80f;
    
    // Start is called before the first frame update
    void Start() {
        obj.gameObject.SetActive(false);
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(obj.position, playerObj.transform.position);
        if ((distance <= minDistance) && (obj.gameObject.activeSelf == false)) obj.gameObject.SetActive(true);
        else if ((distance > minDistance) && (obj.gameObject.activeSelf == true))  obj.gameObject.SetActive(false); 
    }
}

