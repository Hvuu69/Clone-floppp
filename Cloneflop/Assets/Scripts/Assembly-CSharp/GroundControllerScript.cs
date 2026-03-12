using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManagement to reload the scene

public class GroundControllerScript : MonoBehaviour
{
	public float moveSpeed = 3f;   // tốc độ di chuyển
	public float resetX = 20f;     // khoảng cách reset sang phải
	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
	}

	void Update()
	{
		// di chuyển sang trái
		transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

		// nếu đi quá vị trí cho phép thì quay lại bên phải
		if (transform.position.x < startPos.x - resetX)
		{
			transform.position = startPos;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player collided with the ground. Stopping the game.");
			//Gọi hàm GameOver từ OptionsButtonScript
			OptionsButtonScript optionsButtonScript = FindObjectOfType<OptionsButtonScript>();
			if (optionsButtonScript != null)
			{
				optionsButtonScript.GameOver();
			}
			else
			{
				Debug.LogError("OptionsButtonScript not found in the scene!");
			}
		}
	}
}