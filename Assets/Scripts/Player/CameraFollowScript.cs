using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private GameObject target;  
    [SerializeField] private float smoothSpeed = 0.125f; 
    [SerializeField] private Vector2 offset; 

    private void FixedUpdate()
    {
        Vector3 desiredPosition =new Vector3( target.transform.position.x + offset.x,target.transform.position.y + offset.y,transform.position.z);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
