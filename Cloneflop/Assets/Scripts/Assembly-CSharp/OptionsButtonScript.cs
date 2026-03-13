using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonScript : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject Tutorial;
    public GameObject SettingsMenu;
    public GameObject GameOverMenu;
	public GameObject pipeControler;

	private Rigidbody2D rb;
	private bool gameStarted = false;

	public GameObject player;

    private bool tutorialStarted = false;
    private bool gameUIActivated = false;

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

   void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

        // Ban đầu không có trọng lực
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        // Bật trọng lực cho player
        rb.gravityScale = 0.6f;

        // Tắt tutorial
        if (Tutorial != null)
        {
            Tutorial.SetActive(false);
        }
		if(pipeControler != null)
		{
			pipeControler.SetActive(true);
		}
		// Hiển thị Game UI
		if (GameUI != null)
		{
			GameUI.SetActive(true);
		}
    }
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
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
            Debug.LogError("GameOverMenu is not assigned!");
        }

        if (GameUI != null)
        {
            GameUI.SetActive(false);
        }
		// Tắt điều khiển
		enabled = false;
		// Tắt pipe controler
		if(pipeControler != null)		
		{
			pipeControler.SetActive(false);		
    }
	//tắt GameUI
		if(GameUI != null)
		{
		GameUI.SetActive(false);
		}
	}
}