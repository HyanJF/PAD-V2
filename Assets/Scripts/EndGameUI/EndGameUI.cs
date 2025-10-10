using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TMP_Text scoreText;
    public TMP_Text shotsText;

    public GameController gC;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowEndScreen(int finalScore, int totalShots)
    {
        gameObject.SetActive(true);
        scoreText.text = $"{finalScore}";
        shotsText.text = $"{totalShots}";
    }

    public void OnRestartGame(InputAction.CallbackContext context)
    {
        if (gC.gameActive) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReturnToMenu(InputAction.CallbackContext context)
    {
        if (gC.gameActive) return;
        SceneManager.LoadScene("MainMenu");
    }
}