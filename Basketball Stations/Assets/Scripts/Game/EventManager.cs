using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager _instance;

    public delegate void NewGameAction();
    public event NewGameAction OnNewGame;

    public delegate void StartGameAction();
    public event StartGameAction OnStartGame;

    public delegate void HitBoardAction();
    public event HitBoardAction OnHitBoardAction;

    public delegate void HitFloorAction();
    public event HitFloorAction OnHitFloorAction;

    public delegate void ScoreAction();
    public event ScoreAction OnScore;

    public delegate void StationAction();
    public event StationAction OnStationSelected;

    public delegate void PlayerInPositionAction();
    public event PlayerInPositionAction OnPlayerInPositon;

    public delegate void FinishGameAction();
    public event FinishGameAction OnFinishGame;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void NewGame()
    {
        OnNewGame();
    }

    public void StartGame()
    {
        OnStartGame();
    }

    public void Score()
    {
        OnScore();
    }

    public void HitBoard()
    {
        OnHitBoardAction();
    }

    public void HitFloor()
    {
        OnHitFloorAction();
    }

    public void StationSelected()
    {
        OnStationSelected();
    }

    public void GameFinish()
    {
        OnFinishGame();
    }

    public void PlayerInPosition()
    {
        OnPlayerInPositon();
    }

    public void BackToMainMenu()
    {
        OnNewGame = null;
        OnStartGame = null;
        OnHitBoardAction = null;
        OnHitFloorAction = null;
        OnScore = null;
        OnStationSelected = null;
        OnPlayerInPositon = null;
        OnFinishGame = null;
    }
}
