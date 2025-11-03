// InputManager.cs
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private float motorInput;
    private float steeringInput;
    private float brakeInput;

    public float GetMotorInput() => motorInput;
    public float GetSteeringInput() => steeringInput;
    public float GetBrakeInput() => brakeInput;

  
    public void SetMotor(float value) => motorInput = value;
    public void SetSteering(float value) => steeringInput = value;
    public void SetBrake(float value) => brakeInput = value;
}