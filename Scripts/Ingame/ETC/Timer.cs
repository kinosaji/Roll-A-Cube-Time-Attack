using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    [SerializeField] Transform GoldBasket;
    [SerializeField] TextMeshProUGUI bestRecordsText;
    [SerializeField] GameObject field_go;
    [SerializeField] GameObject pauseBtn;

    TextMeshProUGUI timerText;
    Animator bestRecordsAni;
    Field field;
    Cannon cannon;

    const float PAUSE_BTN_SPAWN = 5;

    bool runOnce_PauseBtn;

    float currentSeconds = 0;
    float bestRecords;

    delegate void TimeAttack();
    TimeAttack timeAttack;

    bool GameOver_runOnce = true;
    void Awake()
    {
        bestRecordsText = bestRecordsText.GetComponent<TextMeshProUGUI>();
        timerText = GetComponent<TextMeshProUGUI>();
        bestRecordsAni = bestRecordsText.GetComponent<Animator>();
        field = field_go.GetComponent<Field>();
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
    }
    void Start()
    {
        GameManager.Instance.NewRecords = 0;
        GameManager.Instance.CurrentGold = 0;

        GameManager.Instance.BestRecords = float.Parse(GoogleManager.Instance.playerData.BestRecordsData);
        bestRecords = GameManager.Instance.BestRecords;
        bestRecordsText.text = TimeSpan.FromSeconds(bestRecords).ToString(@"mm\:ss\:ff");

        whiteSpawn = UnityEngine.Random.Range(1, 5);
        greenSpawn = UnityEngine.Random.Range(10, 15);
        redSpawn = UnityEngine.Random.Range(5, 30);
        blueSpawn = UnityEngine.Random.Range(90, 120);
        cannonFire = UnityEngine.Random.Range(1, 30);
        goldSpawnTime = UnityEngine.Random.Range(goldSpawnTimeMin, goldSpawnTimeMax);

        timeAttack = WatingRoom;
        timeAttack += EnterLevel;
        timeAttack += RecordScore;
        timeAttack += LevelAdjustment;

        runOnce_PauseBtn = true;
    }
    void Update()
    {
        timeAttack();
    }
    void WatingRoom()
    {
        if (GameManager.Instance.WatingRoom)
        {
            timerText.text = DateTime.Now.ToString("HH:mm:ss");
        }
        else if (!GameManager.Instance.WatingRoom && currentSeconds == 0)
        {
            timerText.text = "";
        }
        else { timeAttack -= WatingRoom; }
    }
    void RecordScore()
    {
        if (currentSeconds > bestRecords) // 신기록 갱신
        { bestRecordsText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss\:ff"); }

        if (GameManager.Instance.GameOver)
        {
            if (GameOver_runOnce)
            {
                GameOver_runOnce = false;
                timerText.text = "GAME OVER";
                GameManager.Instance.CurrentRecords = currentSeconds; // 현재기록

                if (currentSeconds > bestRecords) // 신기록이었다면
                {
                    NewRecords(currentSeconds);
                }
            }
        }
    }
    void testRecords(float _newRecords) // 테스트 ReportScore
    {
        Debug.Log($"_newRecords : {_newRecords}");
        double newRecords = Math.Truncate(_newRecords * 100) / 100;
        Debug.Log($"newRecords : {newRecords}");
        _newRecords *= 1000;
        long reportRecords = (long)_newRecords;
        Debug.Log($"longRecords : {reportRecords}");
    }
    void NewRecords(float _newRecords)
    {
        bestRecordsAni.SetTrigger("ScoreSetting");
        GameManager.Instance.NewRecords = _newRecords;
        double newRecords = Math.Truncate(_newRecords * 100) / 100;
        GoogleManager.Instance.playerData.BestRecordsData = newRecords.ToString();
        GoogleManager.Instance.SaveCloud();
        GoogleManager.Instance.ReportScore(newRecords);
    }
    #region // Level Design

    int difficulty_Level = 1;
    float levelSeconds = 60;

    float whiteSpawn, greenSpawn, blueSpawn, redSpawn, cannonFire; // 공격요소

    float whiteRandomTime, whiteRandomMin = 20, whiteRandomMax = 25; // 화이트

    float greenRandomTime, greenRandomMin = 25, greenRandomMax = 30; // 그린

    float blueRandomTime, blueRandomMin = 30, blueRandomMax = 50; // 블루

    float redRandomTime, redRandomMin = 10, redRandomMax = 30; // 레드

    float cannonRandomTime, cannonRandomMin = 10, cannonRandomMax = 30; // 캐넌

    float deleteFieldTime = 60;

    void EnterLevel()
    {
        if (GameManager.Instance.GameStart)
        {
            currentSeconds += Time.deltaTime;
            if (Time.timeScale != 0)
            {
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss\:ff");
            }
            else
            {
                timerText.text = "";
            }
            if (currentSeconds > PAUSE_BTN_SPAWN)
            {
                if (runOnce_PauseBtn)
                {
                    runOnce_PauseBtn = false;
                    pauseBtn.SetActive(true);
                }
            }
            if (currentSeconds > whiteSpawn) // 화이트
            {
                whiteRandomTime = UnityEngine.Random.Range(whiteRandomMin, whiteRandomMax);
                whiteSpawn += whiteRandomTime;
                field.RandomBlock("whiteBall");
            }

            if (currentSeconds > greenSpawn) // 그린
            {
                greenRandomTime = UnityEngine.Random.Range(greenRandomMin, greenRandomMax);
                greenSpawn += greenRandomTime;
                field.RandomBlock("greenBall");
            }

            if (currentSeconds > blueSpawn) // 블루 
            {
                blueRandomTime = UnityEngine.Random.Range(blueRandomMin, blueRandomMax);
                blueSpawn += blueRandomTime;
                field.RandomBlock("blueBall");
            }

            if (currentSeconds > redSpawn) // 레드
            {
                redRandomTime = UnityEngine.Random.Range(redRandomMin, redRandomMax);
                redSpawn += redRandomTime;
                field.RandomBlock("redBall");
            }
            if (currentSeconds > cannonFire) // 캐넌
            {
                cannonRandomTime = UnityEngine.Random.Range(cannonRandomMin, cannonRandomMax);
                cannonFire += cannonRandomTime;
                cannon.SetTarget();
            }
            if (currentSeconds > deleteFieldTime) // 필드삭제
            {
                deleteFieldTime += 50;
                field.RandomBlock("deleteField");
            }

            if (currentSeconds > goldSpawnTime)
            {
                goldSpawnTime += UnityEngine.Random.Range(goldSpawnTimeMin, goldSpawnTimeMax);
                GoldSpawn();
            }
        }
    }
    void LevelAdjustment()
    {
        if (currentSeconds > levelSeconds)
        {
            difficulty_Level++;
            levelSeconds += 60;
            if (difficulty_Level > 10)
            {
                timeAttack -= LevelAdjustment;
                return;
            }
            whiteRandomMin--;
            whiteRandomMax--;

            greenRandomMin++;
            greenRandomMax--;

            blueRandomMin++;
            blueRandomMax--;

            redRandomMin++;
            redRandomMax--;

            cannonRandomMax--;

            goldSpawnTimeMin--;
            goldSpawnTimeMax--;
        }
    }
    #endregion


    // Gold
    int goldSpawnTime;
    int goldSpawnTimeMin = 20;
    int goldSpawnTimeMax = 31;
    void GoldSpawn()
    {
        // Sound

        float randXpos = UnityEngine.Random.Range(-4.5f, 4.3f);
        float randZpos = UnityEngine.Random.Range(-5, 4f);

        GameObject _gold = transform.GetChild(0).gameObject;

        _gold.transform.SetParent(GoldBasket);
        _gold.transform.position = new Vector3(randXpos, 0, randZpos);
        _gold.SetActive(true);
    }
}
