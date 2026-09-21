using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0f, 1f, -1f);

    private float targetSmoothSpeed = 0.012f;
    private float currentSmoothSpeed = 0f;
    private float accelerationDuration = 3f;
    private float elapsedTime = 0f;

    private Transform trophyTransform;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;

        trophyTransform = GameObject.Find("Trophy").transform;
        if (trophyTransform != null)
        {
            transform.position = trophyTransform.position + offset;
        }
        
    }

    private void LateUpdate()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / accelerationDuration);

        currentSmoothSpeed = Mathf.Lerp(0f, targetSmoothSpeed, t);

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
        transform.position = smoothedPosition;
    }
}