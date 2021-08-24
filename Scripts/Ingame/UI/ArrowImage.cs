using UnityEngine;
using UnityEngine.UI;

public class ArrowImage : MonoBehaviour
{
    [SerializeField] Sprite enterArrow;
    [SerializeField] Sprite exitArrow;

    Sprite[] arrowSprite = new Sprite[2];

    Image arrowImage;
    void Awake()
    {
        arrowImage = GetComponent<Image>();
        arrowSprite[0] = enterArrow;
        arrowSprite[1] = exitArrow;
    }
    public void MobileArrowEnter()
    {
        arrowImage.sprite = arrowSprite[0];
    }
    public void MobileArrowExit()
    {
        arrowImage.sprite = arrowSprite[1];
    }
}
