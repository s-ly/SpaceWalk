using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Разные типы данных и обращение к ним через инспектор.
public class test_0 : MonoBehaviour
{

    [SerializeField] private float _speed;
    private float _oldMousePositionX; // старая позиция мыши
    private float _eulerY; // поворот
    void Update()
    {
        
        // в момент нажатия сохраняем текущее значение мыши
        if (Input.GetMouseButtonDown(0))
        {
            _oldMousePositionX = Input.mousePosition.x;
        }
        
        // перемещение по оси Z умноженное на время одного кадра.
        // если зажата правая кнопка мыши.
        if (Input.GetMouseButton(0))
        {
            
            // расчёт новой позиции
            Vector3 newPosition = transform.position + transform.forward * Time.deltaTime * _speed;

            // диапазон
            newPosition.x = Mathf.Clamp(newPosition.x, -3f, 3f);

            transform.position = newPosition;


            // Изменение позиции мыши. Из текущего вычитаем старое.
            float deltaX = Input.mousePosition.x - _oldMousePositionX;

            _oldMousePositionX = Input.mousePosition.x;
            _eulerY += deltaX;

            // ограничение диапазона значений
            // от -70 до 70
            _eulerY = Mathf.Clamp(_eulerY, -70, 70);

            // поворачиваем объект
            transform.eulerAngles = new Vector3(0, _eulerY, 0);
        }

        
    }
}
