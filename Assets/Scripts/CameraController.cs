using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Transform CameraFollowObject;
    Transform LookAtTarget;
    public float inputSensitivity = 150.0f;
    public float clampAngle = 80.0f;

    int invertCamera = 1;
    float rotX;
    float rotY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CameraFollowObject = GameObject.Find("Player").transform;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputZ = -Input.GetAxis("Mouse Y");

        rotY += inputX * inputSensitivity * Time.deltaTime;
        rotX += inputZ * invertCamera*inputSensitivity * Time.deltaTime;
        
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
    public void InvertCamera()
    {
        if(invertCamera == 1)
        {
            invertCamera = -1;
        }
        else
        {
            invertCamera = 1;
        }
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