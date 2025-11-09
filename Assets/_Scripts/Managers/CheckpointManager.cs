using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [Header("Налаштування Телепортації")]
    public Rigidbody playerRigidbody; // Перетягніть сюди машину

    [Header("Логіка Чекпоінтів")]
    [Tooltip("Найперша точка старту. Встановіть в інспекторі.")]
    public Transform startingCheckpoint; // <-- НОВЕ ПОЛЕ

    private Transform lastCheckpoint; // Останній пройдений чекпоінт

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }

    void Start()
    {
        // При старті гри, "стартовий" чекпоінт 
        // АВТОМАТИЧНО стає нашим "останнім" чекпоінтом.
        if (startingCheckpoint != null)
        {
            lastCheckpoint = startingCheckpoint;
            // Телепортуємо гравця на старт (про всяк випадок)
            TeleportPlayer(startingCheckpoint);
        }
        else
        {
            Debug.LogError("СТАРТОВИЙ ЧЕКПОІНТ ('Starting Checkpoint') не призначено в CheckpointManager!");
        }
    }

    // Цей метод викликається тригерами завдань (LessonObjectiveTrigger)
    public void SetNewCheckpoint(Transform newCheckpointTransform)
    {
        lastCheckpoint = newCheckpointTransform;
        Debug.Log("=== НОВИЙ ЧЕКПОІНТ: " + newCheckpointTransform.name + " ===");
    }

    // --- ПУБЛІЧНІ МЕТОДИ ДЛЯ ПЕРЕЗАПУСКУ ---

    // Телепортує до ОСТАННЬОГО
    public void ReturnToLastCheckpoint()
    {
        if (lastCheckpoint != null && playerRigidbody != null)
        {
            Debug.Log("--- Повернення до ОСТАННЬОГО чекпоінту: " + lastCheckpoint.name + " ---");
            TeleportPlayer(lastCheckpoint);
        }
    }

    // Телепортує до НАЙПЕРШОГО
    public void ReturnToStartCheckpoint()
    {
        if (startingCheckpoint != null && playerRigidbody != null)
        {
            Debug.Log("--- Повернення до СТАРТОВОГО чекпоінту: " + startingCheckpoint.name + " ---");
            TeleportPlayer(startingCheckpoint);
        }
    }

    // --- Приватний метод телепортації ---
    private void TeleportPlayer(Transform targetCheckpoint)
    {
        // Тимчасово вимикаємо фізику, щоб уникнути "вибухів"
        playerRigidbody.isKinematic = true; 
        
        // Телепортуємо
        playerRigidbody.transform.position = targetCheckpoint.position;
        playerRigidbody.transform.rotation = targetCheckpoint.rotation;

        // Скидаємо всю швидкість
        playerRigidbody.linearVelocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;

        // Вмикаємо фізику назад через долю секунди
        Invoke(nameof(ReEnablePhysics), 0.1f);
    }
    
    private void ReEnablePhysics()
    {
        playerRigidbody.isKinematic = false;
    }
}