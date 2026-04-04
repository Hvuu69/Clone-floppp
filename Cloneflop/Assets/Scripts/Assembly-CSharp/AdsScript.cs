using UnityEngine;
using UnityEngine.Advertisements;

public class AdsScript : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "6065423";
    [SerializeField] string _iOSGameId = "6065422";
    [SerializeField] bool _testMode = true;

    private string _gameId;
    private string _adUnitId = "Interstitial_Android";

    // Key để lưu trạng thái đã mua No Ads
    private const string NoAdsKey = "UserBoughtNoAds";

    void Awake()
    {
        InitializeAds();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeAds()
    {
        // Nếu đã mua No Ads thì không cần khởi tạo quảng cáo để tiết kiệm tài nguyên
        if (IsNoAdsPurchased()) return;

        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void ShowAd()
    {
        // KIỂM TRA QUAN TRỌNG: Nếu đã mua No Ads thì thoát luôn, không hiện quảng cáo
        if (IsNoAdsPurchased())
        {
            Debug.Log("Người dùng đã mua No Ads. Bỏ qua quảng cáo.");
            return;
        }

        if (Advertisement.isInitialized)
        {
            Advertisement.Show(_adUnitId, this);
        }
    }

    // Hàm kiểm tra trạng thái mua hàng
    public bool IsNoAdsPurchased()
    {
        return PlayerPrefs.GetInt(NoAdsKey, 0) == 1;
    }

    // Hàm này sẽ được gọi từ IAP Script sau khi mua hàng thành công
    public void ActivateNoAds()
    {
        PlayerPrefs.SetInt(NoAdsKey, 1);
        PlayerPrefs.Save();
        Debug.Log("Đã kích hoạt chế độ Không quảng cáo!");
    }

    // ... Các hàm callback giữ nguyên như cũ ...
    public void LoadAd()
    {
        if (IsNoAdsPurchased()) return;
        Advertisement.Load(_adUnitId, this);
    }
    public void OnInitializationComplete() { LoadAd(); }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
    public void OnUnityAdsAdLoaded(string adUnitId) { }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { LoadAd(); }
}