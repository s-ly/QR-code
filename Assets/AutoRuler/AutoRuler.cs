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
    //QRCode QrAScript;
    //[SerializeField] Transform pointB;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textDist;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        QrA = null;
        QrB = null;
        QrNameA = null;
        QrNameB = null;
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
            if (!QrScript.taken && !QrScript.onTable)

            {
                QrScript.onTable = true;
                if (!QrA && !slotUsedA)
                {
                    QrA = other.transform;
                    slotUsedA = true;
                    QrNameA = other.name;
                    QrScriptA = QrScript;
                }
                else if (!QrB && !slotUsedB)
                {
                    //QrScript.onTable = true;
                    QrB = other.transform;
                    slotUsedB = true;
                    QrNameB = other.name;
                    QrScriptB = QrScript;
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
                float distance = Vector3.Distance(QrA.position, QrB.position);
                textDist.text = ("Растояние между QR: " + distance);
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
}
