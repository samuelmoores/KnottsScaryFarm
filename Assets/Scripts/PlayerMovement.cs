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
    bool aiming = false;
    [HideInInspector] public bool hasWeapin = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera");
        startingHeight = transform.position.y;
        health = GetComponent<PlayerHealth>();
        animator.SetInteger("direction", -1);
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

            if(hasWeapin)
            {
                aiming = Input.GetKey(KeyCode.Mouse1);
                animator.SetBool("aiming", aiming);
            }

            if(aiming)
            {
                GameObject cam = GameObject.Find("Main Camera");
                Vector3 playerRotation = transform.eulerAngles;
                float cameraYRotation = cam.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(playerRotation.x, cameraYRotation, playerRotation.z);
                direction = cam.transform.forward * vertical + cam.transform.right * horizontal;
                direction.Normalize();

                Debug.Log("horz: " + horizontal + "||" + " vert: " + vertical);

                animator.SetFloat("directionX", horizontal);
                animator.SetFloat("directionZ", vertical);

            }

            if (direction != Vector3.zero && !aiming)
            {
                direction = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * direction;
                direction.Normalize();

                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotation * 100 * Time.deltaTime);
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

    public bool IsAiming()
    {
        return aiming;
    }
}
