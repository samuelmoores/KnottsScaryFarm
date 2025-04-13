using UnityEngine;

public class ArenaEntrance : MonoBehaviour
{
    public Brian brianObject;
    Brian brian;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brian = brianObject.GetComponent<Brian>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PuzzlePiece"))
        {
            brian.InitThunderdome();
        }
    }
}
