using UnityEngine;

public class CottonCandyDisppenser : MonoBehaviour
{
    public GameObject cotton_candy_prefab;
    public Transform dispense_location;
    GameObject cotton_candy;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        if(other.CompareTag("Player"))
        {
            Dispense();
        }
    }
}
