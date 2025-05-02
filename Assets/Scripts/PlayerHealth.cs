using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public AnimationClip damageAnimation;
    public int currentScene;

    Animator animator;
    Rigidbody rb;
    Inventory inventory;

    float health = 1.0f;
    bool dead = false;
    bool damaged = false;
    bool healing = false;
    float damageTimer = 0.0f;
    float restartTimer = 4.0f;
    float healCooldown = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        damageTimer = damageAnimation.length;
        animator = GetComponent<Animator>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
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

        healCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Q) && healCooldown < 0.0f)
        {
            healCooldown = 2.6f;
            float healAmount = inventory.Heal();

            if(healAmount > 0.0f)
            {
                animator.SetTrigger("heal");
            }

            if(health + healAmount <= 1.0f)
            {
                health += healAmount;
            }
            else
            {
                health = 1.0f;
            }
        }

        healing = healCooldown > 0.0f;

    }

    public void DropHeal()
    {
        inventory.DropHeal();
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
        return !dead && !damaged && !healing;
    }

    public float GetHealth()
    {
        return health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pickup"))
        {
            if(collision.gameObject.GetComponent<Pickup>().Name == "Tomato")
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

   
}
