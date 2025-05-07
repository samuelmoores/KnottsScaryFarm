using UnityEngine;

public class Block : MonoBehaviour
{
    public AudioClip sound;

    bool beenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("PuzzlePiece") && !beenHit)
        {
            SoundManager.instance.PlaySound(sound, transform);
            GameManager_Level_01.instance.Score();
            beenHit = true;

            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Material[] mats = renderer.materials;
            mats[0] = mats[3];  // Or any other material
            renderer.materials = mats;
        }
    }
}
