using TMPro;
using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        string sceneName = "Level0";
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
