using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] buttons;
    Pickup[] items;

    int num_items = 0;

    Sprite corndog;
    Sprite tomato;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        items = new Pickup[4];
        corndog = Resources.Load<Sprite>("Corndog");
        tomato = Resources.Load<Sprite>("Tomato");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckInventory()
    {
        return num_items > 0;
    }

    public void Add(Pickup newItem)
    {
        Debug.Log(newItem.Name);

       if(num_items <= items.Length)
        {
            switch(newItem.Name)
            {
                case "corndog":
                    items[num_items] = newItem;
                    buttons[num_items++].GetComponent<Image>().sprite = corndog;
                    break;
                case "tomato":
                    items[num_items] = newItem;
                    buttons[num_items++].GetComponent<Image>().sprite = tomato;
                    break;

            }
        }
    }

    public void Drop()
    {
        if(num_items > 0)
        {
            items[--num_items] = null;
            buttons[num_items].GetComponent<Image>().sprite = null;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
