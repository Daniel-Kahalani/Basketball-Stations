using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Station : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private Material hoverdMaterial;

    List<Vector3> stationsPositions;
    Vector3 middleMid = new Vector3(0, 0, -6.4f);
    Vector3 leftMid = new Vector3(4.8f, 0, -11f);
    Vector3 rightMid = new Vector3(-4.8f, 0, -11f);
    Vector3 middle2Mid = new Vector3(0, 0, -8f);

    Vector3 leftThree = new Vector3(6f, 0, -10f);
    Vector3 middle1Three = new Vector3(3, 0, -7f);
    Vector3 middle2Three = new Vector3(-3, 0, -7f);
    Vector3 rightThree = new Vector3(-6f, 0, -10f);

    private int currentLocation;

    void Start()
    {
        EventManager._instance.OnNewGame += () =>
        {
            initPositions(PlayerPrefs.GetString("gameDifficulty"));
            currentLocation = -1;
            moveStation();
            gameObject.SetActive(false);
        };
        EventManager._instance.OnScore += moveStation;
        EventManager._instance.OnStationSelected += () => gameObject.SetActive(false);
        EventManager._instance.OnFinishGame += () => gameObject.SetActive(false);
    }

    public void initPositions(string difficulty)
    {
        stationsPositions = new List<Vector3>();
        if (difficulty.Equals("NORMAL"))
        {
            inintToNormalStationPos();
        }
        else if (difficulty.Equals("HARD"))
        {
            inintToHardStationPos();
        }
    }

    private void inintToNormalStationPos()
    {
        transform.position = leftMid;
        stationsPositions.Add(leftMid);
        stationsPositions.Add(middleMid);
        stationsPositions.Add(rightMid);
        stationsPositions.Add(middle2Mid);
    }

    private void inintToHardStationPos()
    {
        transform.position = leftThree;
        stationsPositions.Add(leftThree);
        stationsPositions.Add(middle1Three);
        stationsPositions.Add(middle2Three);
        stationsPositions.Add(rightThree);
    }

    private void moveStation()
    {
        if (currentLocation == stationsPositions.Count - 1)
        {
            currentLocation = 0;
        }
        else
        {
            currentLocation++;
        }
        gameObject.SetActive(true);
        transform.position = stationsPositions[currentLocation];
    }
}
