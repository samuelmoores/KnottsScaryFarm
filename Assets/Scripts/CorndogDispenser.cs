using TMPro;
using UnityEngine;

public class CorndogDispenser : MonoBehaviour
{
    public GameObject CornDog_Prefab;
    public Transform spawnLocation;
    GameObject CornDog;

    GameManager gm;
    Player player;
    int num_coins = 0;
    bool foundPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetIsInteracting() && foundPlayer)
        {
            Inventory inventory = player.GetInventory();

            Debug.Log("The player has: " + inventory.CheckCoinAmount() + " coins");

            if(inventory.CheckCoinAmount() >= 3)
            {
                while (inventory.TakeCoin())
                {
                    num_coins++;

                    if (num_coins >= 3)
                    {
                        gm.HideInteractText();
                        CornDog = GameObject.Instantiate(CornDog_Prefab, spawnLocation.position, Quaternion.identity);
                        Rigidbody rb = CornDog.GetComponent<Rigidbody>();
                        rb.AddForce(spawnLocation.transform.forward * 250.0f);
                        break;
                    }
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.ShowInteractText("Buy Corndog\n---\n3 Coins");

            foundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.HideInteractText();
            foundPlayer = false;
        }
    }
}
