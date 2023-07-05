using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Вращение антены радара
public class Radar_antenna_rot : MonoBehaviour
{
    float speed_rot = 45f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, speed_rot * Time.deltaTime, 0.0f, Space.Self);
    }
}
