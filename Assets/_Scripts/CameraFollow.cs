using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;

    [Header("Ajustes de Cámara")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Camera cam;
    private float minCameraX, maxCameraX;
    private float minY, maxY;

    private void Start()
    {
        cam = GetComponent<Camera>();
        CalculateCameraBounds();
    }

    void CalculateCameraBounds()
    {
        if (GridGenerator.Instance == null) return;

        // Tamaños visibles de la cámara en el mundo
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        // Bordes exteriores absolutos del grid
        float gridLeft = GridGenerator.Instance.GetMinX();
        float gridRight = GridGenerator.Instance.GetMaxX();
        float gridTop = GridGenerator.Instance.GetTopY();
        float gridBottom = GridGenerator.Instance.GetBottomY();

        // --- LÍMITES HORIZONTALES (X) EXACTOS ---
        // Sumamos y restamos el 'horzExtent' para que la cámara frene justo 
        // cuando su borde de pantalla toca la línea roja del grid.
        minCameraX = gridLeft + horzExtent;
        maxCameraX = gridRight - horzExtent;

        if (minCameraX > maxCameraX) minCameraX = maxCameraX = 0f;

        // --- LÍMITES VERTICALES (Y) ---
        maxY = gridTop;
        minY = gridBottom + vertExtent;

        if (minY > maxY)
        {
            minY = maxY = (gridBottom + gridTop) / 2f;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        float clampedX = Mathf.Clamp(targetPosition.x, minCameraX, maxCameraX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

        Vector3 finalTarget = new Vector3(clampedX, clampedY, offset.z);

        transform.position = Vector3.Lerp(transform.position, finalTarget, smoothSpeed * Time.deltaTime);
    }
}