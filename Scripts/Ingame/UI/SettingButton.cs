using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] GameObject settingUi_Ads_go; // 광고있을때
    [SerializeField] GameObject settingUi_go; // 광고없을때
    [SerializeField] GameObject disableUi_go;

    [SerializeField] Sprite openGear;
    public void GameSetting()
    {
        FindObjectOfType<AudioManager>().Play("UiTouch");

        GetComponent<Image>().sprite = openGear;
        disableUi_go.SetActive(true);

        if (GoogleManager.Instance.playerData.HasAds)
        { settingUi_Ads_go.SetActive(true); }
        else
        { settingUi_go.SetActive(true); }
    }
}
