using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float mouseSensitivity = 200f; // ���������������� ����
    float speedPlayer = 15.0f; // �������� ������
    float xRotation = 0f;
    Rigidbody rigPlayer;
    Transform transPlayer;
    Transform transCamera;

    float rayDist = 5f; // ������������ ���������, ��� Raycast
    public string nameObj = null;
    Transform grabPoint; // �����, ���� ����� ���������� ������
    GameObject grabbedObj; // ��������� ������
    Rigidbody grabbedObjRig; // Rigidbody ���������� �������
    public bool handsFree = true; // ���� ��������
    public bool youCanGrab = false; // ����� �����
    bool CursorModeLock = true;
    public Vector3 rayTarget = Vector3.zero;
    public bool targetRulerPoint = false;
    public bool targetRulerPointB = false;

    // Start is called before the first frame update
    void Start()
    {
        InItPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
        if (handsFree) RayPlayer();
    }

    private void FixedUpdate()
    {
        ControlFixed();
    }

    void ControlFixed()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigPlayer.AddForce(transform.forward * speedPlayer);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigPlayer.AddForce(transform.forward * -speedPlayer);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigPlayer.AddForce(transform.right * -speedPlayer);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigPlayer.AddForce(-transform.right * -speedPlayer);
        }

        //if (Input.GetKey(KeyCode.N))
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //}
    }

    void Control()
    {
        // �������� ���� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // ������� ������ �� ��� X (�����-����)
        // ������������ ������� ������, ����� ��� �� ����� ����������� ������� ������
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ������� ������ �� ��� Y (�����-������)
        transPlayer.Rotate(Vector3.up * mouseX);

        transCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ������� ������

        if (Input.GetKeyDown(KeyCode.E))
        {
            gragPlayer();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!CursorModeLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                CursorModeLock = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                CursorModeLock = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            targetRulerPoint = !targetRulerPoint;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            targetRulerPointB = !targetRulerPointB;
        }
    }

    void RayPlayer()
    {
        Ray ray = new Ray(transCamera.position, transCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDist))
        {
            if (hit.collider.name != nameObj)
            {
                nameObj = hit.collider.name;
                grabbedObj = hit.collider.gameObject;
                if (grabbedObj.CompareTag("QR"))
                {
                    youCanGrab = true;
                    textMeshProUGUI.text = "E - �����";
                Debug.Log("Objects: " + nameObj);
                }
                else
                {
                    Debug.Log("NO GRAB Objects: " + nameObj);
                    youCanGrab = false;
                    //nameObj = null;
                    textMeshProUGUI.text = "";
                }
            }

            // ����� ������� ����
            rayTarget = hit.point;
            // ������ ���� ��� Debug
            Debug.DrawRay(transCamera.position, transCamera.forward * hit.distance, Color.red);
        }
        else
        {
            // ���������� ���������, ���� ��� �� ����� � ������
            if (handsFree)
            {
                youCanGrab = false;
                nameObj = null;
                textMeshProUGUI.text = "";
                //grabbedObj = null;
                //Debug.Log("����� �������� ��� �������");
            }
        }
    }

    void InItPlayer()
    {
        rigPlayer = GetComponent<Rigidbody>();
        transPlayer = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked; // �������� � ��������� ������ ����
        transCamera = transform.GetChild(0);
        grabPoint = transCamera.GetChild(0);
        textMeshProUGUI.text = "";
    }

    void gragPlayer()
    {
        Debug.Log("gragPlayer()");
        if (youCanGrab && handsFree)
        {
            grabbedObjRig = grabbedObj.GetComponent<Rigidbody>();
            grabbedObjRig.isKinematic = true;
            grabbedObj.transform.SetParent(grabPoint);
            grabbedObj.transform.localPosition = Vector3.zero;
            grabbedObj.transform.localRotation = Quaternion.identity;
            grabbedObj.transform.localRotation = Quaternion.Euler(10, 0, 0); // ������ �� ��� X �� 10 ��������
            youCanGrab = false;
            handsFree = false;
            nameObj = null;
            textMeshProUGUI.text = "E - ��������";

            QRCode QRObjScript = grabbedObj.GetComponentInChildren<QRCode>();
            QRObjScript.taken = true;
            QRObjScript.onTable = false;

        }
        else if (!youCanGrab && !handsFree)
        {
            //grabbedObj.transform.localRotation = Quaternion.identity;
            grabbedObjRig.isKinematic = false;
            grabbedObj.transform.SetParent(null);
            grabbedObjRig = null;
            nameObj = null;
            handsFree = true;
            youCanGrab = false;
            textMeshProUGUI.text = "";

            QRCode QRObjScript = grabbedObj.GetComponentInChildren<QRCode>();
            QRObjScript.taken = false;
        }
    }
}
