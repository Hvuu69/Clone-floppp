using UnityEngine;

public class OptionsButtonScript : MonoBehaviour
{
	public void restartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
		);
		Time.timeScale = 1f;
	}
	public GameObject player; // Assign this in the Unity Editor
	public GameObject Menu; // Assign this in the Unity Editor
	public GameObject GameUI; // Assign this in the Unity Editor
	public GameObject Tutorial; // Assign this in the Unity Editor
	public GameObject PipeController; // Assign this in the Unity Editor


	public void startGame()
	{
		// Activate the player GameObject
		if (player != null)
		{
			player.SetActive(true);
		}
		else
		{
			Debug.LogError("Player GameObject is not assigned in the Inspector!");
		}
		// Deactivate the menu GameObject
		if (Menu != null)
		{
			Menu.SetActive(false);
		}
		else
		{
			Debug.LogError("Menu GameObject is not assigned in the Inspector!");
		}
		//activeate Game ui
		if (GameUI != null)
		{
			GameUI.SetActive(true);
		}
		else
		{
			Debug.LogError("GameUI GameObject is not assigned in the Inspector!");
		}
		//activeate PipeController
		if (PipeController != null)
		{
			PipeController.SetActive(true);
		}
		else
		{
			Debug.LogError("PipeController GameObject is not assigned in the Inspector!");
		}
		// //activeate Tutorial
		// if (Tutorial != null)
		// {
		// 	Tutorial.SetActive(true);
		// }
		// else
		// {
		// 	Debug.LogError("Tutorial GameObject is not assigned in the Inspector!");
		// }


	}

	private void StartTutorial()
	{
		Debug.Log("Tutorial started.");
		// Add tutorial logic here
	}
}
