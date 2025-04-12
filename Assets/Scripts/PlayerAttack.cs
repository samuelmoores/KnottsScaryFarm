using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Hand;
    public AnimationClip ThrowAnimation;
    Inventory inventory;
    PlayerMovement playerMovement;
    Animator animator;

    bool attacking = false;
    float coolDown = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(playerMovement.IsAiming())
            {
                animator.SetTrigger("throw");
                playerMovement.playerSpeed = 0.0f;
                playerMovement.playerRotation = 0.0f;
                attacking = true;
                coolDown = ThrowAnimation.length - 0.2f;
            }
        }

        if(attacking && coolDown > 0.0f)
        {
            coolDown -= Time.deltaTime;

            if (coolDown <= 0.0f)
            {
                attacking = false;
                playerMovement.playerSpeed = 4.0f;
                playerMovement.playerRotation = 7.0f;

            }
        }
    }

    public void Throw()
    {
        List<GameObject> gameObjects = inventory.GetGameObject();

        Rigidbody rb = gameObjects[0].GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * 30.0f, ForceMode.Impulse);
        rb.useGravity = true;
        gameObjects[0].GetComponent<MeshCollider>().enabled = true;

        inventory.Use();
            
    }

    

}
