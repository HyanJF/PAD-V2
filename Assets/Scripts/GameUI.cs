using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public GameController gC;

    private int currentScore = 0;

    public void UpdateWave(int waveNumber)
    {
        if (waveText != null)
            waveText.text = $"Oleada: {waveNumber} / {gC.totalRounds}";
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (scoreText != null)
            scoreText.text = $"Puntos: {currentScore}";
    }
}
