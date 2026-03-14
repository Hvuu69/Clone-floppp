using UnityEngine;
using UnityEngine.Advertisements;

public class AdsScript : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId = "fb5e429d-c189-4cbf-93aa-069157d10717"; // GameID
    [SerializeField] string _iOSGameId = "7654321";
    [SerializeField] bool _testMode = true; // Luôn để true khi đang demo/test

    private string _gameId;
    private string _adUnitId = "Interstitial_Android"; // Tên Ad Unit mặc định của Unity

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    // --- CÁC HÀM ĐIỀU KHIỂN ---

    public void LoadAd()
    {
        Debug.Log("Đang tải quảng cáo...");
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        // Kiểm tra xem máy có mạng và SDK đã sẵn sàng chưa
        if (Advertisement.isInitialized)
        {
            Debug.Log("SDK đã sẵn sàng, đang gọi Show...");
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Debug.LogError("SDK chưa khởi tạo xong! Hãy đợi vài giây rồi bấm lại.");
        }
    }

    // --- LOGIC KHI KHỞI TẠO XONG ---
    public void OnInitializationComplete() { Debug.Log("Unity Ads đã sẵn sàng."); LoadAd(); }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { Debug.Log($"Lỗi khởi tạo: {message}"); }

    // --- LOGIC KHI TẢI XONG ---
    public void OnUnityAdsAdLoaded(string adUnitId) { Debug.Log("Đã tải xong Ad: " + adUnitId); }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { Debug.Log($"Lỗi tải Ad: {message}"); }

    // --- LOGIC KHI ĐANG XEM ---
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Người dùng đã xem hết quảng cáo! Trao thưởng tại đây.");
        }
        // Sau khi xem xong, tải cái mới để sẵn sàng cho lần sau
        LoadAd();
    }
}

