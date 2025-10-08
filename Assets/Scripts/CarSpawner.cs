using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Prefabs de coches disponibles")]
    public GameObject[] carPrefabs;

    [Header("Rotación Y del coche")]
    public float yRotation = 0f;

    public GameObject SpawnCar()
    {
        if (carPrefabs == null || carPrefabs.Length == 0)
        {
            Debug.LogWarning("⚠️ No hay prefabs asignados.");
            return null;
        }

        // Elegir un prefab aleatorio
        int index = Random.Range(0, carPrefabs.Length);
        GameObject prefab = carPrefabs[index];

        Vector3 spawnPos = transform.position;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);

        GameObject newCar = Instantiate(prefab, spawnPos, rotation);

        // Inicializa el CarController si existe
        CarController car = newCar.GetComponent<CarController>();
        if (car != null)
            car.Initialize();

        return newCar;
    }
}
