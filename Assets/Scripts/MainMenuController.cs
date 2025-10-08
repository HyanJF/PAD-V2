using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject menuPanel;
    public Image blackPanel;

    [Header("Duración del Fade")]
    public float fadeDuration = 1.5f;

    private bool gameStarted = false;

    private void Start()
    {
        // Menú visible, juego detenido
        Time.timeScale = 0f;
        if (menuPanel != null)
            menuPanel.SetActive(true);

        // Asegura que el panel esté completamente negro
        if (blackPanel != null)
        {
            Color c = blackPanel.color;
            c.a = 1f;
            blackPanel.color = c;
        }
    }

    public void OnStartGame(InputAction.CallbackContext context)
    {
        if (!context.performed || gameStarted) return;

        gameStarted = true;
        if (menuPanel != null)
            menuPanel.SetActive(false);

        // Inicia la animación del fade
        StartCoroutine(FadeOutAndStart());
    }

    private IEnumerator FadeOutAndStart()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            if (blackPanel != null)
            {
                Color c = blackPanel.color;
                c.a = alpha;
                blackPanel.color = c;
            }

            yield return null;
        }

        if (blackPanel != null)
        {
            Color c = blackPanel.color;
            c.a = 0f;
            blackPanel.color = c;
        }

        // Cuando termina el fade, arranca el juego
        Time.timeScale = 1f;
        Debug.Log("🎮 Juego iniciado después del fade.");
    }

    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("🚪 Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
