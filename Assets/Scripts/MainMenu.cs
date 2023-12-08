using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    private const string highScoreKey = ScoreHandler.highScoreKey;

    [SerializeField] private int maxEnergy;
    [SerializeField] private float energyRechargeDuration;
    
    [SerializeField] private TMP_Text playButton;

    

    private const string energyKey = "Energy";
    private const string energyRechargeTimeKey = "EnergyRechargeTime";

    private void Start() {
        int highScore = PlayerPrefs.GetInt(highScoreKey);

        highScoreText.text = $"High Score: {highScore}";

        int energy = PlayerPrefs.GetInt(energyKey, maxEnergy);

        playButton.text = $"{playButton.text} ({energy})";
    }
    public void Play() {
        int energy = PlayerPrefs.GetInt(energyKey, 3);

        if (energy > 0)
        {
            energy--;
            if (energy == 0)
            {
                DateTime newRecharge = DateTime.Now.AddMinutes(energyRechargeDuration);

                PlayerPrefs.SetString(energyRechargeTimeKey, newRecharge.ToString());
            }
            
            PlayerPrefs.SetInt(energyKey, energy);
            
            SceneManager.LoadScene("Scene_Game");
        }
        else
        {
            if (isEnergyRecharged())
            {
                PlayerPrefs.SetInt(energyKey, maxEnergy);
                playButton.text = $"Play! ({maxEnergy})";
            }
        }
    }
    private bool isEnergyRecharged()
    {
        string energyRechargeTime = PlayerPrefs.GetString(energyRechargeTimeKey, String.Empty);
            
            Debug.Log("energyRechargeTime: " + energyRechargeTime);

            if (energyRechargeTime == String.Empty){return false;}

            DateTime rechangeTime = DateTime.Parse(energyRechargeTime);

            if (DateTime.Now > rechangeTime)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}
