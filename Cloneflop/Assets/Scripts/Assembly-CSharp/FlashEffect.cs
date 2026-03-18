using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public static FlashEffect Instance;
    private Image flashImage;

    void Awake()
    {
        Instance = this;
        flashImage = GetComponent<Image>();
    }

    public void TriggerFlash(float duration = 0.1f, float maxAlpha = 0.8f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine(duration, maxAlpha));
    }

    IEnumerator FlashRoutine(float duration, float maxAlpha)
    {
        // Giai đoạn 1: Hiện lên nhanh
        Color c = flashImage.color;
        c.a = maxAlpha;
        flashImage.color = c;

        // Giai đoạn 2: Mờ dần đi
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Dùng unscaled để chạy được cả khi Time.timeScale = 0
            c.a = Mathf.Lerp(maxAlpha, 0f, elapsed / duration);
            flashImage.color = c;
            yield return null;
        }

        c.a = 0f;
        flashImage.color = c;
    }
}