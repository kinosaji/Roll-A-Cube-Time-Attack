using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] Transform powerTransform;
    [SerializeField] Transform speedTransform;
    [SerializeField] Transform lifeTransform;
    [SerializeField] Transform bluePowerTransform;

    [SerializeField] GameObject gauge;
    [SerializeField] GameObject gauge_Fill;
    [SerializeField] GameObject invCheck;
    [SerializeField] GameObject diceCover;

    [SerializeField] GameObject[] Life_Ui;
    [SerializeField] GameObject[] Power_Ui;
    [SerializeField] GameObject[] Speed_Ui;
    [SerializeField] GameObject[] BluePower_Ui;

    public float DiceMove_Speed = 4f;
    public float DiceRotate_Speed = 400f;
    float PlayerMovePitch = 1.0f;

    DiceCover _diceCover;
    Slider gaugeSlider;
    Image gaugeImage;

    Color originColor = new Color(0, 1, 0);
    Color disableColor = new Color(0.7f, 0.7f, 0.7f);

    SphereCollider powerCol;
    SphereCollider speedCol;
    SphereCollider lifeCol;
    SphereCollider bluePowerCol;

    Material powerMat;
    Material speedMat;
    Material lifeMat;
    Material blueMat;

    [HideInInspector] public int PlayerLife;
    [HideInInspector] public int PlayerPower;
    [HideInInspector] public int PlayerSpeed;
    [HideInInspector] public int PlayerBluePower;

    bool isPowerSet = true;
    bool isPowerGet;
    bool isSpeedSet;
    bool isSpeedGet;
    bool isLifeSet;
    bool isLifeGet;
    bool isBluePowerSet;
    bool isBluePowerGet;

    Vector3 powerSetPos = new Vector3(6, 0, 0);
    Vector3 powerGetPos = new Vector3(6, -2.5f, 0);

    Vector3 speedSetPos = new Vector3(6, 0, -5);
    Vector3 speedGetPos = new Vector3(6, -2.5f, -5);

    Vector3 lifeSetPos = new Vector3(6, 0, 5);
    Vector3 lifeGetPos = new Vector3(6, -2.5f, 5);

    Vector3 bluePowerSetPos = new Vector3(6, 0, -3);
    Vector3 bluePowerGetPos = new Vector3(6, -2.5f, -3);

    float gaugeValue = 0.15f;
    bool upgradeTime;
    bool isFullUpgrade;
    private static UpgradeManager instance;
    public static UpgradeManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<UpgradeManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newObj = new GameObject().AddComponent<UpgradeManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    } // SINGLETON

    void Awake()
    {
        var objs = FindObjectsOfType<UpgradeManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        gaugeSlider = gauge.GetComponent<Slider>();
        gaugeImage = gauge_Fill.GetComponent<Image>();

        powerMat = powerTransform.GetChild(1).GetComponent<Renderer>().material;
        powerCol = powerTransform.gameObject.GetComponent<SphereCollider>();

        speedMat = speedTransform.GetChild(1).GetComponent<Renderer>().material;
        speedCol = speedTransform.gameObject.GetComponent<SphereCollider>();

        lifeMat = lifeTransform.GetChild(1).GetComponent<Renderer>().material;
        lifeCol = lifeTransform.gameObject.GetComponent<SphereCollider>();

        blueMat = bluePowerTransform.GetChild(1).GetComponent<Renderer>().material;
        bluePowerCol = bluePowerTransform.gameObject.GetComponent<SphereCollider>();

        _diceCover = diceCover.GetComponent<DiceCover>();
    }
    void Start()
    {
        Life_Ui[PlayerLife].SetActive(true);
        PlayerLife++;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GaugeUp();
        }

        SetGaugeColor();
        ButtonSetting();
    }
    void ButtonSetting()
    {
        if (isPowerSet) { ButtonSet(powerTransform, powerSetPos, powerMat, powerCol, ref isPowerSet); }
        if (isPowerGet) { ButtonGet(powerTransform, powerGetPos, powerMat, ref isPowerGet); }

        if (isSpeedSet) { ButtonSet(speedTransform, speedSetPos, speedMat, speedCol, ref isSpeedSet); }
        if (isSpeedGet) { ButtonGet(speedTransform, speedGetPos, speedMat, ref isSpeedGet); }

        if (isLifeSet) { ButtonSet(lifeTransform, lifeSetPos, lifeMat, lifeCol, ref isLifeSet); }
        if (isLifeGet) { ButtonGet(lifeTransform, lifeGetPos, lifeMat, ref isLifeGet); }

        if (isBluePowerSet) { ButtonSet(bluePowerTransform, bluePowerSetPos, blueMat, bluePowerCol, ref isBluePowerSet); }
        if (isBluePowerGet) { ButtonGet(bluePowerTransform, bluePowerGetPos, blueMat, ref isBluePowerGet); }
    }
    void ButtonSet(Transform btnTransform, Vector3 targetPos, Material mat, SphereCollider col, ref bool isBoolType)
    {
        btnTransform.position = Vector3.MoveTowards(btnTransform.position, targetPos, 0.05f);
        if (btnTransform.position == targetPos)
        {
            isBoolType = false;
            btnTransform.position = targetPos;
            col.enabled = true;
            mat.EnableKeyword("_EMISSION");
        }
    }
    void ButtonGet(Transform btnTransform, Vector3 targetPos, Material mat, ref bool isBoolType)
    {
        btnTransform.position = Vector3.MoveTowards(btnTransform.position, targetPos, 0.05f);
        if (btnTransform.position == targetPos)
        {
            isBoolType = false;
            btnTransform.position = targetPos;
            mat.DisableKeyword("_EMISSION");
        }
    }
    const int LIFE_FULL_UP = 3;
    const int POWER_FULL_UP = 2;
    const int SPEED_FULL_UP = 3;
    const int BLUE_POWER_FULL_UP = 2;
    void UpgradeSet()
    {
        FindObjectOfType<AudioManager>().Play("UpgradeSet");

        upgradeTime = true;
        gaugeValue -= 0.01f;

        if (PlayerLife < LIFE_FULL_UP) { isLifeSet = true; }
        if (PlayerPower < POWER_FULL_UP) { isPowerSet = true; }
        if (PlayerSpeed < SPEED_FULL_UP) { isSpeedSet = true; }
        if (PlayerBluePower < BLUE_POWER_FULL_UP && PlayerBluePower < PlayerPower) { isBluePowerSet = true; }
    }
    void UpgradeGet()
    {
        upgradeTime = false;

        isPowerGet = true;
        isSpeedGet = true;
        isLifeGet = true;
        isBluePowerGet = true;

        if (PlayerLife == LIFE_FULL_UP
            && PlayerPower == POWER_FULL_UP
            && PlayerSpeed == SPEED_FULL_UP
            && PlayerBluePower == BLUE_POWER_FULL_UP)
        { isFullUpgrade = true; }
    }
    public void GaugeUp()
    {
        if (PlayerLife < LIFE_FULL_UP
            || PlayerPower < POWER_FULL_UP
            || PlayerSpeed < SPEED_FULL_UP
            || PlayerBluePower < BLUE_POWER_FULL_UP)
        { isFullUpgrade = false; }

        if (!invCheck.activeSelf && !upgradeTime && !isFullUpgrade)
        {
            gaugeSlider.value += gaugeValue;
        }
        if (gaugeSlider.value >= 1)
        {
            gaugeSlider.value = 0;
            UpgradeSet();
        }
    }
    void SetGaugeColor()
    {
        if (invCheck.activeSelf || upgradeTime || isFullUpgrade)
        {
            gaugeImage.color = disableColor;
        }
        else
        {
            gaugeImage.color = originColor;
        }
    }
    public void SetCoverColor(ColorType _colorType)
    {
        _diceCover.colorType = _colorType;

        if (diceCover.activeInHierarchy)
        {
            diceCover.SetActive(false);
            diceCover.SetActive(true);
        }
        else
        {
            diceCover.SetActive(true);
        }
    }
    public void LifeUp()
    {
        Life_Ui[PlayerLife].SetActive(true);
        PlayerLife++;
        SetCoverColor(ColorType.life);
        UpgradeGet();
    }
    public void LifeDown()
    {
        PlayerLife--;
        Life_Ui[PlayerLife].SetActive(false);
    }
    public void PowerUp()
    {
        Power_Ui[PlayerPower].SetActive(true);
        PlayerPower++;
        SetCoverColor(ColorType.power);
        UpgradeGet();
    }
    public void BluePowerUp()
    {
        BluePower_Ui[PlayerBluePower].SetActive(true);
        PlayerBluePower++;
        SetCoverColor(ColorType.blue);
        UpgradeGet();
    }
    public void SpeedUp()
    {
        PlayerMovePitch += 0.1f;
        FindObjectOfType<AudioManager>().Pitch("PlayerMove", PlayerMovePitch);

        Speed_Ui[PlayerSpeed].SetActive(true);
        DiceMove_Speed += 1;
        DiceRotate_Speed += 100;
        PlayerSpeed++;
        SetCoverColor(ColorType.speed);
        UpgradeGet();
    }
}
