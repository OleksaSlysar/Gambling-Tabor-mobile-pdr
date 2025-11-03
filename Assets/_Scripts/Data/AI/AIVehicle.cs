// _Scripts/AI/AIVehicle.cs
using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))]
public class AIVehicle : Vehicle 
{
    private NavMeshAgent agent;
    
    [Header("AI Налаштування")]
    public Transform target; 

  
    private float steeringInput;
    private float motorInput;
    private float brakeInput;

    public override void Start()
    {
        base.Start(); 
        agent = GetComponent<NavMeshAgent>();

        
        agent.Warp(transform.position); 
    }

    void Update()
    {
     
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        
        if (Vector3.Distance(transform.position, agent.nextPosition) > agent.height)
        {
            agent.Warp(transform.position);
        }


        if (agent.hasPath)
        {
            
            Vector3 desiredVelocity = agent.desiredVelocity;

            Vector3 localDesiredVelocity = transform.InverseTransformDirection(desiredVelocity.normalized);

           
            steeringInput = Mathf.Clamp(localDesiredVelocity.x, -1f, 1f);

       
            motorInput = Mathf.Clamp01(localDesiredVelocity.z);

       
            if (agent.remainingDistance < agent.stoppingDistance && agent.desiredVelocity.magnitude < 0.5f)
            {
                motorInput = 0;
                brakeInput = 1;
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {

            motorInput = 0;
            steeringInput = 0;
            brakeInput = 1;
        }
    }

   
    protected override float GetMotorInput()
    {
        return motorInput;
    }

    protected override float GetSteeringInput()
    {
        return steeringInput;
    }

    protected override float GetBrakeInput()
    {
        return brakeInput;
    }
}