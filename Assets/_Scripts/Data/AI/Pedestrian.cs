// Pedestrian.cs
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pedestrian : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // ... Логіка для ходіння по тротуарах ...
    }

    void Update()
    {
        // Оновлення анімації на основі швидкості
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}