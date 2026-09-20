using TMPro;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        string sceneName = "Level1";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void BackButton()
    {
        string sceneName = "MainMenu";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void QuitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
