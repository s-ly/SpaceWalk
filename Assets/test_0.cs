using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ���� ������ � ��������� � ��� ����� ���������.
public class test_0 : MonoBehaviour
{

    [SerializeField] private float _speed;
    private float _oldMousePositionX; // ������ ������� ����
    private float _eulerY; // �������
    void Update()
    {
        
        // � ������ ������� ��������� ������� �������� ����
        if (Input.GetMouseButtonDown(0))
        {
            _oldMousePositionX = Input.mousePosition.x;
        }
        
        // ����������� �� ��� Z ���������� �� ����� ������ �����.
        // ���� ������ ������ ������ ����.
        if (Input.GetMouseButton(0))
        {
            
            // ������ ����� �������
            Vector3 newPosition = transform.position + transform.forward * Time.deltaTime * _speed;

            // ��������
            newPosition.x = Mathf.Clamp(newPosition.x, -3f, 3f);

            transform.position = newPosition;


            // ��������� ������� ����. �� �������� �������� ������.
            float deltaX = Input.mousePosition.x - _oldMousePositionX;

            _oldMousePositionX = Input.mousePosition.x;
            _eulerY += deltaX;

            // ����������� ��������� ��������
            // �� -70 �� 70
            _eulerY = Mathf.Clamp(_eulerY, -70, 70);

            // ������������ ������
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }

        
    }
}
