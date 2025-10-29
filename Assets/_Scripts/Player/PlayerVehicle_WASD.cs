// _Scripts/Player/PlayerVehicle_WASD.cs

using UnityEngine;

public class PlayerVehicle_WASD : Venicle 
{
  

    public override void Start()
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
        float spacebar_brake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        return Mathf.Max(s_key_brake, spacebar_brake);
    }
}