using UnityEngine;
using UnityEngine.UI;

public class Prizes : MonoBehaviour
{
    public GameObject Prize_0;
    public GameObject Prize_1;
    public GameObject Prize_2;

    Sprite prize_0_sprite;
    Sprite prize_1_sprite;
    Sprite prize_2_sprite;

    static bool has_prize_0 = false;
    static bool has_prize_1 = false;
    static bool has_prize_2 = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prize_0_sprite = Resources.Load<Sprite>("Prize_0");
        prize_1_sprite = Resources.Load<Sprite>("Prize_1");
        prize_2_sprite = Resources.Load<Sprite>("Prize_2");

        if(has_prize_0)
            Prize_0.GetComponent<Image>().sprite = prize_0_sprite;

        if (has_prize_1)
            Prize_1.GetComponent<Image>().sprite = prize_1_sprite;

        if (has_prize_2)
            Prize_2.GetComponent<Image>().sprite = prize_2_sprite;

    }

    public void SetPrize(int num)
    {
        switch(num)
        {
            case 0:
                Prize_0.GetComponent<Image>().sprite = prize_0_sprite;
                has_prize_0 = true;
                break;
            case 1:
                Prize_1.GetComponent<Image>().sprite = prize_1_sprite;
                has_prize_1 = true;
                break;
            case 2:
                Prize_2.GetComponent<Image>().sprite = prize_2_sprite;
                has_prize_2 = true;
                break;
        }
    }
}
