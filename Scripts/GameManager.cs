using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives = 3;
    public int totalSigns = 5;
    public int collectedSigns = 0;

    public float timeLeft = 60f;

    public TMP_Text livesText;
    public TMP_Text signsText;
    public TMP_Text timerText;
    public TMP_Text messageText;

    public AudioClip backgroundMusic;
    public AudioClip hitSound;
    public AudioClip collectSound;

    private AudioSource audioSource;
    private bool gameEnded = false;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        UpdateLivesText();
        UpdateSignsText();
        UpdateTimerText();

        if (messageText != null)
        {
            messageText.text = "";
        }

        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 0.3f;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (gameEnded)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            UpdateTimerText();
            GameOver();
        }

        UpdateTimerText();
    }

    public bool LoseLife()
    {
        lives--;
        UpdateLivesText();

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (lives <= 0)
        {
            GameOver();
            return false;
        }

        ShowMessage("Be careful! Look both ways before crossing.");
        return true;
    }

    public void CollectSign()
    {
        collectedSigns++;
        UpdateSignsText();

        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        if (collectedSigns >= totalSigns)
        {
            ShowMessage("Great! Now go to school safely.");
        }
        else
        {
            ShowMessage("Safety sign collected!");
        }
    }

    public void TryWin()
    {
        if (collectedSigns >= totalSigns)
        {
            gameEnded = true;
            SceneManager.LoadScene("Win");
        }
        else
        {
            ShowMessage("Collect all safety signs first!");
        }
    }

    void GameOver()
    {
        gameEnded = true;
        SceneManager.LoadScene("GameOver");
    }

    void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives;
    }

    void UpdateSignsText()
    {
        signsText.text = "Signs: " + collectedSigns + "/" + totalSigns;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }

    void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            StopAllCoroutines();
            StartCoroutine(ClearMessage());
        }
    }

    IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(2f);

        if (messageText != null)
        {
            messageText.text = "";
        }
    }
}