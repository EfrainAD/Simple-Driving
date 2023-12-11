using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    // Game Settings
    [SerializeField] private int maxEnergy;
    [SerializeField] private double energyRechargeDuration;
    
    // Game Objects
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text playButtonText;
    [SerializeField] private UnityEngine.UI.Button playButton;
    
    // Game Notifications Game Objects
    [SerializeField] private AndriodNotificationHandler andriodNotificationHandler;
    [SerializeField] private IosNotificationHandler iosNotificationHandler;

    // Keys for PlayerPrefs
    private const string HighScoreKey = ScoreHandler.highScoreKey;
    private const string SceneGameKey = "Scene_Game"; 
    private const string EnergyKey = "Energy";
    private const string EnergyRechargeTimeKey = "EnergyRechargeTime";

    private void Start() {
        displayHighScore();

        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        
        updatePlayButtonText(currentEnergy);

        if (currentEnergy == 0)
        {
            if (isEnergyRecharged())
            {
                resetMaxEnergy();
            }
            else 
            {
                playButton.interactable = false;
                Invoke(nameof(resetMaxEnergy), 5);
                // Invoke(nameof(resetMaxEnergy), (float)energyRechargeDuration * 60);
            }
        }
    }
    public void Play() {
        int currentEnergy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (currentEnergy > 0)
        {
            currentEnergy--;
            PlayerPrefs.SetInt(EnergyKey, currentEnergy);

            if (currentEnergy == 0)
            {
                DateTime nextRechargeTime = setEnergyRechargeTime();
                sendRechargeNotification(nextRechargeTime);
            }
            
            SceneManager.LoadScene(SceneGameKey);
        }
    }

    private void resetMaxEnergy()
    {
        PlayerPrefs.SetInt(EnergyKey, maxEnergy);
        playButton.interactable = true;
        updatePlayButtonText(maxEnergy);
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
    
    private DateTime setEnergyRechargeTime()
    {
        DateTime nextRechargeTime = DateTime.Now.AddMinutes(energyRechargeDuration);

        PlayerPrefs.SetString(EnergyRechargeTimeKey, nextRechargeTime.ToString());

        return nextRechargeTime;
    }
    private bool isEnergyRecharged()
    {
        string energyRechargeTime = PlayerPrefs.GetString(EnergyRechargeTimeKey, String.Empty);
            
        if (energyRechargeTime == String.Empty){return false;}

        DateTime rechangeTime = DateTime.Parse(energyRechargeTime);

        return DateTime.Now > rechangeTime;
    }
    private void sendRechargeNotification(DateTime nextRechargeTime)
    {
#if UNITY_ANDROID
        andriodNotificationHandler.ScheduleNotification(nextRechargeTime);
#elif UNITY_IOS
        iosNotificationHandler.ScheduleNotification((int)energyRechargeDuration);
#endif
    }
}
