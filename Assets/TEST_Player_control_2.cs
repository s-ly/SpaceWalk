using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TEST_Player_control_2 : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 moveVelocity;
    private Vector3 gravVelocity;
    private Vector3 turnvelocity;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform Target;
    
    void Update()
    {

        Quaternion rot = Quaternion.FromToRotation(-transform.up, Target.position - transform.position);
        transform.rotation = rot * transform.rotation;

        gravVelocity = transform.up * -1f;
        if (!controller.isGrounded)
        {
            //controller.Move(gravVelocity * Time.deltaTime);
        }
        
        


        var hImput = Input.GetAxis("Horizontal");
        var vImput = Input.GetAxis("Vertical");

        moveVelocity = transform.forward * speed * vImput;
        
        turnvelocity = transform.up * speed*100f * hImput;



        //controller.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(transform.up, speed * 100f * hImput * Time.deltaTime);
        //transform.localRotation.Equals(transform.localRotation);

        //Vector3 dir = Target.transform.position - transform.position;


        //float y = transform.localEulerAngles.y; // берём локальную ось Y
        //transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);



        //moveVelocity = transform.up * -11.5f;
        controller.Move(moveVelocity * Time.deltaTime);





        //float x = transform.localEulerAngles.x; // берём локальную ось Y
        //float y = transform.localEulerAngles.y; // берём локальную ось Y
        //float z = transform.localEulerAngles.z; // берём локальную ось Y

        //transform.LookAt(Target);

        //Vector3 dir = Target.transform.position - transform.position;
        //Quaternion rot = Quaternion.LookRotation(dir);
        //transform.rotation = rot;

        //transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);
        //transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, z); // обнуляем все повороты кроме оси Y
        //transform.localRotation = Quaternion.Euler(360.0f, 0.0f, 0.0f);
    }
    private void FixedUpdate()
    {
        
    }
}
