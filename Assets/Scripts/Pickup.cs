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
        //hello sam
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
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void Drop()
    {
        pickedUp = false;

    }

    public void Take()
    {
        if(!pickedUp)
        {
            if(rb)
            {
                rb.useGravity = false;
                rb.gameObject.GetComponent<MeshCollider>().enabled = false;
            }

            pickedUp = true;
            gm.HideInteractText();
            SoundManager.instance.PlaySound(PickupSound, transform, 0.15f);
            rotationSpeed = 0.0f;
            transform.SetParent(playerHand.transform, false);
            transform.localPosition = Vector3.zero;
        }
    }

    public void AddToInventory()
    {
        playerHand = GameObject.Find("Player").GetComponent<Player>().handSocket;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.gameObject.GetComponent<MeshCollider>().enabled = false;
        
        pickedUp = true;
        rotationSpeed = 0.0f;
        transform.SetParent(playerHand.transform, false);
        transform.localPosition = Vector3.zero;
    }

    public void Equip()
    {
        transform.SetParent(playerHand.transform, false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            rb = gameObject.GetComponent<Rigidbody>();

            if(rb)
            {
                if(rb.linearVelocity.magnitude < 0.2f)
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

    private void OnCollisionEnter(Collision collision)
    {
        pickedUp = false;
    }
}
