using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string Name;
    public float rotationSpeed;
    public AudioClip PickupSound;

    GameObject playerHand;
    GameManager gm;
    Rigidbody rb;

    bool pickedUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerHand = GameObject.Find("Player").GetComponent<Player>().handSocket;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pickedUp)
        {
            transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
        }
    }

    public void Take()
    {
        if(!pickedUp)
        {
            pickedUp = true;
            gm.HideInteractText();
            SoundManager.instance.PlaySound(PickupSound, transform, 0.5f);
            rotationSpeed = 0.0f;
            transform.position = Vector3.zero;
            if(rb)
            {
                rb.useGravity = false;
                rb.gameObject.GetComponent<MeshCollider>().enabled = false;
                transform.localScale = new Vector3(25, 25, 25);
                GameObject.Find("Player").GetComponent<PlayerMovement>().hasWeapin = true;
            }
            transform.SetParent(playerHand.transform, false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            rb = gameObject.GetComponent<Rigidbody>();

            if(rb)
            {
                if(rb.linearVelocity == Vector3.zero)
                {
                    gm.ShowInteractText(Name);
                }
            }
            else
            {
                gm.ShowInteractText(Name);

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.HideInteractText();
        }
    }
}
