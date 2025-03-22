using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float playerRotation;
    public AudioClip[] footstepSound;
    public float footstepVolume = 1.0f;
    public float jumpHeight;

    CharacterController controller;
    Animator animator;
    GameObject cam;
    PlayerHealth health;
    Vector3 velocity;
    int footstep = 0;
    float startingHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera");
        startingHeight = transform.position.y;
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.canMove())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
            direction.Normalize();

            if(direction != Vector3.zero)
            {
                direction = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * direction;
                direction.Normalize();

                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotation*100 * Time.deltaTime);
            }


            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -Physics.gravity.y);
                animator.SetTrigger("jump");
            }

            velocity.y += Physics.gravity.y * Time.deltaTime * 2.0f;

            controller.Move((direction * playerSpeed + velocity) * Time.deltaTime);
            animator.SetBool("running", direction != Vector3.zero);
        }
    }

    public void Footstep()
    {
        SoundManager.instance.PlaySound(footstepSound[footstep++], gameObject.transform, footstepVolume);
        if(footstep == 2)
        {
            footstep = 0;
        }
    }
}
