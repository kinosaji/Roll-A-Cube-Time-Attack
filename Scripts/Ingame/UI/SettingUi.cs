using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingUi : MonoBehaviour
{
    [SerializeField] GameObject settingButton_go;
    [SerializeField] GameObject soundButton_go;
    [SerializeField] GameObject vibeButton_go;
    [SerializeField] GameObject disableUi_go;

    [SerializeField] Sprite settingClose;
    [SerializeField] Sprite mute;
    [SerializeField] Sprite mute_none;
    [SerializeField] Sprite vibe;
    [SerializeField] Sprite vibe_none;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSound()
    {
        GameManager.Instance.IsMute = !GameManager.Instance.IsMute;

        if (GameManager.Instance.IsMute) // 음소거
        {
            AudioListener.volume = 0;
            soundButton_go.GetComponent<Image>().sprite = mute;
        }
        else // 소리듣기
        {
            AudioListener.volume = 1;
            soundButton_go.GetComponent<Image>().sprite = mute_none;
        }
    }
    public void SetVibe()
    {
        GameManager.Instance.IsVibe = !GameManager.Instance.IsVibe;

        if (GameManager.Instance.IsVibe) // 진동이 있을때
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
            vibeButton_go.GetComponent<Image>().sprite = vibe;
        }
        else // 진동없을때
        {
            vibeButton_go.GetComponent<Image>().sprite = vibe_none;
        }
    }

    public void Close()
    {
        FindObjectOfType<AudioManager>().Play("UiTouch");
        settingButton_go.GetComponent<Image>().sprite = settingClose;
        StartCoroutine(CloseDelay());
    }

    IEnumerator CloseDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        disableUi_go.SetActive(false);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
