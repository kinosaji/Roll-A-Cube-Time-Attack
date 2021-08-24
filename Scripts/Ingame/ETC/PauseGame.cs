using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] GameObject backGround;
    [SerializeField] Sprite[] pauseBtnSprite;

    Image image;

    const float COUNT_SPEED = 1.55f;

    bool isPause;
    bool isResume;
    bool run_Once;
    float count;
    int intCount;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        Resume();
    }
    public void Pause()
    {
        isPause = !isPause;

        if (isPause)
        {
            count = 3.99f;
            countText.text = "PAUSE";
            image.sprite = pauseBtnSprite[1];
            backGround.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("CountDown");
            isResume = true;
            run_Once = true;
            StartCoroutine(ITemporarilyInactive());
        }
    }
    void Resume()
    {
        if (isResume)
        {
            count -= COUNT_SPEED * Time.unscaledDeltaTime;
            intCount = (int)count;
            countText.text = intCount.ToString();
            if (count < 1.1f)
            {
                if (run_Once)
                {
                    run_Once = false;
                    isResume = false;
                    countText.text = "";
                    image.sprite = pauseBtnSprite[0];
                    backGround.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }
    IEnumerator ITemporarilyInactive()
    {
        image.raycastTarget = false;
        image.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(60);

        image.raycastTarget = true;
        image.color = new Color(1, 1, 1, 1);
    }
}
