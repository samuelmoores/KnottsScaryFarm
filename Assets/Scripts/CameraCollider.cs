using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;

    GameObject player;
    GameObject cameraTarget;
    Vector3 dollyDir;
    Vector3 desiredCameraPos;
    RaycastHit hit;
    public float dollyDirAdjusted;
    public float distance;

    void Awake()
    {
        player = GameObject.Find("Player");
        cameraTarget = GameObject.Find("CameraTarget");
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
        maxDistance = Vector3.Distance(player.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit) && hit.transform.gameObject.layer != 1)
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);
            
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        transform.LookAt(cameraTarget.transform);
    }

}
