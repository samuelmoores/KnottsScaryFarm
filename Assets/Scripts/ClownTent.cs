using UnityEngine;

public class ClownTent : MonoBehaviour
{
    public GameObject tomato;
    public Transform hand;
    public Transform throwTarget;
    public float throwSpeed = 30.0f;
    public GameObject exitMeshes;
    float health = 1.0f;
    bool dead = false;
    float exitSpawnTimer = 4.0f;

    GameObject tomato_instance;
    GameObject player;
    Animator animator;

    float distance_to_player = 0.0f;
    float throw_distance = 22.0f;
    float throwTimer = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitMeshes.SetActive(false);

        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            transform.LookAt(player.transform, Vector3.up);
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

            distance_to_player = Vector3.Distance(player.transform.position, transform.position);

            throwTimer -= Time.deltaTime;

            if(throwTimer <= 0.0f && distance_to_player < throw_distance)
            {
                animator.SetTrigger("throw");
                throwTimer = Random.Range(3.0f, 7.0f);
            }
        }
        else if(exitSpawnTimer > 0.0f)
        {
            exitSpawnTimer -= Time.deltaTime;
        }
        else
        {
            exitMeshes.SetActive(true);
            exitMeshes.transform.position = transform.position + (transform.forward * 6) + (transform.right * 4);
            exitMeshes.transform.rotation = transform.rotation;
        }
    }

    public void ThrowTomato()
    {
        if(distance_to_player < throw_distance)
        {
            tomato_instance = Instantiate(tomato, hand.position, tomato.transform.rotation);
            Rigidbody rb = tomato_instance.GetComponent<Rigidbody>();
            rb.AddForce((throwTarget.position - hand.position).normalized * throwSpeed, ForceMode.Impulse);
            rb.angularVelocity = tomato_instance.transform.right * -15.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pickup") && !dead)
        {
            animator.SetTrigger("damage");
            health -= 0.6f;

            if (health <= 0.0f)
            {
                dead = true;
                animator.SetBool("dead", true);
                exitMeshes.transform.position = transform.position + (transform.forward * 3);
            }
        }
    }
}
