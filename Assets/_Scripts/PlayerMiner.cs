using UnityEngine;

public class PlayerMiner : MonoBehaviour
{
    [Header("Configuración de Minado")]
    public LayerMask blockLayer;

    private void Update()
    {
        // Picar con Clic Izquierdo del ratón o tecla E
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            TryMineAccurate();
        }
    }

    void TryMineAccurate()
    {
        // 1. Detectar dirección horizontal (A / D)
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 dir = Vector3.down; // Por defecto abajo

        if (h < 0) dir = Vector3.left;
        else if (h > 0) dir = Vector3.right;

        // 2. DISTANCIA CORTA (El bloque que tienes pegado justo delante/abajo)
        // Usamos 0.5 unidades para que detecte el bloque adyacente inmediato sin saltarse al siguiente
        Vector3 closeTargetPos = transform.position + (dir * 0.5f);

        Collider[] closeHits = Physics.OverlapSphere(closeTargetPos, 0.4f);
        foreach (Collider hit in closeHits)
        {
            // Dentro de tu bucle de destrucción en PlayerMiner.cs:
            if (IsBlock(hit))
            {
                Debug.Log($"¡Bloque destruido: {hit.gameObject.name}!");

                // Añadimos esto para que sume a la UI:
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddDestroyedBlock();
                }

                Destroy(hit.gameObject);
                return;
            }
        }

        // 3. DISTANCIA MEDIA (Por si el jugador está un poco separado y el bloque está a 1 unidad)
        Vector3 farTargetPos = transform.position + (dir * 1.0f);

        Collider[] farHits = Physics.OverlapSphere(farTargetPos, 0.4f);
        foreach (Collider hit in farHits)
        {
            if (IsBlock(hit))
            {
                Debug.Log($"¡Bloque a distancia destruido: {hit.gameObject.name}!");

                // Añadimos esto para que sume a la UI:
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddDestroyedBlock();
                }

                Destroy(hit.gameObject);
                return;
            }
        }

        // 4. PLAN B: Si no encuentra nada en la dirección elegida, comprueba el suelo bajo los pies
        Collider[] groundHits = Physics.OverlapSphere(transform.position + (Vector3.down * 0.4f), 0.4f);
        foreach (Collider hit in groundHits)
        {
            if (IsBlock(hit))
            {
                Debug.Log($"¡Suelo destruido bajo los pies: {hit.gameObject.name}!");

                // Añadimos esto para que sume a la UI:
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddDestroyedBlock();
                }

                Destroy(hit.gameObject);
                return;
            }
        }

        Debug.Log("No hay ningún bloque al alcance para minar.");
    }

    // Método auxiliar para identificar si es un bloque de forma segura
    bool IsBlock(Collider col)
    {
        return col.gameObject.name.Contains("Block") || col.CompareTag("Block");
    }
}