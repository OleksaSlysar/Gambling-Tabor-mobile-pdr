// _Scripts/Managers/CheckpointManager.cs
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private Transform lastCheckpoint; 

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnToLastCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetAllCheckpoints();
        }
    }

    public void SetNewCheckpoint(Transform newCheckpointTransform)
    {
        lastCheckpoint = newCheckpointTransform;
        
        Debug.Log("=== ЧЕКПОІНТ ПРОЙДЕНО: " + newCheckpointTransform.name + " ===");
    }


    private void ReturnToLastCheckpoint()
    {
        if (lastCheckpoint != null)
        {
        
            Debug.Log("--- [R] ЗАГЛУШКА: Повернення до останнього чекпоінту (" + lastCheckpoint.name + ") ---");
            
         
        }
        else
        {
            Debug.LogWarning("--- [R] ЗАГЛУШКА: Немає чекпоінтів, до яких можна повернутися! ---");
        }
    }

    private void ResetAllCheckpoints()
    {
        lastCheckpoint = null; 
        Debug.Log("--- [T] ЗАГЛУШКА: Скидання всіх чекпоінтів ---");
    }
}