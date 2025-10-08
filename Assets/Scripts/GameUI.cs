using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;

    public void UpdateWave(int waveNumber)
    {
        if (waveText != null)
            waveText.text = $"Oleada: {waveNumber}";
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        if (scoreText != null)
            scoreText.text = $"Puntos: {currentScore}";
    }
}
