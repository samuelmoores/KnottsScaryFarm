using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;

public class Drake : MonoBehaviour
{
    GameObject Player;
    PlayerHealth playerHealth;
    NavMeshAgent agent;
    Animator animator;
    bool notTired = true;
    bool hasHitPlayer = false;
    float distanceFromPlayer;
    int random;

    float attackTimer = -1;
    float runTimer = 15.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");
        playerHealth = Player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if(distanceFromPlayer > agent.stoppingDistance && notTired && attackTimer < 0.0f)
        {
            agent.destination = Player.transform.position;
            animator.SetBool("dance", false);
            animator.SetBool("run", true);
            transform.LookAt(new Vector3(Player.transform.position.x, 0.0f, Player.transform.position.z));
            attackTimer = -1;

            runTimer -= Time.deltaTime;
            if(runTimer < 0.0f)
            {
                notTired = false;
                animator.SetBool("run", false);
                agent.isStopped = true;
            }
        }
        else if(runTimer > 0.0f)
        {
            if(attackTimer < 0.0f)
            {
                random = Random.Range(0, 101);
                runTimer = 15.0f;
                animator.SetBool("run", false);

                if (random >= 50 && playerHealth.GetHealth() > 0.0f)
                {
                    animator.SetTrigger("melee");
                    attackTimer = 2.4f;
                }
                else
                {
                    animator.SetBool("dance", true);
                    attackTimer = 7.0f;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;

                if(attackTimer > 2.4 && distanceFromPlayer > 14.0f)
                {
                    attackTimer = -1.0f;
                }

            }


        }
        else
        {
            runTimer -= Time.deltaTime;

            if (runTimer < -5.0f)
            {
                runTimer = 15.0f;
                notTired = true;
                agent.isStopped = false;

            }
        }
    }

    public void HitPlayer(bool contact)
    {
        hasHitPlayer = contact;
    }

    public float GetRunTimer()
    {
        return runTimer;
    }
    public void Attack()
    {
        if(distanceFromPlayer < 4.0 && hasHitPlayer)
        {
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1.0f);
        }
    }
}
