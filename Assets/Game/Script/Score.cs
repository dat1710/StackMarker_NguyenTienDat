using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]private Text scoreText;
    public void UpdateScore(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
