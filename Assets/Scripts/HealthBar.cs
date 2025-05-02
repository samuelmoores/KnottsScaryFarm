using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject character;
    public bool is_player;

    PlayerHealth player_health;
    ClownTent enemy_health;
    Slider slider;
    GameObject cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();

        if(is_player)
        {
            player_health = character.GetComponent<PlayerHealth>();
        }
        else
        {
            enemy_health = character.GetComponent<ClownTent>();
            cam = GameObject.Find("Main Camera");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(is_player)
        {
            slider.value = player_health.GetHealth();
        }
        else
        {
            slider.value = enemy_health.GetHealth();
            transform.rotation = cam.transform.rotation;
        }
    }
}
