using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RulerLineRender : MonoBehaviour
{

    [SerializeField] Transform pointA; // Первая точка
    [SerializeField] Transform pointB; // Вторая точка
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        LineInIt();
    }

    // Update is called once per frame
    void Update()
    {
        // Обновляем позиции точек линии
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);
    }

    void LineInIt()
    {
        // Получаем компонент LineRenderer
        lineRenderer = GetComponent<LineRenderer>();

        // Устанавливаем количество точек линии
        lineRenderer.positionCount = 2;

        // Настраиваем параметры линии
        lineRenderer.startWidth = 0.1f; // Ширина линии в начале
        lineRenderer.endWidth = 0.1f;   // Ширина линии в конце
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Материал линии
        lineRenderer.startColor = Color.red; // Цвет линии в начале
        lineRenderer.endColor = Color.green;   // Цвет линии в конце
    }
}
