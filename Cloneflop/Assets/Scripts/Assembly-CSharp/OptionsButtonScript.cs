using UnityEngine;

public class OptionsButtonScript : MonoBehaviour
{
	public void restartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
		);
	}
}
