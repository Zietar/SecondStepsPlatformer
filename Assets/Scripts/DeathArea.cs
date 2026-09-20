using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private Transform playerTransform;
    private Transform respawnPointTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        respawnPointTransform = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform.position = respawnPointTransform.position;
        }
    }
}
