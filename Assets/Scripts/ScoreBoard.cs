using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score;

    void Update()
    {
        scoreText.text = score.ToString("000000");
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
    }

}
