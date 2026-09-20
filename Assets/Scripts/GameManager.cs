using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float ElapsedTime = 0f;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        timerText.text = ElapsedTime.ToString("F1") + " s";
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

        PlayerPrefs.SetFloat(sceneName + "LastRunTime", ElapsedTime);

        if (!PlayerPrefs.HasKey(sceneName + "BestTime") || ElapsedTime < PlayerPrefs.GetFloat(sceneName + "BestTime"))
            PlayerPrefs.SetFloat(sceneName + "BestTime", ElapsedTime);

        string nextScene = "Level" + (int.Parse(sceneName.Replace("Level", "")) + 1);

        SceneManager.LoadScene(Application.CanStreamedLevelBeLoaded(nextScene) ? nextScene : "Scoreboard");
    }
}