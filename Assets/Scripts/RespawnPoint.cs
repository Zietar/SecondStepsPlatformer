using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private GameObject playerGameObject;

    private void Awake()
    {
        playerGameObject = GameObject.Find("Player");
        if (playerGameObject == null)
        {
            Debug.LogError("[RespawnPoint] Player object not found in the scene.");
        }
        else
        {
            transform.position = playerGameObject.transform.position;
        }
    }
}
