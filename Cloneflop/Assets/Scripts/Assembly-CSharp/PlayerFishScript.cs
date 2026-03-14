using UnityEngine;
using TMPro;


public class PlayerFishScript : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isDead = false;
    public bool hasStarted = false;

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

    public int highScore;
    private static int deathCount = 0;

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

        // Chỉ thực hiện logic khi game đã bắt đầu
        if (hasStarted)
        {
            // Phím Space hoặc Click chuột để nhảy tiếp sau khi đã Start
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Fly();
            }

            // Chỉ bắt đầu xoay khi game đã chạy
            RotateFish();
        }
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
        if (lastScoreText != null)
            lastScoreText.text = score.ToString();
    }

    void GameOver()
    {
        isDead = true;
        deathCount++;

        //Tiếng cá chết
        if (dieSound != null)
            AudioSource.PlayClipAtPoint(dieSound, transform.position, audioVolume);
        // Dừng chuyển động và tăng trọng lực để cá rơi nhanh hơn
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 4;
        // Hiển thị điểm số cuối cùng và cập nhật điểm cao nếu cần
        if (lastScoreText != null)
            lastScoreText.text = score.ToString();
        // Cập nhật điểm cao 
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        // Hiển thị điểm cao
        if (highScoreText != null)
            highScoreText.text = highScore.ToString();

        enabled = false;
        // Cứ mỗi 3 lần chết thì hiện quảng cáo 1 lần
        if (deathCount >= 3)
        {
            AdsScript ads = FindObjectOfType<AdsScript>();
            if (ads != null)
            {
                ads.ShowAd();
                deathCount = 0; // Reset lại biến đếm
            }
        }
    }
}