using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("Referencias")]
    public Image menuImage;
    public GameObject[] menuChildren;

    [Header("Duración del Fade")]
    public float fadeDuration = 1.5f;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip transitionMusic;
    public AudioClip clickSound;

    private bool gameStarting = false;

    private void Start()
    {
        Time.timeScale = 0f;

        if (menuImage != null)
        {
            Color c = menuImage.color;
            c.a = 1f;
            menuImage.color = c;
        }

        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = .3f;
            musicSource.Play();
        }
    }

    public void OnStartGame(InputAction.CallbackContext context)
    {
        if (!context.performed || gameStarting) return;
        gameStarting = true;

        PlayClickSound();

        foreach (var child in menuChildren)
            child.SetActive(false);

        StartCoroutine(TransitionAndLoad());
    }

    private IEnumerator TransitionAndLoad()
    {
        if (menuImage != null)
        {
            float elapsed = 0f;
            Color startColor = menuImage.color;
            Color targetColor = Color.black;
            targetColor.a = 1f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                menuImage.color = Color.Lerp(startColor, targetColor, elapsed / fadeDuration);
                yield return null;
            }
            menuImage.color = targetColor;
        }

        if (musicSource != null && backgroundMusic != null)
        {
            float remainingTime = backgroundMusic.length - (musicSource.time % backgroundMusic.length);
            yield return new WaitForSecondsRealtime(remainingTime);
        }

        // Reproduce la música de transición
        if (musicSource != null && transitionMusic != null)
        {
            musicSource.loop = false;
            musicSource.clip = transitionMusic;
            musicSource.Play();

            yield return new WaitForSecondsRealtime(transitionMusic.length);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("level");
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (gameStarting) return;
        if (!context.performed) return;
        PlayClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
            sfxSource.PlayOneShot(clickSound);
    }
}
