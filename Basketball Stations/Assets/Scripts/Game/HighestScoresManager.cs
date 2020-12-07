using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestScoresManager : MonoBehaviour
{
    private const int maxNumOfScores = 10;
    private int[] highestScores = new int[maxNumOfScores];

    private void Start()
    {
        EventManager._instance.OnFinishGame += addNewScore;
    }

    private void addNewScore()
    {
        int oldScore;
        int newScore = FindObjectOfType<GameManager>().GetScore();
        for (int i = 0; i < maxNumOfScores; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                if (PlayerPrefs.GetInt("HighScore" + i) < newScore)
                {
                    oldScore = PlayerPrefs.GetInt("HighScore" + i);
                    PlayerPrefs.SetInt("HighScore" + i, newScore);
                    newScore = oldScore;
                }
            }
            else
            {
                PlayerPrefs.SetInt("HighScore" + i, newScore);
                newScore = 0;
            }
        }
        updateHighestScoresArray();
    }

    private void updateHighestScoresArray()
    {
        for (int i = 0; i < maxNumOfScores; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                highestScores[i] = PlayerPrefs.GetInt("HighScore" + i);
            }
        }
    }

    public int[] GetHighestScores()
    {
        return highestScores;
    }
}
