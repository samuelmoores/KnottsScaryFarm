using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject Coin_Prefab;
    GameObject Coin;
    float spawnInterval = 3.0f;

    Drake Drake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Drake = GameObject.Find("Drake").GetComponent<Drake>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Drake.GetRunTimer() < 0.0f)
        {
            spawnInterval -= Time.deltaTime;

            if(spawnInterval < 0.0f)
            {
                Coin = GameObject.Instantiate(Coin_Prefab, transform.position, Quaternion.identity);
                Rigidbody rb = Coin.GetComponent<Rigidbody>();
                rb.angularVelocity = new Vector3(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f));

                float y = Random.Range(0.0f, 359.0f);

                transform.Rotate(new Vector3(0.0f, y, 0.0f));

                rb.AddForce(transform.forward * Random.Range(500.0f, 800.0f));
                spawnInterval = 3.0f;
            }
        }
    }
}
