using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 200f; // чувствительность мыши
    float speedPlayer = 15.0f; // скорость игрока
    float xRotation = 0f;
    Rigidbody rigPlayer;
    Transform transPlayer;
    Transform transCamera;

    float rayDist = 5f; // Максимальная дистанция, для Raycast
    public string nameObj = null;
    Transform grabPoint; // Точка, куда будет прикреплен объект
    GameObject grabbedObj; // Выбранный объект
    Rigidbody grabbedObjRig; // Rigidbody выбранного объекта
    public bool handsFree = true; // руки свободны
    public bool youCanGrab = false; // можно брать

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
    }

    void Control()
    {
        // Получаем ввод мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Поворот камеры по оси X (вверх-вниз)
        // Ограничиваем поворот камеры, чтобы она не могла повернуться слишком далеко
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Поворот игрока по оси Y (влево-вправо)
        transPlayer.Rotate(Vector3.up * mouseX);

        transCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // поворот камеры

        if (Input.GetKeyDown(KeyCode.E))
        {
            gragPlayer();
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
                youCanGrab = true;
                Debug.Log("Objects: " + nameObj);
            }
        }
    }
    
    void InItPlayer()
    {
        rigPlayer = GetComponent<Rigidbody>();
        transPlayer = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked; // Скрываем и блокируем курсор мыши
        transCamera = transform.GetChild(0);
        grabPoint = transCamera.GetChild(0);
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
            youCanGrab = false;
            handsFree = false;
            nameObj = null;
        }
        else if (!youCanGrab && !handsFree)
        {
            //grabbedObj.transform.localRotation = Quaternion.identity;
            grabbedObjRig.isKinematic = false;
            grabbedObj.transform.SetParent(null);
            grabbedObjRig = null;
            nameObj = "grabEnd";
            handsFree = true;
            youCanGrab = false;
        }
    }
}
