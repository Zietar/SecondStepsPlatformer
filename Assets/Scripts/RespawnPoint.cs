using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }
}
