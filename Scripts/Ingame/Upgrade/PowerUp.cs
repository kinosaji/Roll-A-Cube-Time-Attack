using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] RectTransform WaitingRoomUi;
    [SerializeField] GameObject Ingame_UI;
    [SerializeField] GameObject DisableUi;

    [SerializeField] Material skin_Default;
    [SerializeField] Transform s_DiceManagerTransform;
    [SerializeField] Transform s_RightTarget;
    [SerializeField] GameObject startKey_go;
    [SerializeField] GameObject s_UpCheck;

    [SerializeField] GameObject[] diceSidePower1;
    [SerializeField] GameObject[] diceSidePower2;

    [SerializeField] GameObject HelpIcon;

    Animator IngameUi_Ani;
    ParticleSystem pupleUpSparks;

    bool isHide;
    bool runOnce = true;

    void Awake()
    {
        IngameUi_Ani = Ingame_UI.GetComponent<Animator>();
        pupleUpSparks = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dice"))
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");

            if (runOnce)
            {
                runOnce = false;
                DisableUi.SetActive(true);
                HelpIcon.SetActive(false);
                isHide = true;
            }
            pupleUpSparks.Play();
            PowerUpTrigger(other.gameObject);
        }
    }
    void Update()
    {
        if (isHide) { HideWaitingUi(); }
    }

    const float UP_SPEED = 200;
    void HideWaitingUi()
    {
        WaitingRoomUi.anchoredPosition += Vector2.up * UP_SPEED * Time.deltaTime;

        if (WaitingRoomUi.anchoredPosition.y >= 300)
        {
            isHide = false;
            IngameUi_Ani.SetTrigger("If_Ui_Ingame");
            DisableUi.SetActive(false);
        }
    }
    void PowerUpTrigger(GameObject other)
    {
        if (UpgradeManager.Instance.PlayerPower == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                diceSidePower1[i].GetComponent<Renderer>().material = skin_Default;
                GameObject diceSide = other.gameObject.transform.GetChild(i).gameObject;
                diceSide.SetActive(false);
            }
            s_DiceManagerTransform.position = s_RightTarget.position;
            other.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            startKey_go.SetActive(true);
        }
        else if (UpgradeManager.Instance.PlayerPower == 1)
        {
            for (int i = 0; i < 6; i++)
            {
                diceSidePower2[i].GetComponent<Renderer>().material = skin_Default;
                GameObject diceSide = other.gameObject.transform.GetChild(i).gameObject;
                diceSide.SetActive(false);
            }
            s_DiceManagerTransform.position = s_RightTarget.position;
            other.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            s_UpCheck.SetActive(true);
        }
        UpgradeManager.Instance.PowerUp();
    }
}
