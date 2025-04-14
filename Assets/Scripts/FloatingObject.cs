using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    GameObject player;
    GameObject brian;
    PlayerMovement playerMovement;
    Rigidbody rb;
    float x;
    float y;
    float z;

    float x_scale;
    float y_scale;
    float z_scale;

    float speed;
    public bool initiated = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        brian = GameObject.Find("Brian");

        playerMovement = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();

        x = Random.Range(1, 10);
        y = Random.Range(1, 10);
        z = Random.Range(1, 10);
        speed = Random.Range(2, 7);

        rb.angularVelocity = new Vector3(x, y, z);

        x_scale = transform.localScale.x;
        y_scale = transform.localScale.y;
        z_scale = transform.localScale.z;


    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Input.GetKeyDown(KeyCode.Mouse1) && distance <= 10.0f)
        {
            rb.angularVelocity = Vector3.zero;
            rb.AddExplosionForce(100.0f, player.transform.position, 10.0f);
        }

        if(playerMovement.GetPlayerSpeed() > 0.0f)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce((player.transform.position - transform.position).normalized);
            }
        }

        if(initiated)
        {
            rb.AddForce(transform.position - brian.transform.position);

            x_scale += Time.deltaTime * 2.0f;
            y_scale += Time.deltaTime * 2.0f;
            z_scale += Time.deltaTime * 2.0f;

            if(x_scale > 4)
            {
                Destroy(gameObject);
            }

            transform.localScale = new Vector3(x_scale, y_scale, z_scale);
        }
    }

    public void InitializeTheGrowth()
    {
        initiated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            rb.AddForce(player.transform.forward * 52.0f * 4);
        }
    }
}
