
using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
public class ArcadeCar : MonoBehaviour
{
    [Header("Керування")]
    public float moveSpeed = 3000f; 
    public float turnSpeed = 1000f; 

    [Header("Зчеплення з дорогою")]
    [Tooltip("Наскільки сильно машина 'чіпляється' за дорогу. 10 = приклеєна, 1 = дрифт")]
    public float gripFactor = 10f;

    private Rigidbody rb;
    private float motorInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
      
        var keyboard = Keyboard.current;
        if (keyboard == null) return; 

       
        if (keyboard.wKey.isPressed) motorInput = 1f;
        else if (keyboard.sKey.isPressed) motorInput = -1f;
        else motorInput = 0f;

      
        if (keyboard.dKey.isPressed) turnInput = 1f;
        else if (keyboard.aKey.isPressed) turnInput = -1f;
        else turnInput = 0f;
        Debug.Log("Turn Input: " + turnInput);
    }

    void FixedUpdate()
    {
     
        float effectiveTurnInput = turnInput;
        if (motorInput < 0)
        {
            effectiveTurnInput = -turnInput; 
        }

       
        rb.AddForce(transform.forward * motorInput * moveSpeed);

      
        rb.AddTorque(transform.up * effectiveTurnInput * turnSpeed);

    
        float lateralVelocity = Vector3.Dot(rb.linearVelocity, transform.right);

     
        Vector3 counterForce = -transform.right * lateralVelocity * gripFactor * rb.mass;
        rb.AddForce(counterForce);
    }
}