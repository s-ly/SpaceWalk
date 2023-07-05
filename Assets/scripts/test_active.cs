using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_active : MonoBehaviour
{
    private float activeTime = 3f;
    private bool hide = false;
    //private bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (activeTime > 0 && hide == true)
        {
            activeTime = activeTime - Time.deltaTime;
            Debug.Log(activeTime.ToString());
        }

        if (activeTime <=0 && hide == true)
        {
            //gameObject.SetActive(true);
            GetComponent<MeshRenderer>().enabled = true;
            hide = false;
            activeTime = 3f;
            Debug.Log("hide = false");
        }
        
        //TimeFireTemp = TimeFireTemp - Time.deltaTime;
        //if (TimeFireTemp <= 0)
        //{
        //    GenerateBullet();
        //    TimeFireTemp = TimeFire;
        //}
    }
    // Вызывается когда в тригер объекта что-то попадает.
    //private void OnTriggerEnter(Collider other)
    //{
    //    // в скрипте crystalManager вызываем метод добавление кристалов
    //    //Destroy(gameObject); // уничтожение объекта
    //    //gameObject.SetActive(false);
    //    GetComponent<MeshRenderer>().enabled = false;
    //    hide = true;
    //}

    private void OnTriggerStay(Collider other)
    {
        GetComponent<MeshRenderer>().enabled = false;
        hide = true;
    }
}
