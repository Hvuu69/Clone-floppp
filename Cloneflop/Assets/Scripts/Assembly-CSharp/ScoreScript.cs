using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	public AudioClip scoreSound;
	private AudioSource audioSource;
	private bool scored = false;

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
		}
	}
}