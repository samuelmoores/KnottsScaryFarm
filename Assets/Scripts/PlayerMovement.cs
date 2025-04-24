using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float playerRotation;
    public AnimationClip jumpAnimation;
    public AudioClip[] footstepSound;
    public float footstepVolume;
    public float jumpHeight;

    CharacterController controller;
    Animator animator;
    GameObject cam;
    PlayerHealth health;
    PlayerAttack playerAttack;
    Vector3 velocity;
    bool aiming = false;
    float jumpCooldown = -1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        
        cam = GameObject.Find("Main Camera");
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

            if(playerAttack.ThrowableEquiped())
            {
                aiming = Input.GetKey(KeyCode.Mouse1);
            }
            else
            {
                aiming = false;
            }

            if (aiming)
            {
                GameObject cam = GameObject.Find("Main Camera");
                Vector3 playerRotation = transform.eulerAngles;
                float cameraYRotation = cam.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(playerRotation.x, cameraYRotation, playerRotation.z);
                direction = cam.transform.forward * vertical + cam.transform.right * horizontal;
                direction.Normalize();

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


            if (Input.GetButtonDown("Jump") && controller.isGrounded && jumpCooldown < 0.0f)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -Physics.gravity.y);
                animator.SetTrigger("jump");
                jumpCooldown = jumpAnimation.length;
            }
            else if(jumpCooldown > -1.0f)
            {
                jumpCooldown -= Time.deltaTime;
            }

            if (velocity.y > -9.8)
            {
                velocity.y += Physics.gravity.y * Time.deltaTime * 2.0f;
            }
            
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerSpeed *= 2.0f; 
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerSpeed /= 2.0f;
            }

            controller.Move((direction * playerSpeed + velocity) * Time.deltaTime);
            animator.SetBool("running", direction != Vector3.zero);
            animator.SetBool("sprint", Input.GetKey(KeyCode.LeftShift));
            animator.SetBool("aiming", aiming);
        }
    }

    public void Footstep()
    {
        SoundManager.instance.PlaySound(footstepSound[Random.Range(0, 8)], gameObject.transform, footstepVolume);
    }

    public bool IsAiming()
    {
        return aiming;
    }


    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
}
