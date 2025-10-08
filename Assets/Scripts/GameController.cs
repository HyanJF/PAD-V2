using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Controladores de Spawns")]
    public CarManager spawnController;

    [Header("Configuración de Oleadas")]
    public int totalRounds = 10;
    public float baseSpawnDelay = 2f;
    public float minSpawnDelay = 0.5f;
    public int carsPerRound = 10;
    public float roundDelay = 3f;

    [Header("UI")]
    public GameUI gameUI;

    int currentRound = 0;
    bool gameActive = false;
    int totalScore = 0; // score acumulado

    private void Start()
    {
        totalScore = 0;
        gameUI?.AddScore(0);       // muestra 0 al inicio
        gameUI?.UpdateWave(0);     // muestra 0 o lo que prefieras
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        gameActive = true;

        for (currentRound = 1; currentRound <= totalRounds; currentRound++)
        {
            gameUI?.UpdateWave(currentRound); // actualizar UI

            float spawnDelay = Mathf.Max(baseSpawnDelay - (currentRound * 0.15f), minSpawnDelay);

            yield return StartCoroutine(SpawnRound(carsPerRound, spawnDelay));

            yield return new WaitForSeconds(roundDelay);
        }

        gameActive = false;
        Debug.Log("🎉 Juego completado");
        // aquí podrías activar UI de victoria
    }

    IEnumerator SpawnRound(int cars, float delay)
    {
        for (int i = 0; i < cars; i++)
        {
            spawnController.SpawnRandomCar();
            yield return new WaitForSeconds(delay);
        }
    }

    // Llamar desde CarController al destruirse: FindObjectOfType<GameController>().AddScore(value);
    public void AddScore(int amount)
    {
        totalScore += amount;
        gameUI?.AddScore(amount); // delega la visualización al GameUI
        Debug.Log($"+{amount} pts  (Total: {totalScore})");
    }
}
