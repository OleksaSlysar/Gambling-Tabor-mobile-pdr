// _Scripts/Player/PlayerVehicle.cs
using UnityEngine;

// Наслідуємо наш клас Vehicle
public class PlayerVehicle : Vehicle 
{
    // Ми використовуємо "стару" систему вводу, оскільки
    // налаштували проект на "Both"

    protected override float GetMotorInput()
{
    float motor = Mathf.Clamp01(Input.GetAxis("Vertical"));
    Debug.Log("Motor Input: " + motor); // <-- ДОДАЙТЕ ЦЕЙ РЯДОК
    return motor;
}

    protected override float GetSteeringInput()
    {
        // A дає -1, D дає +1
        return Input.GetAxis("Horizontal");
    }

    protected override float GetBrakeInput()
    {
        // Гальмуємо, якщо натиснуто "S" (задній хід)
        // або якщо натиснуто "Пробіл"
        float brake_S = Mathf.Abs(Mathf.Min(0, Input.GetAxis("Vertical")));
        float brake_Space = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        
        return Mathf.Max(brake_S, brake_Space);
    }
}