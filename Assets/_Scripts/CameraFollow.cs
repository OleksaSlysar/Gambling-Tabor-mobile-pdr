using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Ціль")]
    public Transform target; 

    [Header("Налаштування")]
    [Tooltip("Наскільки камера позаду та зверху")]
    public Vector3 offset = new Vector3(0f, 5f, -7f); 

    [Tooltip("Наскільки плавно камера 'наздоганяє' машину")]
    public float smoothSpeed = 5f;


    void LateUpdate()
    {
        if (target == null)
            return;

      
        Vector3 desiredPosition = target.position + offset;

      
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        transform.position = smoothedPosition;

        transform.LookAt(target.position);
    }
}
