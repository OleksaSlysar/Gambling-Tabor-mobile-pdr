// _Scripts/Player/PlayerVehicle_WASD.cs

using UnityEngine;


public class PlayerVehicle_WASD : Vehicle 
{
  

    protected override void Start()
    {
        
        base.Start(); 
       
    }

    protected override float GetSteeringInput()
    {
     
        return Input.GetAxis("Horizontal");
    }

    protected override float GetMotorInput()
    {
        
        return Mathf.Clamp01(Input.GetAxis("Vertical"));
    }

    protected override float GetBrakeInput()
    {
       
        float s_key_brake = Mathf.Abs(Mathf.Min(0, Input.GetAxis("Vertical")));

        return Mathf.Max(s_key_brake, spacebar_brake);
    }
}