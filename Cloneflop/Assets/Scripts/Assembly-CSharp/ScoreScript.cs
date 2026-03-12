using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	public AudioClip scoreSound;
	private AudioSource audioSource;
	private bool scored = false;

	private int score = 0; // Field to track the score

	void Start()
	{
		// Lấy AudioSource từ Pipe (object cha)
		audioSource = GetComponentInParent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !scored)
		{
			scored = true;

			Debug.Log("Score!");

			audioSource.PlayOneShot(scoreSound);

			score++; // Increment the score
		}
	}

	public int GetScore()
	{
		return score; // Return the current score
	}
}