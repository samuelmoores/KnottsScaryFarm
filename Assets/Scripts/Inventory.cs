using NUnit.Framework;
using System.Linq.Expressions;
using TMPro;
using Unity.UI;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject Coin_Prefab;
    public GameObject Tomato_Prefab;
    public GameObject Corndog_Prefab;
    public GameObject CottonCandy_Prefab;


    public GameObject[] images;
    public GameObject[] numbers;

    TextMeshProUGUI[] numbers_text;
    GameObject[] item_0;
    GameObject[] item_1;
    GameObject[] item_2;
    GameObject[] item_3;

    static int num_items = 0;
    static int item_0_amount = 0;
    static int item_1_amount = 0;
    static int item_2_amount = 0;
    static int item_3_amount = 0;

    static string item_0_name;
    static string item_1_name;
    static string item_2_name;
    static string item_3_name;


    int selected_item = 0;
    Sprite corndog;
    Sprite tomato;
    Sprite cotton_candy;
    Sprite coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        corndog = Resources.Load<Sprite>("Corndog");
        tomato = Resources.Load<Sprite>("Tomato");
        cotton_candy = Resources.Load<Sprite>("CottonCandy");
        coin = Resources.Load<Sprite>("Coin");

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

        int[] itemAmounts = { item_0_amount, item_1_amount, item_2_amount, item_3_amount };
        GameObject[][] items = { item_0, item_1, item_2, item_3 };
        string[] names = { item_0_name, item_1_name, item_2_name, item_3_name };

        for(int i = 0; i < num_items; i++)
        {

            if (itemAmounts[i] > 0)
            {
                numbers_text[i].gameObject.transform.parent.gameObject.SetActive(true);
                numbers_text[i].text = itemAmounts[i].ToString();

                switch(names[i])
                {
                    case "Coin":
                        images[i].GetComponent<Image>().sprite = coin;

                        for (int j = 0; j < itemAmounts[i]; j++)
                        {
                            items[i][j] = GameObject.Instantiate(Coin_Prefab, Vector3.zero, Quaternion.identity);
                            items[i][j].GetComponent<Pickup>().AddToInventory();
                        }
                        break;

                    case "Tomato":
                        images[i].GetComponent<Image>().sprite = tomato;

                        for (int j = 0; j < itemAmounts[i]; j++)
                        {
                            items[i][j] = GameObject.Instantiate(Tomato_Prefab, Vector3.zero, Quaternion.identity);
                            items[i][j].GetComponent<Pickup>().AddToInventory();
                        }
                        break;

                    case "Corndog":
                        images[i].GetComponent<Image>().sprite = corndog;

                        for (int j = 0; j < itemAmounts[i]; j++)
                        {
                            items[i][j] = GameObject.Instantiate(Corndog_Prefab, Vector3.zero, Quaternion.identity);
                            items[i][j].GetComponent<Pickup>().AddToInventory();
                        }
                        break;

                    case "CottonCandy":
                        images[i].GetComponent<Image>().sprite = cotton_candy;

                        for (int j = 0; j < itemAmounts[i]; j++)
                        {
                            items[i][j] = GameObject.Instantiate(CottonCandy_Prefab, Vector3.zero, Quaternion.identity);
                            items[i][j].GetComponent<Pickup>().AddToInventory();
                        }
                        break;
                }

            }

        }

        EquipItem(0);
    }

    public void Add(Pickup NewItem, bool won = false)
    {
        GameObject[] items_first = { item_0[0], item_1[0], item_2[0], item_3[0] };
        bool already_has_item = false;
        bool foundAvailableSlot = false;

        for(int i = 0; i < 4; i++)
        {
            if (items_first[i] != null)
            {
                Pickup pickup = items_first[i].GetComponent<Pickup>();

                //is the item being added already in the inventory?
                if(pickup.Name == NewItem.Name)
                {
                    already_has_item = true;
                    selected_item = i;
                    break;
                }
            }//if it's not, put it in first available slot
            else if(!foundAvailableSlot && !already_has_item)
            {
                selected_item = i;
                foundAvailableSlot = true;
            }
        }

        string[] names = { item_0_name, item_1_name, item_2_name, item_3_name };

        //adds new image to inventory
        if(!already_has_item)
        {
            num_items++;

            switch(NewItem.Name)
            {
                case "Corndog":
                    images[selected_item].GetComponent<Image>().sprite = corndog;
                    names[selected_item] = "Corndog";
                    break;
                case "Tomato":
                    images[selected_item].GetComponent<Image>().sprite = tomato;
                    names[selected_item] = "Tomato";
                    break;
                case "CottonCandy":
                    images[selected_item].GetComponent<Image>().sprite = cotton_candy;
                    names[selected_item] = "CottonCandy";
                    break;
                case "Coin":
                    images[selected_item].GetComponent<Image>().sprite = coin;
                    names[selected_item] = "Coin";
                    break;
            }
        }

        //add item to inventory
        switch(selected_item)
        {
            case 0:
                if(item_0_amount < 10)
                {
                    item_0[item_0_amount++] = NewItem.gameObject;
                    numbers_text[0].gameObject.transform.parent.gameObject.SetActive(true);
                    numbers_text[0].text = item_0_amount.ToString();
                    item_0_name = names[0];
                }
                 break;
            case 1:
                if(item_1_amount < 10)
                {
                    item_1[item_1_amount++] = NewItem.gameObject;

                    numbers_text[1].gameObject.transform.parent.gameObject.SetActive(true);
                    numbers_text[1].text = item_1_amount.ToString();
                    item_1_name = names[1];

                }
                 break;
            case 2:
                if (item_2_amount < 10)
                {
                    item_2[item_2_amount++] = NewItem.gameObject;

                    numbers_text[2].gameObject.transform.parent.gameObject.SetActive(true);
                    numbers_text[2].text = item_2_amount.ToString();
                    item_2_name = names[2];

                }
                break;
            case 3:
                if (item_3_amount < 10)
                {
                    item_3[item_3_amount++] = NewItem.gameObject;

                    numbers_text[3].gameObject.transform.parent.gameObject.SetActive(true);
                    numbers_text[3].text = item_3_amount.ToString();
                    item_3_name = names[3];

                }
                break;

        }

        //sets all new items to active, all others to not active
        
        if(!won)
        {
            EquipItem(selected_item);
        }
        else
        {
            EquipItem(selected_item);
        }

    }

    
    public void Drop()
    {
        Destroy(item_0[0]);
        Use();
    }

    public GameObject Use()
    {
        if(num_items > 0)
        {
            string[] names = { item_0_name, item_1_name, item_2_name, item_3_name };

            Debug.Log("Get rid of item: " + selected_item);

            switch (selected_item)
            {
                case 0:
                    GameObject obj = item_0[--item_0_amount];
                    item_0[item_0_amount] = null;
                    numbers_text[0].text = item_0_amount.ToString();
                    names[0] = "";

                    Debug.Log("item 0 amount: " + item_0_amount);

                    if (item_0_amount == 0)
                    {
                        num_items--;
                        GameObject.Find("Player").GetComponent<PlayerAttack>().UnequipThrowable();
                        numbers_text[0].gameObject.transform.parent.gameObject.SetActive(false);
                        images[0].GetComponent<Image>().sprite = null;
                    }

                    return obj;

                case 1:
                    obj = item_1[--item_1_amount];
                    item_1[item_1_amount] = null;
                    numbers_text[1].text = item_1_amount.ToString();
                    names[1] = "";

                    if (item_1_amount == 0)
                    {
                        num_items--;
                        GameObject.Find("Player").GetComponent<PlayerAttack>().UnequipThrowable();
                        numbers_text[1].gameObject.transform.parent.gameObject.SetActive(false);
                        images[1].GetComponent<Image>().sprite = null;
                    }

                    return obj;

                case 2:
                    obj = item_2[--item_2_amount];
                    item_2[item_2_amount] = null;
                    numbers_text[2].text = item_2_amount.ToString();
                    names[2] = "";

                    if (item_2_amount == 0)
                    {
                        num_items--;
                        GameObject.Find("Player").GetComponent<PlayerAttack>().UnequipThrowable();
                        numbers_text[2].gameObject.transform.parent.gameObject.SetActive(false);
                        images[2].GetComponent<Image>().sprite = null;
                    }

                    return obj;

                case 3:
                    obj = item_3[--item_3_amount];
                    item_3[item_3_amount] = null;
                    numbers_text[3].text = item_2_amount.ToString();
                    names[3] = "";

                    if (item_3_amount == 0)
                    {
                        num_items--;
                        GameObject.Find("Player").GetComponent<PlayerAttack>().UnequipThrowable();
                        numbers_text[3].gameObject.transform.parent.gameObject.SetActive(false);
                        images[3].GetComponent<Image>().sprite = null;
                    }

                    return obj;
            }
        }
        return null;
    }

    public void EquipItem(int selected_item)
    {
        this.selected_item = selected_item;

        switch (selected_item)
        {
            case 0:
                for(int i = 0; i < item_0_amount; i++)
                {
                    item_0[i].SetActive(true);
                }

                for (int i = 0; i < item_1_amount; i++)
                {
                    item_1[i].SetActive(false);
                }

                for (int i = 0; i < item_2_amount; i++)
                {
                    item_2[i].SetActive(false);
                }

                for (int i = 0; i < item_3_amount; i++)
                {
                    item_3[i].SetActive(false);
                }

                if (item_0[0] != null)
                {
                    Pickup pickup = item_0[0].GetComponent<Pickup>();

                    if(pickup.Name != "CottonCandy")
                    {
                        GameObject.Find("Player").GetComponent<PlayerAttack>().EquipThrowable();
                    }
                }

                break;
            case 1:
                for (int i = 0; i < item_0_amount; i++)
                {
                    item_0[i].SetActive(false);
                }

                for (int i = 0; i < item_1_amount; i++)
                {
                    item_1[i].SetActive(true);
                }

                for (int i = 0; i < item_2_amount; i++)
                {
                    item_2[i].SetActive(false);
                }

                for (int i = 0; i < item_3_amount; i++)
                {
                    item_3[i].SetActive(false);
                }

                if (item_1[0] != null)
                {
                    Pickup pickup = item_1[0].GetComponent<Pickup>();

                    if (pickup.Name != "CottonCandy")
                    {
                        GameObject.Find("Player").GetComponent<PlayerAttack>().EquipThrowable();
                    }
                }

                break;

            case 2:
                for (int i = 0; i < item_0_amount; i++)
                {
                    item_0[i].SetActive(false);
                }

                for (int i = 0; i < item_1_amount; i++)
                {
                    item_1[i].SetActive(false);
                }

                for (int i = 0; i < item_2_amount; i++)
                {
                    item_2[i].SetActive(true);
                }

                for (int i = 0; i < item_3_amount; i++)
                {
                    item_3[i].SetActive(false);
                }

                if (item_2[0] != null)
                {
                    Pickup pickup = item_2[0].GetComponent<Pickup>();

                    if (pickup.Name != "CottonCandy")
                    {
                        GameObject.Find("Player").GetComponent<PlayerAttack>().EquipThrowable();
                    }
                }

                break;

            case 3:
                for (int i = 0; i < item_0_amount; i++)
                {
                    item_0[i].SetActive(false);
                }

                for (int i = 0; i < item_1_amount; i++)
                {
                    item_1[i].SetActive(false);
                }

                for (int i = 0; i < item_2_amount; i++)
                {
                    item_2[i].SetActive(false);
                }

                for (int i = 0; i < item_3_amount; i++)
                {
                    item_3[i].SetActive(true);
                }

                if (item_3[0] != null)
                {
                    Pickup pickup = item_3[0].GetComponent<Pickup>();

                    if (pickup.Name != "CottonCandy")
                    {
                        GameObject.Find("Player").GetComponent<PlayerAttack>().EquipThrowable();
                    }
                }

                break;
        }
    }

    public float Heal()
    {
        GameObject[] objs = { item_0[0], item_1[0], item_2[0], item_3[0] };
        Pickup[] pickups = new Pickup[4];

        for(int i = 0; i < 4; i++)
        {
            if (objs[i] != null)
            {
                pickups[i] = objs[i].GetComponent<Pickup>();
                if (pickups[i].Name == "CottonCandy")
                {
                    return 0.25f;
                }
            }
        }

        return 0.0f; 
    }

    public void DropHeal()
    {
        GameObject[] objs = { item_0[0], item_1[0], item_2[0], item_3[0] };
        Pickup[] pickups = new Pickup[4];

        for (int i = 0; i < 4; i++)
        {
            if (objs[i] != null)
            {
                pickups[i] = objs[i].GetComponent<Pickup>();
                if (pickups[i].Name == "CottonCandy")
                {
                    pickups[i].Drop();
                    Use();
                    Rigidbody rb = objs[i].GetComponent<Rigidbody>();
                    rb.useGravity = true;
                    objs[i].GetComponent<MeshCollider>().enabled = true;

                    Vector3 worldPos = objs[i].transform.position;
                    objs[i].transform.parent = null;
                    objs[i].transform.position = worldPos;
                    objs[i] = null;
                }
            }
        }
    }

    public int CheckCoinAmount()
    {

        if (num_items == 0)
            return 0;
        else
        {
            GameObject[][] items = { item_0, item_1, item_2, item_3 };
            int[] itemAmounts = { item_0_amount, item_1_amount, item_2_amount, item_3_amount };

            for (int i = 0; i < num_items; i++)
            {
                Pickup pickup = items[i][0].GetComponent<Pickup>();

                if (pickup.Name == "Coin")
                    return itemAmounts[i];
            }
        }

        return 0;
    }

    public bool TakeCoin()
    {
        GameObject[][] items = { item_0, item_1, item_2, item_3 };
        int[] items_amounts = { item_0_amount, item_1_amount, item_2_amount, item_3_amount };

        for (int i = 0; i < 4; i++)
        {
            if (items[i][0] != null)
            {
                Pickup pickup = items[i][0].GetComponent<Pickup>();

                if(pickup.Name == "Coin")
                {
                    Destroy(items[i][items_amounts[i] - 1]);
                    items[i][items_amounts[i] - 1] = null;

                    int selected_item_saved = selected_item;
                    selected_item = i;
                    Use();

                    selected_item = selected_item_saved;

                    return true;
                }

            }
        }
        return false;
    }

    public bool CheckInventory()
    {
        if(num_items == 0)
        {
            return false;
        }
        else
        {
            GameObject[][] items = { item_0, item_1, item_2, item_3 };

            for (int i = 0; i < num_items; i++)
            {
                if (items[i][0].GetComponent<Pickup>().Name == "Corndog")
                {
                    return true;
                }
            }
        }

        return false;
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
