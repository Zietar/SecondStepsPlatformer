using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{

    private void Awake()
    {
        SetupButton("PlayButton", PlayButton);
        SetupButton("RestartButton", RestartButton);
        SetupButton("BackButton", BackButton);
        SetupButton("QuitButton", QuitButton);
        SetupButton("ScoreboardButton", ScoreboardButton);
    }

    private void SetupButton(string buttonName, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonGameObject = GameObject.Find(buttonName);
        if (buttonGameObject == null)
        {
            Debug.LogError($"[UIManager] GameObject with name \"{buttonName}\" not found in the scene.");
            return;
        }

        buttonGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        buttonGameObject.GetComponent<Button>().onClick.AddListener(action);
    }


    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
    public void RestartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    public void ScoreboardButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scoreboard");
    }

}