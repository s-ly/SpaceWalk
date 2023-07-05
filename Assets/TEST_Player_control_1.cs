using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Player_control_1 : MonoBehaviour
{
    public float speed = 1;
    public float speed_rotation = 3;
    public CharacterController characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerRotation = new Vector3(0, Input.GetAxis("Horizontal") * speed_rotation , 0);
        Vector3 playerForward = transform.TransformDirection(Vector3.forward);
        transform.Rotate(playerRotation);
        float curSpeed = speed * Input.GetAxis("Vertical") ;
        characterController.SimpleMove(playerForward * curSpeed);
    }
}
