using UnityEngine;
using TMPro;
public class PlayerFishScript : MonoBehaviour
{
	public float jumpForce = 5f;
	private Rigidbody2D rb;
	private bool isDead = false;

	public AudioClip dieSound;
	public AudioClip scoreSound;
	public float audioVolume = 0.5f;

	public float maxUpAngle = 30f;
	public float maxDownAngle = -90f;
	public float rotateSpeed = 5f;
	public int score = 0; // Biến lưu điểm
	public TextMeshProUGUI scoreText;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (isDead) return;

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Fly();
		}

		RotateBird();
	}

	void Fly()
	{
		rb.linearVelocity = Vector2.zero;
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

		transform.rotation = Quaternion.Euler(0, 0, maxUpAngle);
	}

	void RotateBird()
	{
		float velocityY = rb.linearVelocity.y;

		float angle = Mathf.Lerp(maxDownAngle, maxUpAngle, (velocityY + 5f) / 10f);
		angle = Mathf.Clamp(angle, maxDownAngle, maxUpAngle);

		Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

		transform.rotation = Quaternion.Lerp(
			transform.rotation,
			targetRotation,
			rotateSpeed * Time.deltaTime
		);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Kiểm tra xem vật thể va chạm có tag là "ScoreZone" không
		if (collision.gameObject.CompareTag("ScoreZone"))
		{
			score++;
			Debug.Log("Điểm hiện tại: " + score);
			UpdateScoreUI();
			AudioSource.PlayClipAtPoint(scoreSound, transform.position, audioVolume);

		}
		void UpdateScoreUI()
		{
			if (scoreText != null)
			{
				scoreText.text = score.ToString();
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (isDead) return;

		isDead = true;

		Debug.Log("Game Over");

		AudioSource.PlayClipAtPoint(dieSound, transform.position, audioVolume);

		// cho cá rơi xuống
		rb.linearVelocity = Vector2.zero;
		rb.gravityScale = 4;

		// tắt điều khiển
		enabled = false;

		Time.timeScale = 0f;
	}
}