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
	public GameObject SettingsMenu; // Assign this in the Unity Editor


	private bool tutorialStarted = false; // Track if tutorial has started
	private void Update()
	{
		// Check for screen tap
		if (Input.GetMouseButtonDown(0) && tutorialStarted)
		{
			startGame();
		}
	}
	public void settings()
	{
		// Implement settings functionality here
		Debug.Log("Settings button clicked!");
		// Active the settings menu or perform any desired actions
		if (SettingsMenu != null)
		{
			SettingsMenu.SetActive(true);
		}
		else
		{
			Debug.LogError("SettingsMenu GameObject is not assigned in the Inspector!");
		}

	}
	public void startTutorial()
	{
		// Activate the tutorial GameObject
		if (Tutorial != null)
		{
			Tutorial.SetActive(true);
		}
		else
		{
			Debug.LogError("Tutorial GameObject is not assigned in the Inspector!");
		}
		// Activeate the player GameObject
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

		tutorialStarted = true; // Mark tutorial as started
	}



	public void startGame()
	{
		//deactivate the tutorial GameObject
		if (Tutorial != null)
		{
			Tutorial.SetActive(false);
		}

		// Enable gravity for the player
		if (player != null)
		{
			Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				rb.gravityScale = 0.6f; // Adjust gravity scale as needed
			}
			else
			{
				Debug.LogError("Player GameObject does not have a Rigidbody2D component!");
			}
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

	}

}
