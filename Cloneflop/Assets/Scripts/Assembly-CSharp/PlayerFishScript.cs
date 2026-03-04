using UnityEngine;

public class PlayerFishScript : MonoBehaviour
{
	public float jumpForce = 5f;
	private Rigidbody2D rb;
	private bool isDead = false;
	public AudioClip dieSound; // Âm thanh chết
	public float audioVolume = 0.5f; // Âm lượng âm thanh


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Nhấn Space hoặc bấm vào màn hình
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isDead)
		{
			Fly();
		}
	}

	void Fly()
	{
		rb.linearVelocity = Vector2.zero;// Reset vận tốc cũ
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		isDead = true;
		Debug.Log("Game Over");
		AudioSource.PlayClipAtPoint(dieSound, transform.position, audioVolume); // Phát âm thanh chết
		Time.timeScale = 0;   // Dừng game
	}
	public void imortality()//Xàm :))
	{
		Input.GetKeyDown(KeyCode.I);
		Debug.Log("Immortality Activated");
		isDead = false;
		Time.timeScale = 1;   // Tiếp tục game
	}
	public void restartGame()
	{
		Input.GetKeyDown(KeyCode.R);
		Debug.Log("Game Restarted");
		isDead = false;
		Time.timeScale = 1;   // Tiếp tục game
	}
}