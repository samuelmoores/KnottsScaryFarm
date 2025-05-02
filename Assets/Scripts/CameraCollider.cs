using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;
    public float colliderWidth;

    GameObject player;
    GameObject cameraTarget;
    Vector3 dollyDir;
    Vector3 desiredCameraPos;
    RaycastHit hit;
    public float dollyDirAdjusted;
    public float distance;
    public float aimDistance;

    void Awake()
    {
        player = GameObject.Find("Player");
        cameraTarget = GameObject.Find("CameraTarget");
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
        maxDistance = Vector3.Distance(player.transform.position, transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        desiredCameraPos = transform.position;

        Debug.DrawLine(player.transform.position, desiredCameraPos + transform.right * colliderWidth, Color.red);
        Debug.DrawLine(player.transform.position, desiredCameraPos + -transform.right * colliderWidth, Color.blue);


        if (Physics.Linecast(transform.parent.position, desiredCameraPos + transform.right * colliderWidth, out hit) && hit.transform.gameObject.layer != 1)
        {
            float newDistance = Mathf.Clamp(hit.distance * 0.9f, minDistance, maxDistance);
            if (Mathf.Abs(newDistance - distance) > 0.05f)
            {
                distance = newDistance;
            }

        }
        else if(Physics.Linecast(transform.parent.position, desiredCameraPos + transform.right * -colliderWidth, out hit) && hit.transform.gameObject.layer != 1)
        {
            float newDistance = Mathf.Clamp(hit.distance * 0.9f, minDistance, maxDistance);
            if (Mathf.Abs(newDistance - distance) > 0.05f)
            {
                distance = newDistance;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1) && player.GetComponent<PlayerAttack>().ThrowableEquiped())
            {
                distance = aimDistance;
            }
            else
            {
                distance = maxDistance;
            }
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        transform.LookAt(cameraTarget.transform);

    }

}
