using TMPro;
using UnityEngine;

public class GameManager_Level_01 : MonoBehaviour
{
    GameObject interactObject;
    TextMeshProUGUI interactText;

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
        interactObject = GameObject.Find("Interact");
        interactText = GameObject.Find("InteractText").GetComponent<TextMeshProUGUI>();
        interactObject.SetActive(false);

        Wall_Door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Score()
    {
        score++;

        if(score == 3)
        {
            Win();
        }
    }

    public void Win()
    {
        Wall.SetActive(false);
        Wall_Door.SetActive(true);
    }

    public void ShowInteractText(string newText)
    {
        interactObject.SetActive(true);
        interactText.text = newText;
    }

    public void HideInteractText()
    {
        interactObject.SetActive(false);
        interactText.text = "";
    }

}
