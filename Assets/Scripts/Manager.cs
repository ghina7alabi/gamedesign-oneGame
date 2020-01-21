using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI heartCountText;

    bool loaded = false;

    private void Update()
    {
        //Managing high score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
            highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore");
        }

        if (PlayerPrefs.GetInt("HighScore") < PlayerController.score)
        {
            PlayerPrefs.SetInt("HighScore", PlayerController.score);
            highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore");
            PlayerPrefs.Save();
        }

        scoreText.text = "" + PlayerController.score;
        heartCountText.text = "" + PlayerController.heartCount;

        if (PlayerController.heartCount <= 0 && !loaded)
        {
            SceneManager.LoadScene("GameOverScene");
            PlayerController.heartCount = 3;
            loaded = true;
        }


    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
   
    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickHowTo()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

}
