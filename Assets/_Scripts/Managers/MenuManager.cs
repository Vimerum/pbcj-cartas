using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start() {
        AudioManager.instance.PlayMusic("main_theme");
    }

    public void StartGame (int numberOfRows) {
        GameSettings.numberOfRow = numberOfRows;
        SceneManager.LoadScene("MainScene");
    }
}
