using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonScript : MonoBehaviour
{
	public GameObject player;
	public GameObject Menu;
	public GameObject GameUI;
	public GameObject Tutorial;
	public GameObject PipeController;
	public GameObject SettingsMenu;
	public GameObject menuFish;
	public GameObject GameOverMenu;

	private bool tutorialStarted = false;
	private bool gameUIActivated = false;

	private void Update()
	{
		// Check for screen tap
		if (Input.GetMouseButtonDown(0) && tutorialStarted)
		{
			startGame();
		}
	}
	public void restartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
	}

	void ActivateGameUI()
	{
		if (!gameUIActivated && GameUI != null)
		{
			GameUI.SetActive(true);
			gameUIActivated = true;
		}
	}

	public void settings()
	{
		Debug.Log("Settings button clicked!");

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
		if (Tutorial != null)
		{
			Tutorial.SetActive(true);
		}
		else
		{
			Debug.LogError("Tutorial GameObject is not assigned!");
		}

		if (player != null)
		{
			player.SetActive(true);
		}
		else
		{
			Debug.LogError("Player GameObject is not assigned!");
		}

		if (Menu != null)
		{
			Menu.SetActive(false);
		}

		ActivateGameUI();

		if (menuFish != null)
		{
			menuFish.SetActive(false);
		}
		if (GameOverMenu != null)
		{
			GameOverMenu.SetActive(false);
		}

		tutorialStarted = true;

	}


	public void startGame()
	{
		if (!tutorialStarted)
		{
			Debug.Log("You must start the tutorial first!");
			return;
		}

		if (Tutorial != null)
		{
			Tutorial.SetActive(false);
		}

		if (player != null)
		{
			Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

			if (rb != null)
			{
				rb.gravityScale = 0.6f;
			}
			else
			{
				Debug.LogError("Player does not have Rigidbody2D!");
			}
		}

		ActivateGameUI();

		if (PipeController != null)
		{
			PipeController.SetActive(true);
		}
	}

	public void GameOver()
	{
		Time.timeScale = 0f;
		Debug.Log("Game Over!");

		if (GameOverMenu != null)
		{
			GameOverMenu.SetActive(true);
		}
		else
		{
			Debug.LogError("GameOverMenu GameObject is not assigned!");
		}

		if (GameUI != null)
		{
			GameUI.SetActive(false);
		}
	}
}