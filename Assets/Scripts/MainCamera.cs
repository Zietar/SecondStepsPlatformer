using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject playerGameObject;
    private Vector3 offset = new Vector3(0f, 1f, -1f);


    private GameObject trophyGameObject;
    private float targetSmoothSpeed = 0.012f;
    private float currentSmoothSpeed = 0f;
    private float accelerationDuration = 3f;
    private float elapsedTime = 0f;

    private void Awake()
    {
        playerGameObject = GameObject.Find("Player");
        trophyGameObject = GameObject.Find("Trophy");

        if (playerGameObject == null)
        {
            Debug.LogError("[MainCamera] Player object not found in the scene.");
        }

        if (trophyGameObject == null)
        {
            Debug.LogError("[MainCamera] Trophy object not found in the scene.");
        }
        else
        {
            transform.position = trophyGameObject.transform.position + offset;
        }
    }

    private void LateUpdate()
    {
        if (playerGameObject == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / accelerationDuration);

        currentSmoothSpeed = Mathf.Lerp(0f, targetSmoothSpeed, t);

        Vector3 desiredPosition = playerGameObject.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, currentSmoothSpeed);
        transform.position = smoothedPosition;
    }
}