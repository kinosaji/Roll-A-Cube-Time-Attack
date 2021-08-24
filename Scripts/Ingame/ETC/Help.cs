using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    [SerializeField] GameObject Tutorial1;
    [SerializeField] GameObject Tutorial2;
    [SerializeField] GameObject Page1;
    [SerializeField] GameObject Page2;

    Color opaque = new Color(1, 1, 1, 1);
    Color transparent = new Color(1, 1, 1, 0.5f);

    Image page1_Image;
    Image page2_Image;
    void Awake()
    {
        page1_Image =  Page1.GetComponent<Image>();
        page2_Image =  Page2.GetComponent<Image>();
    }
    public void ShowPwtutorialUi()
    {
        Tutorial1.SetActive(true);
        Page1.SetActive(true);
        Page2.SetActive(true);
    }
    public void ShowBptutorialUi()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
        page1_Image.color = transparent;
        page2_Image.color = opaque;
    }
    public void CloseHelpUi()
    {
        Tutorial2.SetActive(false);
        page1_Image.color = opaque;
        page2_Image.color = transparent;
        Page1.SetActive(false);
        Page2.SetActive(false);
    }
}
