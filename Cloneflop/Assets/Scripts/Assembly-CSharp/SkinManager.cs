using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    // --- PHẦN DỮ LIỆU TĨNH (Lưu trữ xuyên suốt các Scene) ---
    public static int SelectedSkinIndex = 0;

    // --- PHẦN CẤU HÌNH TRÊN EDITOR ---
    [Header("Cài đặt cho GameScene")]
    [SerializeField] private RuntimeAnimatorController[] skinAnimators;
    private Animator characterAnimator;

    [Header("Tên các Scene")]
    [SerializeField] private string menuSceneName = "MenuScene";
    [SerializeField] private string gameSceneName = "MainScene";

    void Start()
    {
        // Kiểm tra nếu đang ở GameScene thì mới thực hiện đổi skin cho nhân vật
        if (SceneManager.GetActiveScene().name == gameSceneName)
        {
            characterAnimator = GetComponent<Animator>();
            ApplySkin();
        }
    }

    // --- HÀM DÙNG TẠI MENUSCENE (Gán vào Button OnClick) ---
    public void SelectSkin(int index)
    {
        SelectedSkinIndex = index;
        Debug.Log("Đã lưu Skin ID: " + index);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    // --- HÀM THỰC THI ĐỔI SKIN ---
    private void ApplySkin()
    {
        if (characterAnimator == null) return;

        if (SelectedSkinIndex >= 0 && SelectedSkinIndex < skinAnimators.Length)
        {
            characterAnimator.runtimeAnimatorController = skinAnimators[SelectedSkinIndex];
        }
        else
        {
            Debug.LogError("Chỉ số Skin không hợp lệ hoặc chưa kéo Animator vào mảng!");
        }
    }
}