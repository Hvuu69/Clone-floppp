using UnityEngine;

public class PlayerFishScript : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 5f;
    public float rotateSpeed = 5f;
    public float maxUpAngle = 30f, maxDownAngle = -90f;

    [Header("Audio")]
    public AudioClip dieSound, scoreSound, fihSound;
    public float audioVolume = 0.5f;

    private Rigidbody2D rb;
    private static int deathCount = 0;
    [HideInInspector] public bool isDead = false, hasStarted = false;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (isDead || !hasStarted) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) Fly();
        RotateFish();
    }

    void Fly()
    {
        rb.linearVelocity = Vector2.up * jumpForce;
        transform.rotation = Quaternion.Euler(0, 0, maxUpAngle);
        PlaySound(fihSound);
    }

    void RotateFish()
    {
        float angle = Mathf.Clamp(Mathf.Lerp(maxDownAngle, maxUpAngle, (rb.linearVelocity.y + 5f) / 10f), maxDownAngle, maxUpAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) return;

        if (col.CompareTag("ScoreZone"))
        {
            ScoreManager.Instance.AddScore(1); // Gọi trực tiếp từ ScoreManager
            PlaySound(scoreSound);
        }
        else if (col.CompareTag("Pipe") || col.CompareTag("Ground"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isDead = true;
        deathCount++;
        PlaySound(dieSound);

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 4;
        // Yêu cầu ScoreManager kiểm tra điểm cao
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveAndCheckData();
        }

        if (deathCount >= 3)
        {
            FindObjectOfType<AdsScript>()?.ShowAd();
            deathCount = 0;
        }
        enabled = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip) AudioSource.PlayClipAtPoint(clip, transform.position, audioVolume);
    }
}