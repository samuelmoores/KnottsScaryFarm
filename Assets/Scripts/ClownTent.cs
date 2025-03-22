using UnityEngine;

public class ClownTent : MonoBehaviour
{
    public GameObject Tomato;
    public Transform hand;

    GameObject tomato_instance;
    GameObject player;
    float distance_to_player = 0.0f;
    float throw_distance = 22.0f;
    Animator animator;
    float throwTimer = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        distance_to_player = Vector3.Distance(player.transform.position, transform.position);

        throwTimer -= Time.deltaTime;

        if(throwTimer <= 0.0f && distance_to_player < throw_distance)
        {
            animator.SetTrigger("throw");
            throwTimer = 3.0f;
        }
    }

    public void ThrowTomato()
    {
        if(distance_to_player < throw_distance)
        {
            tomato_instance = Instantiate(Tomato, hand.position, Tomato.transform.rotation);
            Rigidbody rb = tomato_instance.GetComponent<Rigidbody>();
            rb.AddForce((player.transform.position - transform.position).normalized * 10.0f, ForceMode.Impulse);
            rb.angularVelocity = tomato_instance.transform.right * -15.0f;
            Destroy(tomato_instance, 5.0f);
        }
    }
}
