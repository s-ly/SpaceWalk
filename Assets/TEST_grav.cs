using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_grav : MonoBehaviour
{
    [SerializeField] private Rigidbody Player;
    [SerializeField] private Transform PlayerTr;
    // Start is called before the first frame update
    void Start()
    {
        
    }    

    private void FixedUpdate()
    {
        Vector3 dir = (transform.position - PlayerTr.transform.position);
        Player.AddForce(dir);
    }
}
