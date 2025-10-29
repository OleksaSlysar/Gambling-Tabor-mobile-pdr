using UnityEngine;

public class PlayerVehicle : Venicle
{
    private InputManager inputManager;


    public override void Start()
{
    base.Start();
    
    inputManager = FindObjectOfType<InputManager>();
}
protected override float GetMotorInput()
    {
        return inputManager.GetMotorInput();
    }

    protected override float GetSteeringInput()
    {
        return inputManager.GetSteeringInput();
    }

    protected override float GetBrakeInput()
    {
        return inputManager.GetBrakeInput();
    }
}
