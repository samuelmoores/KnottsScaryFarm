using NUnit.Framework;
using TMPro;
using Unity.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] images;
    public GameObject[] numbers;

    TextMeshProUGUI[] numbers_text;
    GameObject[] item_0;
    GameObject[] item_1;
    GameObject[] item_2;
    GameObject[] item_3;

    int num_items = 0;
    int selected_item = 0;
    int item_0_amount = 0;
    int item_1_amount = 0;
    int item_2_amount = 0;
    int item_3_amount = 0;


    Sprite corndog;
    Sprite tomato;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        corndog = Resources.Load<Sprite>("Corndog");
        tomato = Resources.Load<Sprite>("Tomato");

        item_0 = new GameObject[10];
        item_1 = new GameObject[10];
        item_2 = new GameObject[10];
        item_3 = new GameObject[10];

        numbers_text = new TextMeshProUGUI[4];

        for(int i = 0; i < 4; i++)
        {
            numbers_text[i] = numbers[i].GetComponent<TextMeshProUGUI>();
            numbers_text[i].gameObject.transform.parent.gameObject.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add(Pickup NewItem)
    {
        if(num_items == 0)
        {
            num_items++;

            switch(NewItem.Name)
            {
                case "Corndog":
                    images[0].GetComponent<Image>().sprite = corndog;
                    break;
                case "Tomato":
                    images[0].GetComponent<Image>().sprite = tomato;
                    break;

            }
        }


        switch(selected_item)
        {
            case 0:
                if(item_0_amount < 10)
                {
                    item_0[item_0_amount++] = NewItem.gameObject;
                    numbers_text[0].gameObject.transform.parent.gameObject.SetActive(true);

                    numbers_text[0].text = item_0_amount.ToString();
                }
                break;
            case 1:
                item_1_amount++;
                Debug.Log(item_1[0]);
                break;
            case 2:
                item_2_amount++;
                Debug.Log(item_2[0]);
                break;
            case 3:
                item_3_amount++;
                Debug.Log(item_3[0]);
                break;

        }

    }

    
    public void Drop()
    {

    }

    public GameObject Use()
    {
        if(num_items > 0)
        {
            switch(selected_item)
            {
                case 0:
                    GameObject obj = item_0[--item_0_amount];
                    item_0[item_0_amount] = null;
                    numbers_text[0].text = item_0_amount.ToString();

                    if(item_0_amount == 0)
                    {
                        num_items--;
                        GameObject.Find("Player").GetComponent<PlayerAttack>().UnequipThrowable();
                        numbers_text[0].gameObject.transform.parent.gameObject.SetActive(false);
                        
                        switch (obj.GetComponent<Pickup>().Name)
                        {
                            case "Corndog":
                                images[0].GetComponent<Image>().sprite = null;
                                break;
                            case "Tomato":
                                images[0].GetComponent<Image>().sprite = null;
                                break;

                        }
                    }

                    return obj;
            }
        }
        return null;
    }

    public bool CheckInventory()
    {
        return num_items > 0;
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
