using UnityEngine;
using TMPro;

public class SkinButtonUI : MonoBehaviour
{
    public int skinIndex;
    public TextMeshProUGUI skinText;

    private Color lockedColor = Color.red;     // chưa unlock
    private Color unlockedColor = Color.white; // đã unlock
    private Color selectedColor = Color.yellow;// đang chọn

    void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        if (!ScoreManager.Instance.IsSkinUnlocked(skinIndex))
        {
            skinText.color = lockedColor;
        }
        else if (SkinManager.SelectedSkinIndex == skinIndex)
        {
            skinText.color = selectedColor;
        }
        else
        {
            skinText.color = unlockedColor;
        }
    }

    public void OnClick()
    {
        if (!ScoreManager.Instance.IsSkinUnlocked(skinIndex))
            return;

        SkinManager.SelectedSkinIndex = skinIndex;

        SkinButtonUI[] buttons = FindObjectsOfType<SkinButtonUI>();
        foreach (var b in buttons)
            b.UpdateColor();
    }
}