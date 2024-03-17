using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������� ��������� ����� �������� � ������� � ���������� ������.
public class DistanceDetector_shadow : MonoBehaviour {
    public Transform obj;
    GameObject playerObj;
    //public Transform player;
    public float minDistance = 80f;
    // Start is called before the first frame update
    void Start() {
        obj.gameObject.SetActive(false);
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(obj.position, playerObj.transform.position);
        //Debug.Log("=====> obj.position: " + obj.position);
        //Debug.Log("=====> playerObj.transform.position: " + playerObj.transform.position);
        //Debug.Log("Distance: " + distance);
        if ((distance <= minDistance) && (obj.gameObject.activeSelf == false)) obj.gameObject.SetActive(true);
        else if ((distance > minDistance) && (obj.gameObject.activeSelf == true))  obj.gameObject.SetActive(false); 
    }
}
