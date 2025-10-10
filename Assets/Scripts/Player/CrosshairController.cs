using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairController : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float moveSpeed = 1000f;

    private Vector2 moveInput;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("SeMueve");
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rect.anchoredPosition += moveInput * moveSpeed * Time.deltaTime;

        rect.anchoredPosition = new Vector2(
            Mathf.Clamp(rect.anchoredPosition.x, -775, 775),
            Mathf.Clamp(rect.anchoredPosition.y, -430, 430)
        );
    }
}
