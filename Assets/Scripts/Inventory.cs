using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] buttons;
    Pickup[] items;
    List<GameObject> gameObjects;

    int num_items = 0;
    int selectedItem = 0;

    Sprite corndog;
    Sprite tomato;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        items = new Pickup[4];
        corndog = Resources.Load<Sprite>("Corndog");
        tomato = Resources.Load<Sprite>("Tomato");

        gameObjects = new List<GameObject>(4);


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
       if(num_items <= items.Length)
        {

            Debug.Log(num_items);

            items[num_items] = newItem;
            //gameObjects[num_items] = newItem.gameObject;


            switch (newItem.Name)
            {
                case "corndog":
                    buttons[num_items++].GetComponent<Image>().sprite = corndog;
                    break;
                case "tomato":
                    buttons[num_items++].GetComponent<Image>().sprite = tomato;
                    break;

            }
        }
    }

    public List<GameObject> GetGameObject()
    {
        return gameObjects;
    }

    public void Drop()
    {
        if(num_items > 0)
        {
            items[--num_items] = null;
            buttons[num_items].GetComponent<Image>().sprite = null;
            gameObjects[num_items] = null;

        }
    }

    public void EquipItem(int itemToEquip)
    {
        selectedItem = itemToEquip;
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
