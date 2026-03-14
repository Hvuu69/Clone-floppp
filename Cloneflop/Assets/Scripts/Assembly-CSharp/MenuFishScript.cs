using UnityEngine;
using TMPro;

public class MenuFishScript : MonoBehaviour
{
	public float baseSpeed = 1.5f;      // tốc độ chính
	public float amplitude = 0.5f;      // biên độ lượn
	public float frequency = 1f;        // tần số lượn
	public float changeDirectionTime = 3f; // thời gian đổi hướng
	public TextMeshProUGUI menuHighScoreText;

	private Vector3 direction;
	private float timer = 0f;
	private float offset;

	void Start()
	{
		// random phase để mỗi con cá có pattern khác nhau
		offset = Random.Range(0f, 2f * Mathf.PI);
		SetNewDirection();
		int record = PlayerPrefs.GetInt("HighScore", 0);

		if (menuHighScoreText != null)
		{
			menuHighScoreText.text = record.ToString();
		}
	}

	void Update()
	{
		// timer để đổi hướng định kỳ
		timer += Time.deltaTime;

		if (timer >= changeDirectionTime)
		{
			SetNewDirection();
			timer = 0f;
		}

		MoveFish();
	}

	void SetNewDirection()
	{
		// random góc 360 độ
		float angle = Random.Range(0f, 2f * Mathf.PI);
		direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
	}

	void MoveFish()
	{
		float sine = Mathf.Sin(Time.time * frequency + offset) * amplitude;

		// di chuyển dựa vào direction và thêm lượn theo sine định hướng vuông góc
		Vector3 perp = new Vector3(-direction.y, direction.x, 0);

		transform.position += (direction * baseSpeed + perp * sine) * Time.deltaTime;

		// lật sprite theo hướng chính
		if (direction.x > 0)
			transform.localScale = new Vector3(1, 1, 1);
		else
			transform.localScale = new Vector3(-1, 1, 1);
	}
}