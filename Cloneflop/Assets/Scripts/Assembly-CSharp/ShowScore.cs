using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ShowScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI instead of Text
    public ScoreScript scoreScript; // Allow manual assignment of ScoreScript

    void Start()
    {
        if (scoreScript == null)
        {
            // Attempt to find ScoreScript if not manually assigned
            scoreScript = FindObjectOfType<ScoreScript>();

            if (scoreScript == null)
            {
                Debug.LogError("ScoreScript not found in the scene. Please assign it manually.");
            }
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText UI element is not assigned.");
        }
    }

    void Update()
    {
        if (scoreScript != null && scoreText != null)
        {
            // Cập nhật điểm số trên UI
            scoreText.text = "Score: " + scoreScript.GetScore();
        }
    }
}
