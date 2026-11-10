using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Salto")]
    public float moveSpeed = 8f;
    public float jumpForce = 6f;       // Fuerza del salto
    public float playerRadius = 0.5f;
    public LayerMask groundLayer;      // Opcional para detectar suelo

    private Rigidbody rb;
    private float minX;
    private float maxX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (rb != null)
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        if (GridGenerator.Instance != null)
        {
            minX = GridGenerator.Instance.GetMinX() + playerRadius;
            maxX = GridGenerator.Instance.GetMaxX() - playerRadius;
        }
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 1. Movimiento horizontal manteniendo la velocidad de caída vertical
        Vector3 velocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0f);
        rb.linearVelocity = velocity;

        // 2. Detección de salto (Barra espaciadora o W / Flecha arriba)
        // Comprobamos si la velocidad vertical está muy cerca de 0 para asegurar que está en el suelo
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0f);
        }

        // Clamping en X para que no se salga del mapa
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}