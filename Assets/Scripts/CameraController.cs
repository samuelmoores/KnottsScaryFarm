using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Transform CameraFollowObject;
    public float inputSensitivity = 150.0f;
    public float clampAngle = 80.0f;

    float rotX;
    float rotY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraFollowObject = GameObject.Find("Player").transform;
        transform.position = GameObject.Find("Player").transform.position;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Mouse X");
        float inputZ = Input.GetAxis("Mouse Y");

        rotY += inputX * inputSensitivity * Time.deltaTime;
        rotX += inputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = CameraFollowObject;
        float CameraMoveSpeed = 120.0f;

        float step = CameraMoveSpeed * Time.deltaTime;

        if ((target))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}