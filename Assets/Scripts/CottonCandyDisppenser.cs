using UnityEngine;

public class CottonCandyDisppenser : MonoBehaviour
{
    public GameObject cotton_candy_prefab;
    public Transform dispense_location;
    GameObject cotton_candy;
    Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dispense()
    {
        cotton_candy = GameObject.Instantiate(cotton_candy_prefab, dispense_location.position, Quaternion.identity);
        Rigidbody rb = cotton_candy.GetComponent<Rigidbody>();
        rb.AddForce(cotton_candy.transform.forward * 100.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && inventory.TakeCoin())
        {
            Dispense();
        }
    }
}
