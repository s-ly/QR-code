using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorQR : MonoBehaviour
{
    [SerializeField] GameObject QR;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void GenQR()
    {
        Instantiate(QR, spawnPoint.position, spawnPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") )
        {
            Debug.Log("Player enter");
            canvas.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exit");
            canvas.enabled = false;
        }

    }

    void Control()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GenQR();
        }

    }
}
