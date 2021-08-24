using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string SideSkin;
    public string BlackSkin;
    public string WhiteSkin;
    public string BestRecordsData;
    public string PlayerGold;
    public int[] Key;
    public bool HasAds;
}
public class GoogleManager : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] GameObject blackFade_IN;
    [SerializeField] GameObject titleDice;

    Animator fade_IN;
    Animator dice_MOVE;

    public string Player_Name;

    private static GoogleManager instance;
    public static GoogleManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<GoogleManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<GoogleManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    } // SINGLETON
    void Awake()
    {
        var objs = FindObjectsOfType<GoogleManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        fade_IN = blackFade_IN.GetComponent<Animator>();
        dice_MOVE = titleDice.GetComponent<Animator>();
    }
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
        Login();
    }
    void Login()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                LoadCloud();
            }
            else if (Debug.isDebugBuild)
            {
                InGameSceneLoad();
            }
            else
            {
                Application.Quit();
                return;
            }
        });
    }
    void InGameSceneLoad()
    {
        Player_Name = Social.localUser.userName;
        fade_IN.SetTrigger("Fade_In");
        dice_MOVE.SetTrigger("Dice_MOVE");
    }
    public void ReportScore(double score)
    {
        score *= 1000;
        Social.ReportScore((long)score, GPGSIds.leaderboard_ranking, (bool success) =>
        {
            if (success)
            {
                Debug.Log($"Report Score Success : {score}");
            }
        });
    }
    #region 클라우드 저장
    const string fileName = "CloudData2021";
    ISavedGameClient SavedGame()
    {
        return PlayGamesPlatform.Instance.SavedGame;
    }
    public void SaveCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution(fileName,
        DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGame);
    }
    void SaveGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            string jsonData = JsonUtility.ToJson(playerData);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            SavedGame().CommitUpdate(game, update, bytes, SaveData);
        }
    }
    void SaveData(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
        }
    }
    public void LoadCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution(fileName,
                    DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);
    }
    void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadData);
    }
    void LoadData(SavedGameRequestStatus status, byte[] LoadedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string jsonData = System.Text.Encoding.UTF8.GetString(LoadedData);
            if (jsonData == "")
            {
                SaveCloud();
                LoadCloud();
            }
            else
            {
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    InGameSceneLoad();
                }
            }
        }
    }
    #endregion
}