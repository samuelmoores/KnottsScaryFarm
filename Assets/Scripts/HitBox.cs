using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject Drake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Drake.GetComponent<Drake>().HitPlayer(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Drake.GetComponent<Drake>().HitPlayer(false);
        }
    }
}
