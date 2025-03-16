using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float rotationSpeed;

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
        pickedUp = true;
        gm.HideInteractText();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.ShowInteractText(gameObject.tag);
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
