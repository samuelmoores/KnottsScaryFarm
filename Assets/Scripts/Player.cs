using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject inventory_obj;
    Inventory inventory;
    Pickup pickup;
    bool foundPickup = false;
    bool isInteracting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = inventory_obj.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    public bool GetIsInteracting()
    {
        return isInteracting;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    void ProcessInput()
    {
        //bring up inventory
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventory.gameObject.activeSelf)
            {
                inventory.Show();

            }
            else
            {
                inventory.Hide();
            }
        }

        //grab a pickup
        if(foundPickup)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                inventory.Add(pickup);
                pickup.Take();
                foundPickup = false; 
            }
        }

        //interacting
        isInteracting = Input.GetKeyDown(KeyCode.E);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup"))
        {
            foundPickup = true;
            pickup = other.gameObject.GetComponent<Pickup>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            foundPickup = false;
            pickup = null;
        }
    }
}
