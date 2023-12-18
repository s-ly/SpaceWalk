using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Renderer>().enabled = false;
    }
    
    IEnumerator FieldActiveTime() {
        yield return new WaitForSeconds(0.15f);
        transform.GetComponent<Renderer>().enabled = false;
    }

    public void FieldActive() {
        transform.GetComponent<Renderer>().enabled = true;
        StartCoroutine(FieldActiveTime());        
    }
}
