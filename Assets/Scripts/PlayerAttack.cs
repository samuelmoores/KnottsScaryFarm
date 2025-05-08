using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform Hand;
    public AnimationClip ThrowAnimation;
    Inventory inventory;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Animator animator;
    GameObject WhiteClown;
    GameObject cam;

    bool attacking = false;
    float coolDown = 0.0f;
    bool throwableEquiped = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        WhiteClown = GameObject.Find("CircusTentClown");
        cam = GameObject.Find("Main Camera");
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
        GameObject objectToThrow = inventory.Use();
        Pickup pickup = objectToThrow.GetComponent<Pickup>();
        pickup.Drop();

        if(pickup.Name != "CottonCandy")
        {
            objectToThrow.transform.parent = null;
            Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward * 30.0f, ForceMode.Impulse);
            rb.useGravity = true;
            objectToThrow.GetComponent<MeshCollider>().enabled = true;
        }


        if(pickup.Name == "Corndog" && WhiteClown)
        {
            WhiteClown.GetComponent<ClownTent>().LookAtCorndog(objectToThrow);
        }
    }

    public void EquipThrowable()
    {
        throwableEquiped = true;
    }

    public void UnequipThrowable()
    {
        throwableEquiped = false;
    }

    public bool ThrowableEquiped()
    {
        return throwableEquiped;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            if (collision.gameObject.GetComponent<Pickup>().Name == "Tomato")
            {
                coolDown = -1;
                attacking = false;
                playerMovement.playerSpeed = 4.0f;
                playerMovement.playerRotation = 7.0f;
            }

        }
    }



}
