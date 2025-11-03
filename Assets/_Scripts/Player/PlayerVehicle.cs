// _Scripts/Player/PlayerVehicle.cs
using UnityEngine;

// Наслідуємо наш клас Vehicle
public class PlayerVehicle : Vehicle 
{
    // Ми використовуємо "стару" систему вводу, оскільки
    // налаштували проект на "Both"

    protected override float GetMotorInput()
    {
        // Input.GetAxis("Vertical") - це W (дає +1) і S (дає -1)
        // Ми беремо тільки позитивні значення для газу
        return Mathf.Clamp01(Input.GetAxis("Vertical"));
    }

    protected override float GetSteeringInput()
    {
        // Input.GetAxis("Horizontal") - це A (дає -1) і D (дає +1)
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