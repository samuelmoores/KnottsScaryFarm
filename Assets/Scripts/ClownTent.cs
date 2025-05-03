using UnityEngine;

public class ClownTent : MonoBehaviour
{
    public GameObject tomato;
    public GameObject coin;
    public Transform hand;
    public Transform throwTarget;
    public Transform coinSpawnPosition;
    public float throwSpeed = 30.0f;
    public GameObject exitMeshes;
    float health = 1.0f;
    bool dead = false;
    bool damaged = false;
    float exitSpawnTimer = 4.0f;

    GameObject tomato_instance;
    GameObject coin_instance;
    GameObject player;
    Animator animator;

    float distance_to_player = 0.0f;
    float throw_distance = 25.0f;
    float throwTimer = 3.0f;
    float prev_y_rotation = 0.0f;

    GameObject corndog;
    bool foundCorndog = false;
    float corndog_fascination_timer = -1.0f;

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
            if(foundCorndog && corndog_fascination_timer < 0.0f)
            {
                corndog_fascination_timer = 7.0f;
            }

            if(corndog_fascination_timer < 0.0f)
            {
                transform.LookAt(player.transform, Vector3.up);
            }
            else
            {
                transform.LookAt(corndog.transform, Vector3.up);
                corndog_fascination_timer -= Time.deltaTime;

                if(corndog_fascination_timer < 0.0f)
                {
                    foundCorndog = false;
                }

            }

            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

            if((prev_y_rotation - transform.rotation.eulerAngles.y) < 0.0f)
            {
                animator.SetBool("turningRight", true);
                animator.SetBool("turningLeft", false);
            }
            else if((prev_y_rotation - transform.rotation.eulerAngles.y) > 0.0f)
            {
                animator.SetBool("turningRight", false);
                animator.SetBool("turningLeft", true);
            }
            else
            {
                animator.SetBool("turningRight", false);
                animator.SetBool("turningLeft", false);
                animator.SetBool("foundCorndog", foundCorndog);
            }

            prev_y_rotation = transform.rotation.eulerAngles.y;

            distance_to_player = Vector3.Distance(player.transform.position, transform.position);

            throwTimer -= Time.deltaTime;

            if(throwTimer <= 0.0f && distance_to_player < throw_distance && !foundCorndog)
            {
                animator.SetTrigger("throw");
                throwTimer = Random.Range(2.0f, 3.5f);
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
            //rb.AddForce((throwTarget.position - hand.position).normalized * throwSpeed, ForceMode.Impulse);
            rb.AddForce(transform.forward * throwSpeed, ForceMode.Impulse);

            rb.angularVelocity = tomato_instance.transform.right * -15.0f;
        }
    }

    public void LookAtCorndog(GameObject corndog)
    {
        this.corndog = corndog;
        foundCorndog = true;
    }


    public float GetHealth()
    {
        return health;
    }

    public void CanTakeDamage()
    {
        damaged = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pickup") && !dead && !damaged)
        {
            coin_instance = GameObject.Instantiate(coin, coinSpawnPosition.position, Quaternion.identity);
            coin_instance.GetComponent<Rigidbody>().AddForce(coinSpawnPosition.up * 250.0f);
            coin_instance.GetComponent<Rigidbody>().angularVelocity = new Vector3(2, 4, 5);

            animator.SetTrigger("damage");
            health -= 0.1f;
            foundCorndog = false;
            animator.SetBool("foundCorndog", false);

            if (health <= 0.0f)
            {
                dead = true;
                animator.SetBool("dead", true);
                exitMeshes.transform.position = transform.position + (transform.forward * 3);
            }
        }
    }
}
