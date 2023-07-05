using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    [SerializeField] Transform _target;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _target.position;
    }
}
