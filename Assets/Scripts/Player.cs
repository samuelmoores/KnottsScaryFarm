using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pauseMenu;
    Inventory inventory;
    Pickup pickup;
    bool foundPickup = false;
    bool isInteracting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {

            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.GetComponent<PauseMenu>().Hide();
            }
            else
            {
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                pauseMenu.GetComponent<PauseMenu>().Show();
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
