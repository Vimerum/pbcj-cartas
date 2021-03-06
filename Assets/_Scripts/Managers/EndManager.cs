using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndManager : MonoBehaviour
{
    [Header("Settings")]
    public bool isCredits = false;
    public bool useRecord = false;
    [Header("References")]
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI timeValue;

    private void Start() {
        if (isCredits) {
            AudioManager.instance.PlayMusic("victory_theme");
            return;
        }

        string scoreName = string.Format("last_{0:d}", GameSettings.numberOfRow);
        if (useRecord) {
            scoreName = string.Format("high_{0:d}", GameSettings.numberOfRow);
            AudioManager.instance.PlayMusic("victory_theme");
        }
        string timeName = "time_" + scoreName;

        int currScore = PlayerPrefs.GetInt(scoreName, 0);
        float currTime = PlayerPrefs.GetFloat(timeName, 0f);

        int minutes = Mathf.FloorToInt(currTime / 60f);
        int seconds = Mathf.FloorToInt(currTime % 60f);

        scoreValue.text = currScore.ToString();
        timeValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Retry () {
        SceneManager.LoadScene("MainScene");
    }

    public void Credits() {
        SceneManager.LoadScene("CreditsScene");
    }

    public void Quit () {
 #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
 #else
         Application.Quit();
 #endif
    }
}
