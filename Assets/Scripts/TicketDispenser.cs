using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TicketDispenser : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    GameManager gm;
    Player player;
    static int num_coins = 0;
    bool foundPlayer = false;
    float GoingToSeeBrianTimer = -1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        textMeshPro.text = num_coins.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = num_coins.ToString();

        if(player.GetIsInteracting() && foundPlayer)
        {
            Inventory inventory = player.GetInventory();

            if (inventory.TakeCoin())
            {
                num_coins++;

                if (num_coins >= 3)
                {
                    player.gameObject.GetComponent<PlayerHealth>().GetExcitedToGoSeeBrian();
                    player.gameObject.GetComponent<Animator>().SetBool("DanceForBrian", true);
                    gm.HideInteractText();
                    GoingToSeeBrianTimer = 4.0f;
                }
            }
        }

        if(GoingToSeeBrianTimer > 0.0f)
        {
            GoingToSeeBrianTimer -= Time.deltaTime;
        }
        else if(GoingToSeeBrianTimer > -0.2f && GoingToSeeBrianTimer < 0.0f)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gm.ShowInteractText("Go See Brian\n---\n3 Coins");

            foundPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gm.HideInteractText();
            foundPlayer = false;
        }
    }
}
