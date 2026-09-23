using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Level1Text;
    [SerializeField] private TextMeshProUGUI Level2Text;
    [SerializeField] private TextMeshProUGUI Level3Text;
    [SerializeField] private TextMeshProUGUI Level4Text;
    [SerializeField] private TextMeshProUGUI Level5Text;

    private const int LevelCount = 5;

    private void Start()
    {
        TextMeshProUGUI[] levelTexts = { Level1Text, Level2Text, Level3Text, Level4Text, Level5Text };

        for (int i = 0; i < LevelCount; i++)
        {
            string sceneName = "Level" + (i + 1);
            string lastKey = sceneName + "LastRunTime";
            string bestKey = sceneName + "BestTime";

            string lastTime = PlayerPrefs.HasKey(lastKey)
                ? FormatTime(PlayerPrefs.GetFloat(lastKey))
                : "--:--";

            string bestTime = PlayerPrefs.HasKey(bestKey)
                ? FormatTime(PlayerPrefs.GetFloat(bestKey))
                : "--:--";

            if (levelTexts[i] != null)
            {
                levelTexts[i].text = $"{sceneName}   Last: {lastTime}   Best: {bestTime}";
            }
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int hundredths = Mathf.FloorToInt((timeInSeconds * 100f) % 100f);
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}