using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	[Header("UI References")]
	public TextMeshProUGUI[] scoreTexts;
	public TextMeshProUGUI highScoreText;
	public TextMeshProUGUI totalScoreText;

	[Header("Skin System")]
	[Tooltip("Danh sách mốc điểm để mở khóa skin. Index 0 ứng với Skin 0, Index 1 ứng với Skin 1...")]
	public List<int> skinUnlockMilestones;

	[Header("Debug Tools (Inspector)")]
	public int debugSetTotalScoreValue;

	private int currentScore = 0;
	private int highScore;
	private int totalScore;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			// Đảm bảo ScoreManager không bị mất khi chuyển cảnh nếu cần
			// DontDestroyOnLoad(gameObject); 
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		// Load dữ liệu khi vào game
		highScore = PlayerPrefs.GetInt("HighScore", 0);
		totalScore = PlayerPrefs.GetInt("TotalScore", 0);

		// Kiểm tra mở khóa skin ngay khi vào game dựa trên totalScore cũ
		CheckSkinUnlocks();
		UpdateUI();
	}

	// --- LOGIC ĐIỂM SỐ ---

	public void AddScore(int amount)
	{
		currentScore += amount;
		UpdateUI();
	}

	public void SaveAndCheckData()
	{
		// Cộng dồn điểm hiện tại vào tổng điểm
		totalScore += currentScore;
		PlayerPrefs.SetInt("TotalScore", totalScore);

		// Kiểm tra kỷ lục mới
		if (currentScore > highScore)
		{
			highScore = currentScore;
			PlayerPrefs.SetInt("HighScore", highScore);
		}

		// Reset điểm hiện tại sau khi đã cộng dồn vào tổng
		currentScore = 0;

		CheckSkinUnlocks();
		PlayerPrefs.Save();
		UpdateUI();
	}

	public void CheckSkinUnlocks()
	{
		if (skinUnlockMilestones == null) return;

		for (int i = 0; i < skinUnlockMilestones.Count; i++)
		{
			// Nếu tổng điểm >= mốc quy định
			if (totalScore >= skinUnlockMilestones[i])
			{
				// Kiểm tra xem trong máy đã lưu là "đã mở" chưa (0 là chưa, 1 là rồi)
				if (PlayerPrefs.GetInt("SkinUnlocked_" + i, 0) == 0)
				{
					PlayerPrefs.SetInt("SkinUnlocked_" + i, 1);
					Debug.Log($"<color=green>🎉 ĐÃ MỞ KHÓA SKIN INDEX {i} TẠI MỐC {skinUnlockMilestones[i]} ĐIỂM!</color>");
				}
			}
		}
		PlayerPrefs.Save();
	}

	public bool IsSkinUnlocked(int index)
	{
		// Skin đầu tiên (index 0) thường mặc định là mở
		if (index == 0) return true;

		// Các skin còn lại kiểm tra giá trị đã lưu
		return PlayerPrefs.GetInt("SkinUnlocked_" + index, 0) == 1;
	}

	public void UpdateUI()
	{
		string s = currentScore.ToString();
		foreach (var textObj in scoreTexts)
		{
			if (textObj != null) textObj.text = s;
		}

		if (highScoreText != null) highScoreText.text = highScore.ToString();
		if (totalScoreText != null) totalScoreText.text = "Total: " + totalScore.ToString();
	}

	// --- CÔNG CỤ DEBUG (Chuột phải vào Component trên Inspector để dùng) ---

	[ContextMenu("Set Total Score From Value")]
	public void SetTotalScoreFromInspector()
	{
		totalScore = debugSetTotalScoreValue;
		PlayerPrefs.SetInt("TotalScore", totalScore);
		CheckSkinUnlocks();
		UpdateUI();
		Debug.Log("<color=cyan>Đã gán Total Score thành: </color>" + totalScore);
	}

	[ContextMenu("Add 500 Total Score")]
	public void QuickAdd500()
	{
		totalScore += 500;
		PlayerPrefs.SetInt("TotalScore", totalScore);
		CheckSkinUnlocks();
		UpdateUI();
	}

	[ContextMenu("Reset All Data")]
	public void ResetAllData()
	{
		PlayerPrefs.DeleteAll(); // Xóa sạch bộ nhớ
		currentScore = 0;
		highScore = 0;
		totalScore = 0;
		UpdateUI();
		Debug.Log("<color=red>Dữ liệu đã được xóa sạch!</color>");
	}
}