using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RulerLineRender : MonoBehaviour
{

    [SerializeField] Transform pointA; // ������ �����
    [SerializeField] Transform pointB; // ������ �����
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        LineInIt();
    }

    // Update is called once per frame
    void Update()
    {
        // ��������� ������� ����� �����
        lineRenderer.SetPosition(0, pointA.position);
        lineRenderer.SetPosition(1, pointB.position);
    }

    void LineInIt()
    {
        // �������� ��������� LineRenderer
        lineRenderer = GetComponent<LineRenderer>();

        // ������������� ���������� ����� �����
        lineRenderer.positionCount = 2;

        // ����������� ��������� �����
        lineRenderer.startWidth = 0.1f; // ������ ����� � ������
        lineRenderer.endWidth = 0.1f;   // ������ ����� � �����
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �������� �����
        lineRenderer.startColor = Color.red; // ���� ����� � ������
        lineRenderer.endColor = Color.green;   // ���� ����� � �����
    }
}
