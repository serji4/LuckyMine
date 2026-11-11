using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Instancia estática para que la cámara y el jugador puedan consultar los datos fácilmente
    public static GridGenerator Instance;

    [Header("Configuración del Mapa")]
    public int width = 100;
    public int height = 100;
    public float blockSize = 1.0f;

    [Header("Prefabs de Bloques")]
    public GameObject stonePrefab;
    public GameObject copperPrefab;
    public GameObject ironPrefab;
    public GameObject goldPrefab;
    public GameObject luckyOrePrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        float xOffset = (width * blockSize) / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = (x * blockSize) - xOffset + (blockSize / 2f);
                float posY = y * blockSize;

                Vector3 spawnPos = new Vector3(posX, posY, 0);
                GameObject prefabToSpawn = SelectBlockByDepth(y);

                GameObject newBlock = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, transform);
                newBlock.name = $"Block_{x}_{y}";
            }
        }
    }

    GameObject SelectBlockByDepth(int depth)
    {
        float roll = Random.Range(0f, 100f);

        if (depth < 10)
        {
            if (roll < 80) return stonePrefab;
            if (roll < 95) return copperPrefab;
            return ironPrefab;
        }
        else if (depth < 25)
        {
            if (roll < 50) return stonePrefab;
            if (roll < 80) return ironPrefab;
            if (roll < 95) return goldPrefab;
            return luckyOrePrefab;
        }
        else
        {
            if (roll < 30) return stonePrefab;
            if (roll < 60) return goldPrefab;
            return luckyOrePrefab;
        }
    }

    // Métodos públicos para que la Cámara y el Jugador consulten los límites exactos
    public float GetMinX() => -(width * blockSize) / 2f;
    public float GetMaxX() => (width * blockSize) / 2f;
    public float GetTopY() => 0f;
    public float GetBottomY() => -height * blockSize;
}