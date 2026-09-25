using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private GameObject playerGameObject;
    private GameObject respawnPointGameObject;

    private void Awake()
    {
        playerGameObject = GameObject.Find("Player");
        respawnPointGameObject = GameObject.Find("RespawnPoint");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerGameObject == null || respawnPointGameObject == null)
        {
            Debug.LogError("[DeathArea] Player or RespawnPoint object not found in the scene.");
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            playerGameObject.transform.position = respawnPointGameObject.transform.position;
        }
    }
}
