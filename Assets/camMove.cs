// ���������� ������ �� ����������

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

    // ���������� ����� ���� ��������
    void LateUpdate()
    {
        transform.position = _target.position;
    }
}
