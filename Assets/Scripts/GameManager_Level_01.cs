using UnityEngine;

public class GameManager_Level_01 : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Wall_Door;
    public GameObject corndog;
    public Transform player_hand;

    public static GameManager_Level_01 instance;

    int score_to_win = 3;
    public int score = 0;
    bool won = false;

    Inventory inventory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wall_Door.SetActive(false);
        Wall.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(score == 3 && !won)
        {
            Win();
        }
    }

    public void Score()
    {
        score++;

        if(score == score_to_win)
        {
            Win();
        }
    }

    public void Win()
    {
        Wall.SetActive(false);
        Wall_Door.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            GameObject corndog_instance = Instantiate(corndog);

            Pickup pickup = corndog_instance.GetComponent<Pickup>();
            inventory.Add(pickup, true);
            pickup.AddToInventory();

        }

        won = true;
    }

}
