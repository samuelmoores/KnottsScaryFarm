using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject interactObject;
    TextMeshProUGUI interactText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactObject = GameObject.Find("Interact");
        interactText = GameObject.Find("InteractText").GetComponent<TextMeshProUGUI>();
        interactObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

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
