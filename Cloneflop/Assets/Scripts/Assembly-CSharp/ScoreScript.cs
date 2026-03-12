using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	public AudioClip scoreSound;
	private AudioSource audioSource;
	private bool scored = false;
	private int score = 0; // Track the score

	void Start()
	{
		// Lấy AudioSource từ Pipe (object cha)
		audioSource = GetComponentInParent<AudioSource>();
	}

	public int GetScore()
	{
		return score;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !scored)
		{
			scored = true;
			score++; // Increment the score

			Debug.Log("Score!");

			audioSource.PlayOneShot(scoreSound);
		}
	}
}