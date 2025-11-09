using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCar_v3 : MonoBehaviour
{
    [Header("Налаштування Керування")]
    [Tooltip("Максимальна швидкість вперед (в метрах/сек)")]
    public float maxMoveSpeed = 15f; 
    [Tooltip("Швидкість обертання (в градусах/сек)")]
    public float turnSpeed = 100f; 

    // --- (Секцію "Візуальні Ефекти" повністю видалено) ---

    private Rigidbody rb;
    private float motorInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.automaticCenterOfMass = false; 
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        // Отримуємо "команди" від InputManager
        motorInput = InputManager.Instance.GetMotorInput();
        turnInput = InputManager.Instance.GetTurnInput();
        
        // --- (Виклик UpdateWheelVisuals() видалено) ---
    }
    
    // --- (Функцію UpdateWheelVisuals() повністю видалено) ---

    // FixedUpdate застосовує фізику
    void FixedUpdate()
    {
        // 1. Поворот (Обертання)
        float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
        
        // 2. Рух (Переміщення)
        Vector3 moveDistance = -transform.forward * motorInput * maxMoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDistance);
    }

    // Логіка Аварій (Без змін)
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Road") && !collision.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("АВАРІЯ! Врізалися в: " + collision.gameObject.name);
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.ReturnToLastCheckpoint();
            }
        }
    }
}