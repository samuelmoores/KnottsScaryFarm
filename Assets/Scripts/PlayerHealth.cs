using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public AnimationClip damageAnimation;
    public int currentScene;

    Animator animator;
    Rigidbody rb;

    float health = 1.0f;
    bool dead = false;
    bool damaged = false;
    float damageTimer = 0.0f;
    float restartTimer = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damageTimer = damageAnimation.length;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damaged && damageTimer > 0.0f)
        {
            damageTimer -= Time.deltaTime;
        }
        else if(dead)
        {
            restartTimer -= Time.deltaTime;

            if(restartTimer < 0.0f)
            {
                SceneManager.LoadScene(currentScene);
            }
        }
        else
        {
            damaged = false;
            damageTimer = damageAnimation.length;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        damaged = true;
        health -= damageAmount;

        if(health <= 0.0f)
        {
            health = 0.0f;
            dead = true;
            animator.SetBool("dead", true);
        }
    }

    public bool canMove()
    {
        return !dead && !damaged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tomato"))
        {
            TakeDamage(0.3f);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity /= 2;

            if(!dead)
            {
                animator.SetTrigger("damage");
            }
        }
    }

   
}
