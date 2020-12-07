using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text countdownText;

    [SerializeField] private Vector3[] shotPositions;

    [SerializeField] private Light shotSpotLight;

    [SerializeField] private TextMeshProUGUI scoreLabel;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI timerLabel;

    [SerializeField] private TextMeshProUGUI timeLeftText;

    [SerializeField] private GameObject gameOverWindow;

    [SerializeField] private GameObject highestScoresWindow;

    [SerializeField] private TextMeshProUGUI[] highestScoresText;

    [SerializeField] private int timeLeft = 15;

    [SerializeField] private int countdownTime = 3;

    private int score;
    private bool startGame = true;


    private void Start()
    {
        EventManager._instance.OnNewGame += startNewGame;
        EventManager._instance.OnScore += updateScore;
        EventManager._instance.OnFinishGame += gameFinished;     
    }

    private void Update()
    {
        if (startGame)
        {
            EventManager._instance.NewGame();
            startGame = false;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("gameDifficulty", "NORMAL");
    }

    private void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void startNewGame()
    {
        score = 0;
        highestScoresWindow.SetActive(false);
        gameOverWindow.SetActive(false);
        timeLeftText.text = timeLeft.ToString();
        scoreText.text = score.ToString();
        StartCoroutine(countdownToStart(countdownTime));
    }

    private IEnumerator countdownToStart(int countdownSeconds)
    {
        countdownText.gameObject.SetActive(true);
        while (countdownSeconds > 0)
        {
            countdownText.text = countdownSeconds.ToString();
            yield return new WaitForSeconds(1f);
            countdownSeconds--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        startGameTimer();
    }

    private void startGameTimer()
    {
        scoreLabel.gameObject.SetActive(true);
        timerLabel.gameObject.SetActive(true);
        timeLeftText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        StartCoroutine(timeLeftToGame(timeLeft));
        EventManager._instance.StartGame();
    }

    private IEnumerator timeLeftToGame(int secondsLeftToGame)
    {
        timeLeftText.text = secondsLeftToGame.ToString();
        yield return new WaitForSeconds(1f);
        if (secondsLeftToGame > 0)
        {
            secondsLeftToGame--;
            StartCoroutine(timeLeftToGame(secondsLeftToGame));
        }
        else
        {
            EventManager._instance.GameFinish();
        }
    }

    private void gameFinished()
    {
        scoreLabel.gameObject.SetActive(false);
        timerLabel.gameObject.SetActive(false);
        timeLeftText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        highestScoresWindow.SetActive(false);
        gameOverWindow.SetActive(true);
        FindObjectOfType<AudioManager>().Play("WindowPopup");
    }

    public int GetScore()
    {
        return score;
    }

    public void OnRestartButtonClick()
    {
        EventManager._instance.NewGame();
    }

    public void OnHighestScoresButtonClick()
    {
        int[] highestScores = FindObjectOfType<HighestScoresManager>().GetHighestScores();
        for (int i = 0; i < highestScores.Length; i++)
        {
            highestScoresText[i].text = PlayerPrefs.GetInt("HighScore" + i).ToString();
        }
        gameOverWindow.SetActive(false);
        highestScoresWindow.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        EventManager._instance.BackToMainMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnBackButton()
    {
        highestScoresWindow.SetActive(false);
        gameOverWindow.SetActive(true);
    }
}
