using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public AdsScript adsScript; // Kéo file AdsScript vào đây trong Inspector

    public void OnPurchaseComplete(Product product)
    {
        // Kiểm tra đúng ID sản phẩm tắt quảng cáo của bạn
        if (product.definition.id == "com.yourgame.removeads")
        {
            adsScript.ActivateNoAds();
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Mua hàng thất bại: {failureReason}");
    }
}