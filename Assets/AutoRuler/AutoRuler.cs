using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoRuler : MonoBehaviour
{
    [SerializeField] Player player;
    //[SerializeField] GameObject rulerTarget;
    [SerializeField] Transform QrA;
    [SerializeField] Transform QrB;
    bool slotUsedA = false;
    bool slotUsedB = false;
    public string QrNameA;
    public string QrNameB;
    QRCode QrScriptA;
    QRCode QrScriptB;
    public Collider colliderA;
    public Collider colliderB;
    //QRCode QrAScript;
    //[SerializeField] Transform pointB;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textDist;
    [SerializeField]  Transform pointA;
    [SerializeField]  Transform pointB;
    float rolerDrawHeight = 0.5f;

    // для рисования Ruler
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Настройка LineRenderer
        lineRenderer.positionCount = 2; // Две точки: A и B
        lineRenderer.startWidth = 0.005f; // Ширина линии
        lineRenderer.endWidth = 0.005f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Материал линии
        lineRenderer.startColor = Color.yellow; // Цвет линии
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = false;

        canvas.enabled = false;
        QrA = null;
        QrB = null;
        QrNameA = null;
        QrNameB = null;
        colliderA = null;
        colliderB = null;
    }

    // Update is called once per frame
    void Update()
    {
        checkQR();
        measurement();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = true;
        }

        if (other.CompareTag("QR"))
        {
            QRCode QrScript = other.transform.GetComponentInChildren<QRCode>();
            Collider collider = other.transform.GetComponent<Collider>();
            if (!QrScript.taken && !QrScript.onTable)

            {
                QrScript.onTable = true;
                if (!QrA && !slotUsedA)
                {
                    QrA = other.transform;
                    slotUsedA = true;
                    QrNameA = other.name;
                    QrScriptA = QrScript;
                    colliderA = collider;
                }
                else if (!QrB && !slotUsedB)
                {
                    QrB = other.transform;
                    slotUsedB = true;
                    QrNameB = other.name;
                    QrScriptB = QrScript;
                    colliderB = collider;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = false;
        }
    }
    void checkQR()
    {
        if (slotUsedA)
        {
            if (QrScriptA.taken)
            {
                Debug.Log("A taken");
                QrScriptA.onTable = false;
                QrNameA = null;
                QrA = null;
                slotUsedA = false;
                QrScriptA = null;
                colliderA = null;
                lineRenderer.enabled = false;
            }
        }
        if (slotUsedB)
        {
            if (QrScriptB.taken)
            {
                Debug.Log("B taken");
                QrScriptB.onTable = false;
                QrNameB = null;
                QrB = null;
                slotUsedB = false;
                QrScriptB = null;
                colliderB = null;
                lineRenderer.enabled = false;
            }
        }
    }
    // измерение
    void measurement()
    {
        if (slotUsedA && slotUsedB)
        {

            if (QrA != null && QrB != null)
            {
                distCalc();
            }
        }
        else if ((slotUsedA && !slotUsedB) || (!slotUsedA && slotUsedB))
        {
            textDist.text = "Теперь положи второй QR-код";
        }
        else
        {
            textDist.text = "Тут будет результат измерения";
        }
    }

    // расчёт растояния
    void distCalc()
    {
        // old
        //float distance = Vector3.Distance(QrA.position, QrB.position);
        //textDist.text = ("Растояние между QR: " + distance);

        // Находим ближайшие точки на поверхности каждого коллайдера
        Vector3 closestPointA = colliderA.ClosestPoint(colliderB.transform.position);
        Vector3 closestPointB = colliderB.ClosestPoint(colliderA.transform.position);

        // Вычисляем расстояние между этими точками
        float distance = Vector3.Distance(closestPointA, closestPointB);
        textDist.text = ("Растояние между QR: " + distance);

        // Debug
        pointA.position = closestPointA;
        pointB.position = closestPointB;
        RenderRuler();
    }

    void RenderRuler()
    {
        lineRenderer.enabled = true;
        Vector3 drawPoinA = new Vector3(pointA.position.x, rolerDrawHeight, pointA.position.z);
        Vector3 drawPoinB = new Vector3(pointB.position.x, rolerDrawHeight, pointB.position.z);
        lineRenderer.SetPosition(0, drawPoinA);
        lineRenderer.SetPosition(1, drawPoinB);
        
        //Vector3 drawSupportPoinA = new Vector3(drawPoinA.x, rolerDrawHeight + 0.1f, drawPoinA.z);
        //Vector3 drawSupportPoinB = new Vector3(drawPoinB.x, rolerDrawHeight + 0.1f, drawPoinB.z);
        //lineRenderer.SetPosition(2, pointA.position);
        //lineRenderer.SetPosition(3, drawSupportPoinA);
        //lineRenderer.SetPosition(0, pointB.position);
        //lineRenderer.SetPosition(1, drawSupportPoinB);
    }
}
