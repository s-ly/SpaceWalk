using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{

    [SerializeField] private float _height;
    [SerializeField] private int _coin;
    [SerializeField] private string _name;
    [SerializeField] private Color _color;
    [SerializeField] private Vector3 _pos;
    [SerializeField] private bool _alive;
    [SerializeField] private Light _sun;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _ball;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, _height, 1);
        gameObject.name = _name;
        gameObject.GetComponent<Renderer>().material.color = _color;
        transform.position = _pos;
        gameObject.SetActive(_alive);
        _sun.intensity = 2;
        //_sun.color = Color.red;
        _sun.color = _color;
        _camera.fieldOfView = 120;
    }

    // Update is called once per frame
    void Update()
    {
        _ball.position = transform.position + new Vector3(0, 3, 0);
    }
}
