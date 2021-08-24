using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region // GAME SET
    public bool GameStart;
    public bool WatingRoom;
    public bool GameOver;

    public bool IsVibe = true;
    public bool IsMute;
    #endregion

    #region // DICE SET
    public bool IsJoystickEnable;
    public bool IsMoving;
    public bool IsMoveEnd;
    public bool DiceTopSideSet;
    public bool IsSphereMove;
    public bool IsUgrading;
    public bool IsSlowMove;
    public bool IsFalling;

    public int SomeKey;
    #endregion

    #region // SCORE SET
    public float CurrentRecords;
    public float BestRecords;
    public float NewRecords;
    #endregion

    public int CurrentGold;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<GameManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<GameManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    } // SINGLETON
    void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    void Update()
    {
        if (IsMoveEnd) // Áü¹ú¶ôÇØ¼Ò
        {
            IsMoving = false;
            DiceTopSideSet = true;
            IsJoystickEnable = true;
            IsMoveEnd = false;
        }
    }
}