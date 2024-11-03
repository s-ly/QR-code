using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorQR : MonoBehaviour
{
    [SerializeField] GameObject QR;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_InputField inputField;
    string textQR;
    bool generatorOn = false;
    int namePrefix = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        //InItInputField();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void GenQR()
    {
        GameObject qr = Instantiate(QR, spawnPoint.position, spawnPoint.rotation);
        GameObject boxQR = qr.transform.GetChild(0).gameObject;
        GameObject qrCode = boxQR.transform.GetChild(0).gameObject;
        qrCode.GetComponent<QRCode>().InIt(inputField.text);

        // уникальное имя
        namePrefix++;
        string tmp = boxQR.name + namePrefix.ToString();
        boxQR.name = tmp;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player enter");
            canvas.enabled = true;
            generatorOn = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exit");
            canvas.enabled = false;
            generatorOn = false;
        }

    }

    void Control()
    {
        if (Input.GetKeyDown(KeyCode.Q) && generatorOn)
        {
            GenQR();
        }

    }

    void InItInputField()
    {
        // Привязываем метод к событию OnValueChanged
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    // Метод, вызываемый при изменении значения в InputField
    void OnInputFieldValueChanged(string value)
    {
        Debug.Log("Value changed: " + value);
        textQR = inputField.text;
    }
}
