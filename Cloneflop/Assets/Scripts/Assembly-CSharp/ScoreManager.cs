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
	[Tooltip("Danh sách mốc điểm tích lũy để mở skin (vd: 100, 500, 1000)")]
	public List<int> skinUnlockMilestones;

	private int currentScore = 0;
	private int highScore;
	private int totalScore;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			// Nếu bạn muốn ScoreManager tồn tại xuyên suốt các Scene
			// DontDestroyOnLoad(gameObject); 
		}
		else Destroy(gameObject);
	}

	void Start()
	{
		// Tải dữ liệu khi bắt đầu game
		highScore = PlayerPrefs.GetInt("HighScore", 0);
		totalScore = PlayerPrefs.GetInt("TotalScore", 0);
		UpdateUI();
	}

	public void AddScore(int amount)
	{
		currentScore += amount;
		UpdateUI();
	}

	public void SaveAndCheckData()
	{
		// 1. Cập nhật Tổng điểm tích lũy
		totalScore += currentScore;
		PlayerPrefs.SetInt("TotalScore", totalScore);

		// 2. Cập nhật Điểm cao nhất (High Score)
		if (currentScore > highScore)
		{
			highScore = currentScore;
			PlayerPrefs.SetInt("HighScore", highScore);
		}

		// 3. Kiểm tra mở khóa skin
		CheckSkinUnlocks();

		// 4. Lưu lại và cập nhật UI
		PlayerPrefs.Save();
		UpdateUI();

		Debug.Log($"Game Over! Điểm màn này: {currentScore} | Tổng tích lũy: {totalScore}");

		// Reset điểm hiện tại cho lần chơi sau (nếu cần)
		// currentScore = 0; 
	}

	void CheckSkinUnlocks()
	{
		for (int i = 0; i < skinUnlockMilestones.Count; i++)
		{
			// Kiểm tra: Nếu đủ điểm VÀ skin này CHƯA từng được mở khóa trước đó
			if (totalScore >= skinUnlockMilestones[i] && !IsSkinUnlocked(i))
			{
				PlayerPrefs.SetInt("SkinUnlocked_" + i, 1);
				Debug.Log("<color=green>CHÚC MỪNG!</color> Đã mở khóa Skin mới tại mốc: " + skinUnlockMilestones[i]);

			}
		}
	}

	public void UpdateUI()
	{
		string s = currentScore.ToString();
		foreach (var textObj in scoreTexts)
		{
			if (textObj != null) textObj.text = s;
		}

		if (highScoreText) highScoreText.text = highScore.ToString();
		if (totalScoreText) totalScoreText.text = "Total: " + totalScore.ToString();
	}

	// Hàm kiểm tra trạng thái mở khóa
	public bool IsSkinUnlocked(int index)
	{
		// Skin mặc định (index 0) nên luôn được mở
		if (index == 0) return true;
		return PlayerPrefs.GetInt("SkinUnlocked_" + index, 0) == 1;
	}

	// Hàm reset dữ liệu (Dùng để test khi cần)
	[ContextMenu("Reset All Data")]
	public void ResetData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Đã xóa hết dữ liệu game!");
	}

	[ContextMenu("Add 100 Total Score (Debug)")]
	public void DebugAddTotalScore()
	{
		totalScore += 100;
		PlayerPrefs.SetInt("TotalScore", totalScore);

		CheckSkinUnlocks();
		PlayerPrefs.Save();
		UpdateUI();

		Debug.Log("DEBUG: Added 100 Total Score. New Total = " + totalScore);
	}
	public void DebugAddScoreButton()
	{
		totalScore += 100;
		CheckSkinUnlocks();
		UpdateUI();
	}
}