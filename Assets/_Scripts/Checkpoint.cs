using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        // Рекомендую додати цьому об'єкту тег "Checkpoint",
        // щоб машина в нього не "врізалася"
        this.gameObject.tag = "Checkpoint"; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetNewCheckpoint(transform);
        }
    }

    // Візуальна стрілка у редакторі
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);
        Gizmos.DrawWireSphere(transform.position + transform.forward * 5f, 0.5f);
    }
}