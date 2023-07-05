// Следование камеры за персонажем

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    [SerializeField] Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Вызывается после всех опдэйтов
    void LateUpdate()
    {
        transform.position = _target.position;
    }
}
