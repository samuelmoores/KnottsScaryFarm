using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    Pickup[] items;
    int num_items = 0;

    Sprite corndog;
    GameObject button_0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items = new Pickup[4];
        Debug.Log(items);
        corndog = Resources.Load<Sprite>("Corndog");
        button_0 = GameObject.Find("Button (0)");
        gameObject.SetActive(false);
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
        Debug.Log(items.Length);

       if(num_items < items.Length)
        {
            Debug.Log(newItem);
            items[num_items++] = newItem;
            button_0.GetComponent<Image>().sprite = corndog;
        }
    }

    public void Drop()
    {
        if(num_items > 0)
        {
            items[num_items--] = null;
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
