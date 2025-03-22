using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public AnimationClip damageAnimation;

    Animator animator;

    float health = 1.0f;
    bool dead = false;
    bool damaged = false;
    float damageTimer = 0.0f;

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
        }
    }

    public bool canMove()
    {
        return !dead && !damaged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Damage"))
        {
            animator.SetTrigger("damage");
            TakeDamage(0.3f);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity /= 2;
        }
    }
}
