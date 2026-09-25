using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float elapsedTime = 0f;
    private GameObject timerGameObject;


    private void Awake()
    {
        timerGameObject = GameObject.Find("Timer");

        if (timerGameObject == null)
        {
            Debug.LogError("[GameManager] Timer object not found in the scene.");
            return;
        }
    }


    private void Update()
    {
        if (timerGameObject == null) return;

        elapsedTime += Time.deltaTime;
        timerGameObject.GetComponent<TextMeshProUGUI>().text = elapsedTime.ToString("F1") + " s";
 
    }

    private void OnEnable()
    {
        Trophy.OnTrophyReached += HandleTrophyReached;
    }

    private void OnDisable()
    {
        Trophy.OnTrophyReached -= HandleTrophyReached;
    }

    private void HandleTrophyReached()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetFloat(sceneName + "LastRunTime", elapsedTime);

        if (!PlayerPrefs.HasKey(sceneName + "BestTime") || elapsedTime < PlayerPrefs.GetFloat(sceneName + "BestTime"))
            PlayerPrefs.SetFloat(sceneName + "BestTime", elapsedTime);

        string nextScene = "Level" + (int.Parse(sceneName.Replace("Level", "")) + 1);

        SceneManager.LoadScene(Application.CanStreamedLevelBeLoaded(nextScene) ? nextScene : "Scoreboard");
    }
}