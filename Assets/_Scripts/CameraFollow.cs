using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;

    [Header("Ajustes de Cámara")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);


    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}