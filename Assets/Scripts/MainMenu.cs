using System.Linq;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GameObject canvas = GameObject.Find("Canvas");
        GameObject itchQuit = Resources.FindObjectsOfTypeAll<GameObject>()
            .FirstOrDefault(g => g.name == "ItchQuit" && g.scene.isLoaded);

        if (canvas != null) canvas.SetActive(false);
        if (itchQuit != null) itchQuit.SetActive(true);
#elif UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}