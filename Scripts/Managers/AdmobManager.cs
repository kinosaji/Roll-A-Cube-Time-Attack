using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Purchasing;

public class AdmobManager : MonoBehaviour
{
    [SerializeField] GameObject adsArrow;
    [SerializeField] GameObject defaultArrow;

    // private readonly string UNIT_ID = "ca-app-pub-3940256099942544/1033173712"; // 전면광고 테스트
    // private readonly string BUNIT_ID = "ca-app-pub-3940256099942544/6300978111"; // 배너광고 테스트

    private readonly string UNIT_ID = "ca-app-pub-3940256099942544/1033173712";
    private readonly string TEST_ID = "ca-app-pub-3940256099942544/1033173712";

    private readonly string BUNIT_ID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string BTEST_ID = "ca-app-pub-3940256099942544/6300978111";

    private readonly string REMOVE_ADS = "com.kinosaji.dicepuzzle.removeads";

    InterstitialAd interstitialAd;
    BannerView bannerAd;

    void Start()
    {
        SetArrowJoystick();

        if (GoogleManager.Instance.playerData.HasAds)
        {
            MobileAds.Initialize(InitializationStatus => { });
            RequestBanner();
            RequestInterstitial();
        }
        else
        {
            return;
        }
    }
    AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    void RequestInterstitial() // 전면광고준비
    {
        string unitID = Debug.isDebugBuild ? TEST_ID : UNIT_ID;

        if (this.interstitialAd != null)
        {
            this.interstitialAd.Destroy();
        }
        this.interstitialAd = new InterstitialAd(unitID);
        this.interstitialAd.LoadAd(this.CreateAdRequest());
    }
    public void ShowInterstitial() // 전면광고 출력
    {
        if (this.interstitialAd.IsLoaded()) { interstitialAd.Show(); }
    }
    void RequestBanner()
    {
        string unitID = Debug.isDebugBuild ? BTEST_ID : BUNIT_ID;

        if (this.bannerAd != null)
        {
            this.bannerAd.Destroy();
        }
        this.bannerAd = new BannerView(unitID, AdSize.Banner, AdPosition.Bottom);
        this.bannerAd.LoadAd(this.CreateAdRequest());
    }
    void SetArrowJoystick()
    {
        if (GoogleManager.Instance.playerData.HasAds)
        {
            adsArrow.SetActive(true);
        }
        else
        {
            defaultArrow.SetActive(true);
        }
    }
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == REMOVE_ADS)
        {
            bannerAd.Hide();
            GoogleManager.Instance.playerData.HasAds = false;
            GoogleManager.Instance.SaveCloud();
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        if (failureReason == PurchaseFailureReason.DuplicateTransaction)
        {
            bannerAd.Hide();
            GoogleManager.Instance.playerData.HasAds = false;
            GoogleManager.Instance.SaveCloud();
        }
        else
        {
            return;
        }
    }
}
