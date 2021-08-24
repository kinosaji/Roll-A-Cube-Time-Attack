using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Records : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI current_text;
    [SerializeField] TextMeshProUGUI best_text;

    [SerializeField] Text yourRecords_text;
    [SerializeField] Text gold_text;

    [SerializeField] GameObject restartButton_go;
    [SerializeField] GameObject skipButton_go;
    [SerializeField] GameObject goldObj;

    int timeFlowSpeed = 5;
    float currentFlow;
    float bestFlow;

    string ResultSound = "LoginSuccess";

    bool cur_runOnce = true;
    bool bs1_runOnce = true;
    bool be2_runOnce = true;

    void Awake()
    {
        current_text = current_text.GetComponent<TextMeshProUGUI>();
        best_text = best_text.GetComponent<TextMeshProUGUI>();

        gold_text.text = GameManager.Instance.CurrentGold.ToString();
    }
    void Start()
    {
        if (GameManager.Instance.IsMute)
        { AudioListener.volume = 0; }
        else
        { AudioListener.volume = 1; }

        FindObjectOfType<AudioManager>().Play("GrowTime");
        FindObjectOfType<AudioManager>().Play("GrowBestTime");

        bestFlow = GameManager.Instance.BestRecords;
        best_text.text = TimeSpan.FromSeconds(bestFlow).ToString(@"mm\:ss\:ff");
    }
    void Update()
    {
        RecordsFlow(
            GameManager.Instance.CurrentRecords,
            GameManager.Instance.NewRecords);
    }
    void RecordsFlow(float currentRecords, float newRecords)
    {
        if (currentFlow < currentRecords)
        {
            currentFlow += timeFlowSpeed * Time.deltaTime;
            current_text.text = TimeSpan.FromSeconds(currentFlow).ToString(@"mm\:ss\:ff");
        }
        else
        {
            currentFlow = currentRecords;
            current_text.text = TimeSpan.FromSeconds(currentFlow).ToString(@"mm\:ss\:ff");
            if (cur_runOnce)
            {
                cur_runOnce = false;
                FindObjectOfType<AudioManager>().Stop("GrowTime");
                ShowRestartButton();
            }
        }

        if (newRecords != 0) //신기록을 세웠다면
        {
            if (bestFlow < currentFlow)
            {
                bestFlow += timeFlowSpeed * Time.deltaTime;
                best_text.text = TimeSpan.FromSeconds(bestFlow).ToString(@"mm\:ss\:ff");
                if (bs1_runOnce)
                {
                    bs1_runOnce = false;
                    FindObjectOfType<AudioManager>().Play("GrowBestTime");
                    ResultSound = "NewRecords";
                }
            }
            else if (bestFlow >= newRecords)
            {
                bestFlow = newRecords;
                best_text.text = TimeSpan.FromSeconds(bestFlow).ToString(@"mm\:ss\:ff");
                if (be2_runOnce)
                {
                    be2_runOnce = false;
                    FindObjectOfType<AudioManager>().Stop("GrowBestTime");
                    yourRecords_text.text = "New Records";
                }
            }
        }
        else
        {
            if (be2_runOnce)
            {
                be2_runOnce = false;
                FindObjectOfType<AudioManager>().Stop("GrowBestTime");
            }
        }
    }
    void ShowRestartButton()
    {
        FindObjectOfType<AudioManager>().Play(ResultSound);

        int gold = GameManager.Instance.CurrentGold + int.Parse(GoogleManager.Instance.playerData.PlayerGold);
        GoogleManager.Instance.playerData.PlayerGold = gold.ToString();
        GoogleManager.Instance.SaveCloud(); // For GoldSave
        skipButton_go.SetActive(false);
        goldObj.SetActive(true);
        restartButton_go.SetActive(true);
    }
    public void Skip()
    {
        FindObjectOfType<AudioManager>().Pitch("GrowTime", 4);
        FindObjectOfType<AudioManager>().Pitch("GrowBestTime", 4);
        skipButton_go.SetActive(false);
        timeFlowSpeed = 100;
    }
}
