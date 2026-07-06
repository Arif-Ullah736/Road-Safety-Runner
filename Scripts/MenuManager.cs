using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public float clickDelay = 0.2f;

    public void StartGame()
    {
        StartCoroutine(LoadSceneAfterClick("Game"));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadSceneAfterClick("MainMenu"));
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterClick());
    }

    IEnumerator LoadSceneAfterClick(string sceneName)
    {
        PlayClickSound();
        yield return new WaitForSeconds(clickDelay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator QuitAfterClick()
    {
        PlayClickSound();
        yield return new WaitForSeconds(clickDelay);

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void PlayClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}