using TMPro;
using UnityEngine;

public class Clown : MonoBehaviour
{
    public GameObject secretEntrance;
    public GameObject fence;
    public AudioClip wrongSound;
    public AudioClip rightSound;

    Player player;
    GameManager gm;
    Animator animator;

    bool canAcceptGift = false;
    bool hasGift = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        secretEntrance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canAcceptGift)
        {
            if(player.GetIsInteracting() & !hasGift)
            {
                Inventory inventory = player.GetInventory();
                
                if(inventory.CheckInventory())
                {
                    AcceptGift();
                    inventory.Drop();
                }
                else
                {
                    SoundManager.instance.PlaySound(wrongSound, transform, 0.05f);
                }
            }
        }
    }

    public void AcceptGift()
    {
        SoundManager.instance.PlaySound(rightSound, transform, 0.5f);
        hasGift = true;
        animator.SetBool("takeGift", true);
        gm.HideInteractText();
        fence.SetActive(false);
        secretEntrance.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gm.ShowInteractText("Bestow Gift");
            canAcceptGift = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.HideInteractText();
            canAcceptGift = false;
        }
    }
}
