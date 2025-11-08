// TrafficSystem.cs
using UnityEngine;

public class TrafficSystem : MonoBehaviour
{
    public RaycastCar player;
    
    // ... посилання на всі світлофори, знаки ...

    void Update()
    {
        CheckSpeeding();
        CheckRedLightViolation();
        // ... інші перевірки ...
    }

    void CheckSpeeding()
    {
        // Логіка перевірки швидкості гравця у поточній зоні
        // if (player.currentSpeed > currentSpeedLimit) { PenalizePlayer("Перевищення"); }
    }

    void CheckRedLightViolation()
    {
        // Логіка перевірки перетину стоп-лінії на червоний
    }

    public void PenalizePlayer(string violationType, int fine)
    {
        Debug.Log($"Порушення: {violationType}! Штраф: {fine}");
        // ... Зберегти штраф в БД
    }
}