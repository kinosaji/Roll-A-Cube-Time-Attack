using UnityEngine;

public enum ColorType
{
    life,
    power,
    blue,
    speed,
    slow
}
public class DiceCover : MonoBehaviour
{
    public ColorType colorType;

    float Transpaency_Speed;

    Transform Dice;
    Material mat;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }
    void Start()
    {
        Dice = GameObject.Find("Dice").transform;
    }
    void OnEnable()
    {
        Transpaency_Speed = 30;

        mat.color = CoverColor(colorType);

        if (colorType == ColorType.slow)
        {
            Transpaency_Speed = 10;

            if (!GameManager.Instance.IsSlowMove) { GameManager.Instance.IsSlowMove = true; }
        }
        else
        {
            GameManager.Instance.IsUgrading = true;
        }
    }
    void OnDisable()
    {
        if (GameManager.Instance.IsUgrading) { GameManager.Instance.IsUgrading = false; }
        if (GameManager.Instance.IsSlowMove) { GameManager.Instance.IsSlowMove = false; }
    }
    void Update()
    {
        transform.position = Dice.position;

        mat.color -= new Color(0, 0, 0, 0.05f) * Transpaency_Speed * Time.deltaTime;
        if (mat.color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    Color lifeColor = new Color(1, 0, 0, 1);
    Color powerColor = new Color(1, 0, 1, 1);
    Color blueColor = new Color(0, 0, 1, 1);
    Color speedColor = new Color(0, 1, 1, 1);
    Color slowColor = new Color(0, 1, 0, 1);
    Color CoverColor(ColorType type)
    {
        Color currentColor = Color.clear;

        switch (type)
        {
            case ColorType.life:
                currentColor = lifeColor;
                break;
            case ColorType.power:
                currentColor = powerColor;
                break;
            case ColorType.blue:
                currentColor = blueColor;
                break;
            case ColorType.speed:
                currentColor = speedColor;
                break;
            case ColorType.slow:
                currentColor = slowColor;
                break;
        }
        return currentColor;
    }
}
