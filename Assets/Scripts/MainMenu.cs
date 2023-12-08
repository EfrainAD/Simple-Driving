using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    private string highScoreKey = ScoreHandler.highScoreKey;
    private void Start() {
        int highScore = PlayerPrefs.GetInt(highScoreKey);

        highScoreText.text = $"High Score: {highScore}";
    }
    public void Play() {
        SceneManager.LoadScene("Scene_Game");
    }
}
