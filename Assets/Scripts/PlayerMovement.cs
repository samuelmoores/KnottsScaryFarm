using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float playerRotation;
    public AnimationClip jumpAnimation;
    public AudioClip[] footstepSound;
    public AudioClip[] grassFootstepSound;

    public float footstepVolume;
    public float jumpHeight;

    public Transform RegularTarget;
    public Transform AimTarget;

    CharacterController controller;
    Animator animator;
    GameObject cam;
    CinemachineCamera cam_cine;
    CinemachineOrbitalFollow cam_follow;
    PlayerHealth health;
    PlayerAttack playerAttack;
    Vector3 velocity;
    bool aiming = false;
    float jumpCooldown = -1.0f;
    bool onGrass = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        
        cam = GameObject.Find("Main Camera");
        cam_cine = GameObject.Find("FreeLook Camera").GetComponent<CinemachineCamera>();
        cam_follow = GameObject.Find("FreeLook Camera").GetComponent<CinemachineOrbitalFollow>();
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
            else if(!Input.GetKey(KeyCode.Mouse1))
            {
                aiming = false;
            }

            if (aiming)
            {
                cam_cine.Follow = AimTarget;
                cam_cine.LookAt = AimTarget;

                cam_follow.Orbits.Top.Height = 1.11f;
                cam_follow.Orbits.Top.Radius = 0.94f;

                cam_follow.Orbits.Center.Height = 0.3f;
                cam_follow.Orbits.Center.Radius = 1.22f;

                cam_follow.Orbits.Bottom.Height = -1.38f;
                cam_follow.Orbits.Bottom.Radius = 1.04f;

                Vector3 playerRotation = transform.eulerAngles;
                float cameraYRotation = cam.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(playerRotation.x, cameraYRotation, playerRotation.z);
                direction = cam.transform.forward * vertical + cam.transform.right * horizontal;
                direction.Normalize();

                animator.SetFloat("directionX", horizontal);
                animator.SetFloat("directionZ", vertical);
            }
            else
            {
                cam_cine.Follow = RegularTarget;
                cam_cine.LookAt = RegularTarget;

                cam_follow.Orbits.Top.Height = 2.8f;
                cam_follow.Orbits.Top.Radius = 1.54f;

                cam_follow.Orbits.Center.Height = 0.9f;
                cam_follow.Orbits.Center.Radius = 3.88f;

                cam_follow.Orbits.Bottom.Height = -1.5f;
                cam_follow.Orbits.Bottom.Radius = 1.64f;
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
            
            if(Input.GetKeyDown(KeyCode.LeftShift) && playerSpeed == 4 && !aiming)
            {
                playerSpeed *= 2.0f; 
                animator.SetBool("sprint", true);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && playerSpeed == 8)
            {
                playerSpeed /= 2.0f;
                animator.SetBool("sprint", false);

            }

            controller.Move((direction * playerSpeed + velocity) * Time.deltaTime);
            animator.SetBool("running", direction != Vector3.zero);
            animator.SetBool("aiming", aiming);
        }
    }

    
    public void Footstep()
    {
        if(onGrass)
        {
            SoundManager.instance.PlaySound(grassFootstepSound[Random.Range(0, 8)], gameObject.transform, footstepVolume);
        }
        else
        {
            SoundManager.instance.PlaySound(footstepSound[Random.Range(0, 8)], gameObject.transform, footstepVolume);
        }
    }

    public bool IsAiming()
    {
        return aiming;
    }


    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Grass"))
        {
            onGrass = true;
        }

        if(other.CompareTag("Tile"))
        {
            onGrass = false;
        }
    }
}
