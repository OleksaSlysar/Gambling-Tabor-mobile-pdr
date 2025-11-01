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
    

    [Header("Налаштування Телепортації")]
    [Tooltip("Перетягніть сюди GameObject вашої машини")]
    public Rigidbody playerRigidbody; 

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
       
        if (lastCheckpoint != null && playerRigidbody != null)
        {
           
            playerRigidbody.isKinematic = true; 

           
            playerRigidbody.transform.position = lastCheckpoint.position;
            
          
            playerRigidbody.transform.rotation = lastCheckpoint.rotation;

         
            playerRigidbody.linearVelocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;

           
            Invoke(nameof(ReEnablePhysics), 0.1f);
            
            Debug.Log("--- [R] Машину повернуто до чекпоінту: " + lastCheckpoint.name + " ---");
        }
        else if (playerRigidbody == null)
        {
            Debug.LogError("--- [R] Не можу повернути! 'Player Rigidbody' не призначено у CheckpointManager!");
        }
        else
        {
            Debug.LogWarning("--- [R] Немає чекпоінтів, до яких можна повернутися! ---");
        }
    }


    private void ReEnablePhysics()
    {
        playerRigidbody.isKinematic = false;
    }

    private void ResetAllCheckpoints()
    {
        lastCheckpoint = null; 
        Debug.Log("--- [T] Скидання всіх чекпоінтів ---");
    }
}