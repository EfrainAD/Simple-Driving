using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Game Settings
    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRechargeDuration;
    
    // Game Objects
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text playButtonText;

    // Keys for PlayerPrefs
    private const string HighScoreKey = ScoreHandler.highScoreKey;
    private const string SceneGameKey = "Scene_Game"; 
    private const string EnergyKey = "Energy";
    private const string EnergyRechargeTimeKey = "EnergyRechargeTime";

    private void Start() {
        displayHighScore();

        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        updatePlayButtonText(currentEnergy);
    }
    public void Play() {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt(EnergyKey, currentEnergy);

            if (currentEnergy == 0)
            {
                setEnergyRechargeTime();
            }
            
            SceneManager.LoadScene(SceneGameKey);
        }
        else if (isEnergyRecharged())
        {
            PlayerPrefs.SetInt(EnergyKey, maxEnergy-1); 
            SceneManager.LoadScene(SceneGameKey);
        }
    }

    private void displayHighScore()
    {
        int highScore = PlayerPrefs.GetInt(HighScoreKey);

        highScoreText.text = $"High Score: {highScore}";
    }
    private void updatePlayButtonText(int energy)
    {
        playButtonText.text = $"Play ({energy})";
    }
    
    private void setEnergyRechargeTime()
    {
        DateTime nextRechargeTime = DateTime.Now.AddMinutes(energyRechargeDuration);

        PlayerPrefs.SetString(EnergyRechargeTimeKey, nextRechargeTime.ToString());
    }
    private bool isEnergyRecharged()
    {
        string energyRechargeTime = PlayerPrefs.GetString(EnergyRechargeTimeKey, String.Empty);
            
        if (energyRechargeTime == String.Empty){return false;}

        DateTime rechangeTime = DateTime.Parse(energyRechargeTime);

        return DateTime.Now > rechangeTime;
    }
}
