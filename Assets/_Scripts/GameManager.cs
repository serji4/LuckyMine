using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro (recomendado)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI blocksDestroyedText; // Arrastra aquí tu texto de la UI

    private int destroyedCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddDestroyedBlock()
    {
        destroyedCount++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (blocksDestroyedText != null)
        {
            blocksDestroyedText.text = "Bloques minados: " + destroyedCount;
        }
    }
}