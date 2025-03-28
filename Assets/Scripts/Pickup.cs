using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string Name;
    public float rotationSpeed;
    public AudioClip PickupSound;

    GameManager gm;

    bool pickedUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            Destroy(gameObject, PickupSound.length);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.ShowInteractText(Name);
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
