using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class SkinManager : MonoBehaviour
{
    public static int SelectedSkinIndex = 0;

    [Header("Cài đặt cho GameScene")]
    [SerializeField] private RuntimeAnimatorController[] skinAnimators;
    private Animator characterAnimator;

    [Header("Giao diện Menu")]
    [SerializeField] private TextMeshProUGUI[] skinButtonTexts;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color lockedColor = Color.gray;

    [Header("Tên các Scene")]
    [SerializeField] private string menuSceneName = "MenuScene";
    [SerializeField] private string gameSceneName = "MainScene";

    void Start()
    {
        // Load index đã lưu từ máy
        SelectedSkinIndex = PlayerPrefs.GetInt("SavedSelectedSkin", 0);

        if (SceneManager.GetActiveScene().name == gameSceneName)
        {
            characterAnimator = GetComponent<Animator>();
            ApplySkin();
        }
        else
        {
            UpdateVisuals();
        }
    }

    public void SelectSkin(int index)
    {
        // Kiểm tra xem skin có được mở khóa không thông qua ScoreManager
        if (ScoreManager.Instance != null && !ScoreManager.Instance.IsSkinUnlocked(index))
        {
            Debug.Log("<color=red>Skin này còn đang khóa!</color>");
            return;
        }

        // Cập nhật giá trị mới
        SelectedSkinIndex = index;

        // Lưu vào máy ngay lập tức
        PlayerPrefs.SetInt("SavedSelectedSkin", SelectedSkinIndex);
        PlayerPrefs.Save();

        UpdateVisuals();
        Debug.Log("<color=green>Đã chọn Skin: </color>" + index);
    }

    private void UpdateVisuals()
    {
        if (skinButtonTexts == null || skinButtonTexts.Length == 0) return;

        for (int i = 0; i < skinButtonTexts.Length; i++)
        {
            if (skinButtonTexts[i] == null) continue;

            bool isUnlocked = true;
            if (ScoreManager.Instance != null)
                isUnlocked = ScoreManager.Instance.IsSkinUnlocked(i);

            // Chỉ thay đổi màu sắc (Color), không chạm vào nội dung chữ (Text)
            if (!isUnlocked)
            {
                skinButtonTexts[i].color = lockedColor;
            }
            else if (i == SelectedSkinIndex)
            {
                skinButtonTexts[i].color = selectedColor;
            }
            else
            {
                skinButtonTexts[i].color = normalColor;
            }
        }
    }

    private void ApplySkin()
    {
        if (characterAnimator == null) return;

        if (SelectedSkinIndex >= 0 && SelectedSkinIndex < skinAnimators.Length)
        {
            if (skinAnimators[SelectedSkinIndex] != null)
            {
                characterAnimator.runtimeAnimatorController = skinAnimators[SelectedSkinIndex];
            }
        }
    }

    // Các hàm chuyển cảnh
    public void LoadGame() => SceneManager.LoadScene(gameSceneName);
    public void BackToMenu() => SceneManager.LoadScene(menuSceneName);
}