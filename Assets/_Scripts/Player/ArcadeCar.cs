
using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCar : MonoBehaviour
{
    [Header("Керування")]
    public float moveSpeed = 3000f; 
    public float turnSpeed = 1000f; 

    private Rigidbody rb;
    private float motorInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      
        var keyboard = Keyboard.current;
        if (keyboard == null) return; 

        if (keyboard.wKey.isPressed)
            motorInput = 1f;
        else if (keyboard.sKey.isPressed)
            motorInput = -1f;
        else
            motorInput = 0f;

        if (keyboard.dKey.isPressed)
            turnInput = 1f;
        else if (keyboard.aKey.isPressed)
            turnInput = -1f;
        else
            turnInput = 0f;
    }

    void FixedUpdate()
    {
       rb.AddForce(transform.forward * motorInput * moveSpeed);
    rb.AddTorque(transform.up * turnInput * turnSpeed);
    }
}