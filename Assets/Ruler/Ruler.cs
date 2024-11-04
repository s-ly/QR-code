using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ruler : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject rulerTarget;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI textDist;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.targetRulerPoint)
        {
            PositionPoint();
        }
        if (player.targetRulerPointB)
        {
            PositionPointB();
        }

        float distance = Vector3.Distance(pointA.position, pointB.position);
        string strDist = "Растояние = " + distance;
        textDist.text = strDist;
    }

    void RuletPosition()
    {
        rulerTarget.transform.position = player.rayTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = true;
            RuletPosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            canvas.enabled = false;
        }
    }

    void PositionPoint()
    {
        pointA.position = rulerTarget.transform.position;
    }

    void PositionPointB()
    {
        pointB.position = rulerTarget.transform.position;
    }
}
