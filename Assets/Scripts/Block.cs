using UnityEngine;

public class Block : MonoBehaviour
{
    public AudioClip sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PuzzlePiece"))
        {
            SoundManager.instance.PlaySound(sound, transform);
            GameManager_Level_01.instance.Score();
        }
    }
}
