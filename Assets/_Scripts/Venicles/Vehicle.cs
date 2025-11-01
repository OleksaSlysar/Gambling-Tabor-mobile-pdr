
using UnityEngine;
public abstract class Venicle : MonoBehaviour
{

    public float maxSpeed = 120f;
    public float currentSpeed;
    public float steeringAngle = 30f;
    public float motorTorque = 1500f;
    public float brakeTorque = 3000f;
    [Header("Components")]
    public WheelCollider[] wheelColliders;
    public Transform[] wheelMeshes;
    public virtual void Start() { }

    protected abstract float GetMotorInput();
    protected abstract float GetSteeringInput();
    protected abstract float GetBrakeInput();

    protected virtual void FixedUpdate()
    {
        float motor = GetMotorInput() * motorTorque;
        float steering = GetSteeringInput() * steeringAngle;
        float brake = GetBrakeInput() * brakeTorque;

        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (i < 2)
            {
                wheelColliders[i].steerAngle = steering;
            }

            wheelColliders[i].motorTorque = motor;
            wheelColliders[i].brakeTorque = brake;

            UpdateWheelMesh(wheelColliders[i], wheelMeshes[i]);


        }
        currentSpeed = wheelColliders[0].rpm * wheelColliders[0].radius * 2f * Mathf.PI / 60f * 3.6f;
    }
    
    void UpdateWheelMesh(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;

    }
    
}
