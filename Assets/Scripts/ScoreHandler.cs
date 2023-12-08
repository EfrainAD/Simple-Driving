using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    private float score;
    public const string highScoreKey = "HighScore";

    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
    private void OnDestroy() {
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Mathf.FloorToInt(score));
        }
    }
}
