using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Pantalla Final")]
    public EndGameUI endGameUI;
    public int totalShots = 0;

    [Header("Controladores de Spawns")]
    public CarManager spawnController;

    [Header("Configuración de Oleadas")]
    public int totalRounds = 10;
    public float baseSpawnDelay = 2f;
    public float minSpawnDelay = 0.5f;
    public int carsPerRound = 10;
    public float roundDelay = 3f;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioClip levelMusic;           
    public AudioClip endGameMusic;

    [Header("UI")]
    public GameUI gameUI;

    int currentRound = 0;
    public bool gameActive;
    int totalScore = 0;

    private void Start()
    {
        musicSource.clip = levelMusic;
        musicSource.loop = false;
        musicSource.Play();

        gameActive = false;
        totalScore = 0;
        gameUI?.AddScore(0);       
        gameUI?.UpdateWave(0);     
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        gameActive = true;

        for (currentRound = 1; currentRound <= totalRounds; currentRound++)
        {
            gameUI?.UpdateWave(currentRound);

            float spawnDelay = Mathf.Max(baseSpawnDelay - (currentRound * 0.15f), minSpawnDelay);

            yield return StartCoroutine(SpawnRound(carsPerRound, spawnDelay));

            yield return new WaitForSeconds(roundDelay);
        }

        gameActive = false;
        Debug.Log("🎉 Juego completado");

        EndGame();
        endGameUI?.ShowEndScreen(totalScore, totalShots);
    }

    IEnumerator SpawnRound(int cars, float delay)
    {
        for (int i = 0; i < cars; i++)
        {
            spawnController.SpawnRandomCar();
            yield return new WaitForSeconds(delay);
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        gameUI?.AddScore(amount);
        Debug.Log($"+{amount} pts  (Total: {totalScore})");
    }

    public void EndGame()
    {

        musicSource.Stop();

        musicSource.clip = endGameMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void Playsound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}
