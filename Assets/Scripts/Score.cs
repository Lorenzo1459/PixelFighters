using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int scoreInt = 0;
    public TMPro.TMP_Text scoreText;

    public void addScore(int points){
        scoreInt+= points;
        scoreText.SetText(scoreInt.ToString());
    }
}
