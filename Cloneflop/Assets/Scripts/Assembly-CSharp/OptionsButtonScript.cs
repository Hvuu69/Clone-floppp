using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonScript : MonoBehaviour
{
    [Header("Menus & UI")]
    public GameObject GameUI;
    public GameObject Tutorial, GameOverMenu, pauseMenu, optionMenu;

    [Header("Game Control")]
    public GameObject pipeControler;
    public GameObject player;

    private Rigidbody2D rb;
    private bool gameStarted = false;
    private bool isPaused = false;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Vô hiệu hóa trọng lực ban đầu
    }

    void Update()
    {
        // Bắt đầu game khi click chuột lần đầu
        if (Input.GetMouseButtonDown(0) && !gameStarted) StartGame();
    }

    void StartGame()
    {
        gameStarted = true;
        rb.gravityScale = 0.6f;

        // Kích hoạt script cá và cho nhảy ngay lập tức
        var fish = player.GetComponent<PlayerFishScript>();
        if (fish)
        {
            fish.hasStarted = true;
            player.SendMessage("Fly");
        }

        SetUIState(gameMode: true);
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
    }

    // Hàm dùng chung cho các nút Menu
    public void SetOptionMenu(bool active) => optionMenu?.SetActive(active);

    public void RestartGame() => ChangeScene(SceneManager.GetActiveScene().name);

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverMenu?.SetActive(true);
        GameUI?.SetActive(false);
        pipeControler?.SetActive(false);
        enabled = false;
    }

    // Hàm phụ trợ để dọn dẹp logic bật/tắt UI khi bắt đầu
    private void SetUIState(bool gameMode)
    {
        Tutorial?.SetActive(!gameMode);
        GameUI?.SetActive(gameMode);
        pipeControler?.SetActive(gameMode);
    }
}