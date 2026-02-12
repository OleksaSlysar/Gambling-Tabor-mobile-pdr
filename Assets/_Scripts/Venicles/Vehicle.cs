// _Scripts/Vehicles/Vehicle.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Vehicle : MonoBehaviour
{
    [Header("Фізичні Компоненти")]
    [Tooltip("Масив з 4-х WheelCollider (FL, FR, RL, RR)")]
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    
    [Tooltip("Масив з 4-х 3D-моделей коліс (FL, FR, RL, RR)")]
    public Transform[] wheelMeshes = new Transform[4];

    [Header("Параметри Руху")]
    public float motorTorque = 2500f; // Сила двигуна
    public float brakeTorque = 5000f; // Сила гальм
    public float maxSteeringAngle = 35f; // Максимальний кут повороту коліс

    protected Rigidbody rb;
    public float currentSpeedKPH;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); 
    }

    public virtual void FixedUpdate()
    {
        // 1. Отримуємо команди від "нащадка" (гравця)
        float motor = GetMotorInput() * motorTorque;
        float steering = GetSteeringInput() * maxSteeringAngle;
        float brake = GetBrakeInput() * brakeTorque;

        // 2. Застосовуємо команди до колайдерів коліс
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            // Перші два колеса (0 і 1) - передні, вони повертають
            if (i < 2) 
            {
                wheelColliders[i].steerAngle = steering;
            }

            // Застосовуємо гальма
            if (brake > 0)
            {
                wheelColliders[i].brakeTorque = brake;
                wheelColliders[i].motorTorque = 0f;
            }
            else // Або газ
            {
                wheelColliders[i].brakeTorque = 0f;
                wheelColliders[i].motorTorque = motor; 
            }

            // 3. Оновлюємо візуальне положення 3D-моделей коліс
            UpdateWheelMesh(wheelColliders[i], wheelMeshes[i]);
        }
        
        currentSpeedKPH = rb.linearVelocity.magnitude * 3.6f;
    }

    // "Нащадок" (PlayerVehicle) має надати ці методи
    protected abstract float GetMotorInput();
    protected abstract float GetSteeringInput();
    protected abstract float GetBrakeInput();
    
    // Синхронізація 3D-моделі колеса з фізичним колайдером
    void UpdateWheelMesh(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }
}