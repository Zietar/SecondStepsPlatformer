using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 1f, -1f);
    private float smoothSpeed = 0.012f;
    private Transform target;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}