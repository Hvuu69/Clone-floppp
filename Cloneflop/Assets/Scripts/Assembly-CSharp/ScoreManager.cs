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
	public List<int> skinUnlockMilestones; // Danh sách mốc điểm để mở skin (vd: 100, 500, 1000)

	private int currentScore = 0;
	private int highScore;
	private int totalScore;


	void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	void Start()
	{
		// Tải dữ liệu cũ
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
		// 1. Lấy lại tổng điểm cũ từ bộ nhớ trước khi cộng (để đảm bảo không bị mất dữ liệu)
		totalScore = PlayerPrefs.GetInt("TotalScore", 0);

		// 2. Cộng dồn điểm của màn chơi vừa rồi vào tổng điểm
		totalScore += currentScore;

		// 3. Lưu tổng điểm mới vào PlayerPrefs
		PlayerPrefs.SetInt("TotalScore", totalScore);

		// 4. Kiểm tra và lưu High Score (điểm cao nhất 1 lần chơi)
		int oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
		if (currentScore > oldHighScore)
		{
			PlayerPrefs.SetInt("HighScore", currentScore);
		}

		// 5. Kiểm tra mở khóa Skin (nếu có)
		CheckSkinUnlocks();

		// Quan trọng: Phải gọi Save() để đảm bảo dữ liệu ghi xuống ổ cứng
		PlayerPrefs.Save();

		// Cập nhật lại giao diện
		UpdateUI();

		Debug.Log("Game Over! Điểm màn này: " + currentScore + " | Tổng tích lũy: " + totalScore);
	}
	void CheckSkinUnlocks()
	{
		for (int i = 0; i < skinUnlockMilestones.Count; i++)
		{
			if (totalScore >= skinUnlockMilestones[i])
			{
				// Lưu trạng thái đã mở khóa skin thứ i
				PlayerPrefs.SetInt("SkinUnlocked_" + i, 1);
				Debug.Log("Đã mở khóa Skin mới tại mốc: " + skinUnlockMilestones[i]);
			}
		}
	}

	public void UpdateUI()
	{
		string s = currentScore.ToString();
		// Dùng vòng lặp để cập nhật tất cả các Text trong mảng
		foreach (var textObj in scoreTexts)
		{
			if (textObj != null) textObj.text = s;
		}
		if (highScoreText) highScoreText.text = highScore.ToString();
		if (totalScoreText) totalScoreText.text = "Total: " + totalScore;
	}

	// Hàm kiểm tra xem một skin cụ thể đã mở chưa (dùng cho menu chọn skin)
	public bool IsSkinUnlocked(int index) => PlayerPrefs.GetInt("SkinUnlocked_" + index, 0) == 1;
}