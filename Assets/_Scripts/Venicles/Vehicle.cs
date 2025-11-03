
// _Scripts/Vehicles/Vehicle.cs
using UnityEngine;

// Вимагає Rigidbody на машині
[RequireComponent(typeof(Rigidbody))]
public abstract class Vehicle : MonoBehaviour
{
    [Header("Фізичні Компоненти")]
    [Tooltip("Масив з 4-х WheelCollider (FL, FR, RL, RR)")]
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    
    [Tooltip("Масив з 4-х 3D-моделей коліс (FL, FR, RL, RR)")]
    public Transform[] wheelMeshes = new Transform[4];

    [Header("Параметри Руху")]
    public float motorTorque = 2000f; 
    public float brakeTorque = 3000f; 
    public float maxSteeringAngle = 35f; 

    protected Rigidbody rb;
    public float currentSpeedKPH; 

  
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        
       
        rb.centerOfMass = new Vector3(0, -0.5f, 0); 
    }

  
    protected virtual void FixedUpdate()
    {
   
        float motor = GetMotorInput() * motorTorque;
        float steering = GetSteeringInput() * maxSteeringAngle;
        float brake = GetBrakeInput() * brakeTorque;

    
        for (int i = 0; i < wheelColliders.Length; i++)
        {
           
            if (i < 2) 
            {
                wheelColliders[i].steerAngle = steering;
            }

           
            if (brake > 0)
            {
                wheelColliders[i].brakeTorque = brake;
                wheelColliders[i].motorTorque = 0f; 
            }
            else
            {
                wheelColliders[i].brakeTorque = 0f;
            
                wheelColliders[i].motorTorque = motor; 
            }

            UpdateWheelMesh(wheelColliders[i], wheelMeshes[i]);
        }
        
        currentSpeedKPH = rb.linearVelocity.magnitude * 3.6f;
    }


    protected abstract float GetMotorInput();
    protected abstract float GetSteeringInput();
    protected abstract float GetBrakeInput();
    
    void UpdateWheelMesh(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot); 
        mesh.position = pos;
        mesh.rotation = rot;
    }
}