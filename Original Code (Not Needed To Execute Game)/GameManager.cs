using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public Text scoreText;

    public int highscore;
    public Text highscoreText;

    public int bonusSpacing = 100;
    public int scoreBefore = 0;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelScoreText;
    public Text gameOverPanelHighscoreText;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        GetHighscore();
    }

    private void GetHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore.ToString();
    }

    public bool checkForBonus()
    {
        if (score > 0 && score - scoreBefore >= bonusSpacing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void updateScoreBefore()
    {
        scoreBefore = score;
        Debug.Log("Update score Before: " + scoreBefore.ToString());
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }
    }

    public void OnBombHit()
    {
        // Debug.Log("Bomb Hit");
        gameOverPanel.SetActive(true);
        gameOverPanelScoreText.text = "Score: " + score.ToString();
        GetHighscore();
        gameOverPanelHighscoreText.text = "Best: " + highscore.ToString();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        gameOverPanel.SetActive(false);
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(obj);
        }

        Time.timeScale = 1;
    }

}
