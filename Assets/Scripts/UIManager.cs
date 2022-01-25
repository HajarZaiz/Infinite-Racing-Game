using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text scoreText;
    private float score = 0;
    private int scoreIncrement = 10;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Your Score: " + score.ToString("0");
    }

    void Update()
    {
        scoreText.text = "Your Score: " + score.ToString("0");
        score += scoreIncrement * Time.deltaTime;
    }

   public void ScoreBoost()
    {
        score = score + 100;
        //scoreText.text = "Your Score: " + score.ToString() + " POINTS";
    }
}
