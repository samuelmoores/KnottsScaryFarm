using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    float x;
    float y;
    float z;
    float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        x = Random.Range(1, 10);
        y = Random.Range(1, 10);
        z = Random.Range(1, 10);
        speed = Random.Range(2, 7);

        rb.angularVelocity = new Vector3(x, y, z);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.Mouse0) && distance <= 10.0f)
        {
            rb.angularVelocity = Vector3.zero;
            rb.AddExplosionForce(100.0f, player.transform.position, 10.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rb.AddForce(player.transform.forward * 220.0f);
        }
    }
}
