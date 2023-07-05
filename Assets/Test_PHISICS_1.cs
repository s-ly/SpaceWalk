using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PHISICS_1 : MonoBehaviour
{
    private Rigidbody rig;
    private float Speed = 40.0f;
    private float RotationSpeed = 20.0f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddRelativeForce(0f, 1000f, 0f);
        }
    }

    private void FixedUpdate()
    {
        float HorizontalForce = Input.GetAxis("Horizontal") * RotationSpeed;
        float VerticallForce = Input.GetAxis("Vertical") * Speed;

        //rig.AddForce(HorizontalForce, 0f, VerticallForce);
        rig.AddRelativeForce(0f, 0f, VerticallForce);
        rig.AddRelativeTorque(0f, HorizontalForce, 0f);

        //if (Input.GetKey(KeyCode.A))
        //{
        //    rig.AddForce(HorizontalForce, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    rig.AddForce(15f, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rig.AddForce(0f, 0f, 15f);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    rig.AddForce(0f, 0f, -15f);
        //}
    }
}
