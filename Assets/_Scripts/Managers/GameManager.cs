using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameStatus {
    InGame,
    Ended,
}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("References")]
    public TextMeshProUGUI triesValue;
    public TextMeshProUGUI recordValue;
    public TextMeshProUGUI timeValue;

    public int Tries { get; private set; } = 0;

    private GameStatus status;
    private float elapsedTime = 0f;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;

        string recordName = string.Format("high_{0:d}", GameSettings.numberOfRow);
        int currRecord = PlayerPrefs.GetInt(recordName, -1);
        if (currRecord < 0) {
            recordValue.text = "-";
        } else { 
            recordValue.text = currRecord.ToString();
        }

        status = GameStatus.InGame;
    }

    private void Start() {
        AudioManager.instance.PlayMusic("main_theme");
    }

    private void Update() {
        if (status == GameStatus.InGame) {
            UpdateTime();
        }
    }

    private void UpdateTime () {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timeValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreaseTries () {
        Tries++;
        triesValue.text = string.Format("{0:d}", Tries);
    }

    public void EndGame () {
        status = GameStatus.Ended;

        string recordName = string.Format("high_{0:d}", GameSettings.numberOfRow);
        int currRecord = PlayerPrefs.GetInt(recordName, -1);

        if (currRecord < 0 || Tries < currRecord) {
            string timeRecordName = "time_" + recordName;
            PlayerPrefs.SetInt(recordName, Tries);
            PlayerPrefs.SetFloat(timeRecordName, elapsedTime);
            SceneManager.LoadScene("EndRecordScene");
        } else {
            string lastScoreName = string.Format("last_{0:d}", GameSettings.numberOfRow);
            string lastTimeName = "time_" + lastScoreName;
            PlayerPrefs.SetInt(lastScoreName, Tries);
            PlayerPrefs.SetFloat(lastTimeName, elapsedTime);
            SceneManager.LoadScene("EndScene");
        }
    }
}
