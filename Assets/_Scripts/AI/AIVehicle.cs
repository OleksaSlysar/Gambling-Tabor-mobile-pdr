// AIVehicle.cs
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AIVehicle : Venicle
{
    private NavMeshAgent agent;
   

    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
       
    }
    
    // Тут логіка буде складнішою. 
    // ШІ має "тиснути" на віртуальні педалі
    protected override float GetMotorInput()
    {
        // Логіка ШІ для розгону
        return 1.0f; // Тимчасово
    }

    protected override float GetSteeringInput()
    {
        // Логіка ШІ для повороту
        return 0f; // Тимчасово
    }

    protected override float GetBrakeInput()
{
        // Логіка ШІ для гальмування (на світлофорі, перед гравцем)
        return 0f; // Тимчасово
    }
}