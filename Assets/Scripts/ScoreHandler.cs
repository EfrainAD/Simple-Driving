using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    private float score;

    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
