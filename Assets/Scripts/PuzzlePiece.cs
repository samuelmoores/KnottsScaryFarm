using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    Vector3 startingPosition;
    Rigidbody rb;
    float x;
    float y;
    float z;

    int direction = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition.x = transform.position.x;
        startingPosition.y = transform.position.y;
        startingPosition.z = transform.position.z;

        x = Random.Range(1, 3);
        y = Random.Range(1, 3);
        z = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch(direction)
            {
                case 0:
                    rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    direction++;
                    break;
                case 1:
                    rb.AddForce(Vector3.forward * 5, ForceMode.Impulse);
                    direction++;
                    break;
                case 2:
                    rb.AddForce(Vector3.forward * -5, ForceMode.Impulse);
                    direction++;
                    break;
                case 3:
                    rb.AddForce(Vector3.up * -5, ForceMode.Impulse);
                    direction = 0;
                    break;

            }

        }
        else
        {
            rb.angularVelocity = new Vector3(x, y, z);
        }
    }
}
