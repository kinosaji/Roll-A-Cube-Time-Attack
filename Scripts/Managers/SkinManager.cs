using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject CloseShopUI;
    [SerializeField] GameObject HelpIcon;

    [SerializeField] Material[] SideSkin;
    [SerializeField] Material[] BlackSkin;
    [SerializeField] Material[] WhiteSkin;

    [SerializeField] GameObject[] Slot;
    [SerializeField] GameObject[] SideTarget;
    [SerializeField] GameObject[] BlackTarget;
    [SerializeField] GameObject[] WhiteTarget;

    [SerializeField] GameObject[] SelectIcon;

    [SerializeField] Sprite[] ShopBtnSprite;
    [SerializeField] Image[] TabImage;

    [SerializeField] Text GoldNum;
    [SerializeField] GameObject[] LockObj;

    PlayerData _playerData;

    Renderer[] SideTargetRender;
    Renderer[] BlackTargetRender;
    Renderer[] WhiteTargetRender;

    string tempTabName;
    string tempSideSlotName;
    string tempBlackSlotName;
    string tempWhiteSlotName;

    int currentGold;
    int three_Click;

    Image shopBtnImage;

    Color opaque = new Color(1, 1, 1, 1);
    Color transparent = new Color(1, 1, 1, 0.3f);
    Vector3 selectIconPos = new Vector3(48, 180, 0);

    Dictionary<int, GameObject> lock_Dic = new Dictionary<int, GameObject>();
    List<int> keyList = new List<int>();
    void Awake()
    {
        if (!Debug.isDebugBuild)
        {
            GoogleManager.Instance.LoadCloud();
        }
        shopBtnImage = GetComponent<Image>();

        SideTargetRender = new Renderer[SideTarget.Length];
        BlackTargetRender = new Renderer[BlackTarget.Length];
        WhiteTargetRender = new Renderer[WhiteTarget.Length];

        for (int i = 0; i < SideTarget.Length; i++)
        {
            SideTargetRender[i] = SideTarget[i].GetComponent<Renderer>();
        }
        for (int i = 0; i < BlackTarget.Length; i++)
        {
            BlackTargetRender[i] = BlackTarget[i].GetComponent<Renderer>();
        }
        for (int i = 0; i < WhiteTarget.Length; i++)
        {
            WhiteTargetRender[i] = WhiteTarget[i].GetComponent<Renderer>();
        }
        for (int i = 0; i < LockObj.Length; i++)
        {
            lock_Dic.Add(i, LockObj[i]);
        }
    }
    void Start()
    {
        _playerData = GoogleManager.Instance.playerData;

        currentGold = int.Parse(_playerData.PlayerGold);
        GoldNum.text = currentGold.ToString();

        SideSlotClick(_playerData.SideSkin);
        BlackSlotClick(_playerData.BlackSkin);
        WhiteSlotClick(_playerData.WhiteSkin);

        UnLock();
    }

    #region // UI Show,Close
    public void ShowShopUi()
    {
        FindObjectOfType<AudioManager>().Play("UiTouch");

        shopBtnImage.sprite = ShopBtnSprite[1];
        HelpIcon.SetActive(false);
        CloseShopUI.SetActive(true);
        ShopUI.SetActive(true);

        TabClick("SideTab");
    }
    public void CloseShopUi()
    {
        FindObjectOfType<AudioManager>().Play("UiTouch");

        shopBtnImage.sprite = ShopBtnSprite[0];
        HelpIcon.SetActive(true);
        CloseShopUI.SetActive(false);
        ShopUI.SetActive(false);

        GoogleManager.Instance.playerData = _playerData;
        GoogleManager.Instance.SaveCloud();
    }
    #endregion
    public void TabClick(string tabName) // Tab Click
    {
        if (tempTabName == tabName) { return; }

        string slotName = "";
        switch (tabName)
        {
            case "SideTab":
                slotName = "SideSlot";
                break;
            case "BlackTab":
                slotName = "BlackSlot";
                break;
            case "WhiteTab":
                slotName = "WhiteSlot";
                break;
        }

        for (int i = 0; i < TabImage.Length; i++)
        {
            Color curColor = TabImage[i].name == tabName ? opaque : transparent;
            TabImage[i].color = curColor;
        }

        for (int i = 0; i < Slot.Length; i++)
        {
            bool isSame = Slot[i].name == slotName ? true : false;
            Slot[i].SetActive(isSame);
        }

        switch (slotName)
        {
            case "SideSlot":
                SelectIconPosition(_playerData.SideSkin);
                break;
            case "BlackSlot":
                SelectIconPosition(_playerData.BlackSkin);
                break;
            case "WhiteSlot":
                SelectIconPosition(_playerData.WhiteSkin);
                break;
        }
        tempTabName = tabName;
    }

    #region // Slot Click
    public void SideSlotClick(string slotName)
    {
        if (tempSideSlotName == slotName) { return; }

        switch (slotName)
        {
            case "Nomal":
                SkinApply(SideSkin[0], "Side");
                break;
            case "Modern1":
                SkinApply(SideSkin[1], "Side");
                break;
            case "Modern2":
                SkinApply(SideSkin[2], "Side");
                break;
        }
        tempSideSlotName = slotName;
        _playerData.SideSkin = slotName;
        three_Click++;
        if (three_Click > 3) { FindObjectOfType<AudioManager>().Play("Pop"); }
    }
    public void BlackSlotClick(string slotName)
    {
        if (tempBlackSlotName == slotName) { return; }

        switch (slotName)
        {
            case "Nomal":
                SkinApply(BlackSkin[0], "Black");
                break;
            case "Modern1":
                SkinApply(BlackSkin[1], "Black");
                break;
            case "Modern2":
                SkinApply(BlackSkin[2], "Black");
                break;
        }
        tempBlackSlotName = slotName;
        _playerData.BlackSkin = slotName;
        three_Click++;
        if (three_Click > 3) { FindObjectOfType<AudioManager>().Play("Pop"); }

    }
    public void WhiteSlotClick(string slotName)
    {
        if (tempWhiteSlotName == slotName) { return; }

        switch (slotName)
        {
            case "Nomal":
                SkinApply(WhiteSkin[0], "White");
                break;
            case "Modern1":
                SkinApply(WhiteSkin[1], "White");
                break;
            case "Modern2":
                SkinApply(WhiteSkin[2], "White");
                break;
        }
        tempWhiteSlotName = slotName;
        _playerData.WhiteSkin = slotName;
        three_Click++;
        if (three_Click > 3) { FindObjectOfType<AudioManager>().Play("Pop"); }
    }
    void SkinApply(Material mat, string skinType)
    {
        switch (skinType)
        {
            case "Side":
                for (int i = 0; i < SideTarget.Length; i++)
                {
                    SideTargetRender[i].material = mat;
                }
                break;
            case "Black":
                ObjectPoolManager.Instance.black_mat = mat;
                for (int i = 0; i < BlackTarget.Length; i++)
                {
                    BlackTargetRender[i].material = mat;
                }
                break;
            case "White":
                ObjectPoolManager.Instance.white_mat = mat;
                for (int i = 0; i < WhiteTarget.Length; i++)
                {
                    WhiteTargetRender[i].material = mat;
                }
                break;
        }
    }
    #endregion
    public void SelectIconPosition(string slotName)
    {
        GameObject slot = GameObject.Find(slotName);
        string parentName = slot.transform.parent.name;
        int iconNum = 0;

        switch (parentName)
        {
            case "SideSlot":
                iconNum = 0;
                break;
            case "BlackSlot":
                iconNum = 1;
                break;
            case "WhiteSlot":
                iconNum = 2;
                break;
        }
        SelectIcon[iconNum].transform.SetParent(slot.transform);
        SelectIcon[iconNum].transform.SetAsLastSibling();
        SelectIcon[iconNum].GetComponent<RectTransform>().anchoredPosition = selectIconPos;
    }
    void UnLock()
    {
        GameObject _lockNum;

        for (int i = 0; i < _playerData.Key.Length; i++)
        {
            keyList.Add(_playerData.Key[i]);
            lock_Dic.TryGetValue(_playerData.Key[i], out _lockNum);
            _lockNum.SetActive(false);
        }
    }
    public void BuySkin_Thirty(int keyNum)
    {
        const int PRICE = 30;
        int currentKeyNum = keyNum;
        GameObject _lockNum;
        string gold;

        if (currentGold >= PRICE) // 골드가 스킨을 구매하기에 충분하다면
        {
            FindObjectOfType<AudioManager>().Play("BuyDone");

            lock_Dic.TryGetValue(currentKeyNum, out _lockNum);
            _lockNum.SetActive(false);

            keyList.Add(currentKeyNum);
            _playerData.Key = new int[keyList.Count];

            for (int i = 0; i < keyList.Count; i++)
            {
                _playerData.Key[i] = keyList[i];
            }
            currentGold -= PRICE;
            gold = currentGold.ToString();
            GoldNum.text = gold;
            _playerData.PlayerGold = gold;

            GoogleManager.Instance.playerData = _playerData;
            GoogleManager.Instance.SaveCloud();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Lock");
            StartCoroutine(INot_EnoughGold());
        }
    }
    Color redColor = new Color(1, 0.5f, 0.5f);
    Color originColor = new Color(1, 1, 1);
    int clickNum;
    IEnumerator INot_EnoughGold()
    {
        clickNum++;
        GoldNum.color = redColor;
        yield return new WaitForSeconds(0.5f);
        clickNum--;
        if (clickNum == 0) { GoldNum.color = originColor; }
    }
}