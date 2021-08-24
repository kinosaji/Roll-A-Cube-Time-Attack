using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bombTimer;
    [SerializeField] GameObject aim;

    TextMeshProUGUI bombTimer_Text;
    Image aim_Image;
    Cannon cannon;

    float currentTime;
    bool isBurning;
    bool runOnce;

    void Awake()
    {
        bombTimer_Text = bombTimer.GetComponent<TextMeshProUGUI>();
        aim_Image = aim.GetComponent<Image>();
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
    }
    void OnEnable()
    {
        currentTime = 3.5f;
        aim_Image.color = new Color(1, 1, 1);
        runOnce = true;
        isBurning = true;
    }
    void Update()
    {
        if (isBurning)
        {
            currentTime -= 2 * Time.deltaTime;
            bombTimer_Text.text = currentTime.ToString("N0");
        }
        else
        {
            bombTimer_Text.text = "0";
        }

        if (currentTime <= 0)
        {
            if (runOnce)
            {
                runOnce = false;
                isBurning = false;
                cannon.IsTrigger = true;
                aim_Image.color = new Color(1, 0, 0);
            }
        }
    }
}