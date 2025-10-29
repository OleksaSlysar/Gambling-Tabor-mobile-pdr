// _Scripts/Checkpoint.cs
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    private void Awake()
    {
        
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetNewCheckpoint(transform);
            
           
        }
    }
}
