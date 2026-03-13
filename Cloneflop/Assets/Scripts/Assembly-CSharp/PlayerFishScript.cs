using UnityEngine;
using TMPro;

public class PlayerFishScript : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isDead = false;

    public AudioClip dieSound;
    public AudioClip scoreSound;
    public AudioClip fihSound;
    public float audioVolume = 0.5f;

    public float maxUpAngle = 30f;
    public float maxDownAngle = -90f;
    public float rotateSpeed = 5f;

    public int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI lastScoreText;

    private int highScore;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = highScore.ToString();

        UpdateScoreUI();
    }

    void Update()
    {
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Fly();
        }

        RotateFish();
    }

    void Fly()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        transform.rotation = Quaternion.Euler(0, 0, maxUpAngle);

        if (fihSound != null)
            AudioSource.PlayClipAtPoint(fihSound, transform.position, audioVolume);
    }

    void RotateFish()
    {
        float velocityY = rb.linearVelocity.y;

        float angle = Mathf.Lerp(maxDownAngle, maxUpAngle, (velocityY + 5f) / 10f);
        angle = Mathf.Clamp(angle, maxDownAngle, maxUpAngle);

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("ScoreZone"))
        {
            score++;
            UpdateScoreUI();

            if (scoreSound != null)
                AudioSource.PlayClipAtPoint(scoreSound, transform.position, audioVolume);
        }

        if (collision.CompareTag("Pipe") || collision.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    void GameOver()
    {
        isDead = true;

        if (dieSound != null)
            AudioSource.PlayClipAtPoint(dieSound, transform.position, audioVolume);

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 4;

        if (lastScoreText != null)
            lastScoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (highScoreText != null)
            highScoreText.text = highScore.ToString();

        enabled = false;
    }
}