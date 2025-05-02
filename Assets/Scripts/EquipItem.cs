using UnityEngine;

public class EquipItem : MonoBehaviour
{
    Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedItem(int selected_item)
    {
        inventory.EquipItem(selected_item);
    }
}
