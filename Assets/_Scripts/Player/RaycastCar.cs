using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RaycastCar : MonoBehaviour
{
    [Header("Налаштування Керування")]
    public float motorForce = 1500f;
    public float steerForce = 2000f;
    public float maxSteerAngle = 30f;

    [Header("Налаштування Фізики")]
    public float suspensionLength = 1f;
    public float suspensionForce = 25000f;
    public float suspensionDamper = 3000f;
    public float gripFactor = 7f;
    
    [Header("Компоненти (Обов'язково)")]
    public Rigidbody rb;
    public float currentSpeedKPH; // Для TrafficSystem

    [Tooltip("Порожні об'єкти, звідки стріляти променями (FL, FR, RL, RR)")]
    public Transform[] raycastPoints; // [0]FL, [1]FR, [2]RL, [3]RR

    [Tooltip("3D-моделі коліс (FL, FR, RL, RR)")]
    public Transform[] wheelMeshes; // [0]FL, [1]FR, [2]RL, [3]RR

    private float motorInput;
    private float steeringInput;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        // 1. Отримуємо ввід від гравця
        motorInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        // 2. Оновлюємо візуальне положення коліс
        UpdateWheelVisuals();
        
        // 3. Оновлюємо публічну швидкість
        currentSpeedKPH = rb.linearVelocity.magnitude * 3.6f;
    }

    void FixedUpdate()
    {
        // 3. Застосовуємо фізику для кожного "колеса"
        for (int i = 0; i < raycastPoints.Length; i++)
        {
            ApplyPhysicsToWheel(raycastPoints[i], i);
        }
    }

    void ApplyPhysicsToWheel(Transform rayPoint, int index)
    {
        RaycastHit hit;
        Vector3 wheelPos = rayPoint.position;

        if (Physics.Raycast(wheelPos, -transform.up, out hit, suspensionLength))
        {
            // --- СИЛА ПІДВІСКИ ---
            float compression = (suspensionLength - hit.distance) / suspensionLength;
            float springForce = compression * suspensionForce;
            Vector3 wheelVelocity = rb.GetPointVelocity(wheelPos);
            float damperForce = Vector3.Dot(wheelVelocity, transform.up) * suspensionDamper;
            Vector3 suspension = transform.up * (springForce - damperForce);
            rb.AddForceAtPosition(suspension, wheelPos);

            // --- СИЛА ЗЧЕПЛЕННЯ (Проти бокового заносу) ---
            float lateralVelocity = Vector3.Dot(rb.GetPointVelocity(wheelPos), transform.right);
            Vector3 grip = -transform.right * lateralVelocity * gripFactor;
            rb.AddForceAtPosition(grip, wheelPos);

            // --- СИЛА ПОВОРОТУ (Тільки для передніх коліс) ---
            if (index < 2) // [0]FL та [1]FR
            {
                Vector3 steerDir = transform.right * steeringInput * steerForce;
                rb.AddForceAtPosition(steerDir, wheelPos);
            }

            // --- СИЛА ДВИГУНА (Тільки для задніх коліс) ---
            if (index >= 2) // [2]RL та [3]RR
            {
                Vector3 motorDir = transform.forward * motorInput * motorForce;
                rb.AddForceAtPosition(motorDir, wheelPos);
            }
        }
    }

    void UpdateWheelVisuals()
    {
        float rotationAngle = rb.linearVelocity.magnitude * 360f * Time.deltaTime;
        float steerAngle = steeringInput * maxSteerAngle;

        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            if (wheelMeshes[i] == null) continue;

            if (i < 2)
            {
                wheelMeshes[i].localRotation = Quaternion.Euler(
                    wheelMeshes[i].localRotation.eulerAngles.x, 
                    steerAngle, 
                    wheelMeshes[i].localRotation.eulerAngles.z
                );
            }
            wheelMeshes[i].Rotate(rotationAngle, 0, 0, Space.Self);
        }
    }

    // "Розмітка" для вікна Scene
    private void OnDrawGizmos()
    {
        if (rb == null && GetComponent<Rigidbody>()) rb = GetComponent<Rigidbody>();
        if (rb == null) return;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.TransformPoint(rb.centerOfMass), 0.1f);

        if (raycastPoints == null || raycastPoints.Length != 4) return;

        foreach (Transform rayPoint in raycastPoints)
        {
            if (rayPoint == null) continue;

            RaycastHit hit;
            Vector3 rayStart = rayPoint.position;
            Vector3 rayDirection = -transform.up;

            if (Physics.Raycast(rayStart, rayDirection, out hit, suspensionLength))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(rayStart, hit.point);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hit.point, 0.05f);
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(rayStart, rayStart + rayDirection * suspensionLength);
            }
        }
    }

    // Логіка аварій
    private void OnCollisionEnter(Collision collision)
    {
        // Перевіряємо, чи це НЕ дорога
        if (!collision.gameObject.CompareTag("Road"))
        {
            // І чи це НЕ інший гравець або чекпоінт
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Checkpoint"))
            {
                Debug.Log("АВАРІЯ! Врізалися в: " + collision.gameObject.name);
                if (CheckpointManager.Instance != null)
                {
                    // Викликаємо публічний метод
                    CheckpointManager.Instance.ReturnToLastCheckpoint();
                }
            }
        }
    }
}