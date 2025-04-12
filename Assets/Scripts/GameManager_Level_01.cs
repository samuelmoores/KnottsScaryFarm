using UnityEngine;

public class GameManager_Level_01 : MonoBehaviour
{
    public GameObject Wall;
    public GameObject Wall_Door;

    public static GameManager_Level_01 instance;

    int score_to_win = 3;
    int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
    }

}
