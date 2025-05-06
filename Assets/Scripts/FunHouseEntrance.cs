using UnityEngine;

public class FunHouseEntrance : MonoBehaviour
{
    public Transform toTransform;
    public GameObject colliders;
    bool open = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            colliders.SetActive(false);
            transform.position = Vector3.Lerp(transform.position, toTransform.position, Time.deltaTime/10);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toTransform.rotation, Time.deltaTime* 3);
            transform.localScale = Vector3.Lerp(transform.localScale, toTransform.localScale, Time.deltaTime/10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Pickup pickup = other.gameObject.GetComponent<Pickup>();

            if (pickup.Name == "Tomato")
            {
                open = true;
            }
        }
    }
}
