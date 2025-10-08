using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    public int life = 1; // vida del coche
    public float lifeTime = 10f;
    public int scoreValue = 1;

    private bool initialized = false;

    public void Initialize()
    {
        initialized = true;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (!initialized) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void TakeDamage()
    {
        life--;
        if (life <= 0)
        {
            var controller = FindAnyObjectByType<GameController>();
            if (controller != null)
            {
                controller.AddScore(scoreValue);
            }
            Destroy(gameObject);
            Debug.Log("🚗 Coche destruido");
        }
    }
}
