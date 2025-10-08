using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Spawners disponibles")]
    public CarSpawner[] spawners;

    // Llama a un spawner aleatorio
    public void SpawnRandomCar()
    {
        if (spawners == null || spawners.Length == 0)
        {
            Debug.LogWarning("⚠️ No hay spawners asignados.");
            return;
        }

        int index = Random.Range(0, spawners.Length);
        spawners[index].SpawnCar();
    }
}
