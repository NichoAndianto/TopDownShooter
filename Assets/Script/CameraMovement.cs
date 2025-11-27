using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Camera Settings")]
    public float smoothSpeed = 10f;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float collisionRadius = 0.3f;
    public float minDistance = 0.5f;  
    public float collisionSmooth = 10f;

    private Vector3 currentPosition;

    void LateUpdate()
    {
        if (player == null) return;

        
        Vector3 desiredPosition = player.position + offset;

       
        Vector3 direction = (desiredPosition - player.position).normalized;
        float desiredDistance = Vector3.Distance(player.position, desiredPosition);

        
        RaycastHit hit;
        if (Physics.SphereCast(player.position, collisionRadius, direction, out hit, desiredDistance))
        {
            
            float hitDistance = hit.distance - 0.1f;

            float clampedDistance = Mathf.Clamp(hitDistance, minDistance, desiredDistance);

            Vector3 collisionPosition = player.position + direction * clampedDistance;

            
            currentPosition = Vector3.Lerp(transform.position, collisionPosition, collisionSmooth * Time.deltaTime);
        }
        else
        {
            
            currentPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        transform.position = currentPosition;
    }

    void OnDrawGizmos()
    {
        
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, collisionRadius);
        }
    }
}
