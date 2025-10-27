// InputManager.cs
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Ці значення будуть змінюватись через UI кнопки (OnPointerDown, OnPointerUp)
    private float motorInput;
    private float steeringInput;
    private float brakeInput;

    public float GetMotorInput() => motorInput;
    public float GetSteeringInput() => steeringInput;
    public float GetBrakeInput() => brakeInput;

    // Методи, які будуть викликатися UI кнопками
    public void SetMotor(float value) => motorInput = value;
    public void SetSteering(float value) => steeringInput = value;
    public void SetBrake(float value) => brakeInput = value;
}