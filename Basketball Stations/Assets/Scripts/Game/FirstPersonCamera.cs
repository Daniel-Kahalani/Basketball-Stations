using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float normalDistance = 1.3f;

    [SerializeField] private float hardDistance = 1.8f;

    [SerializeField] private float normalHeight = 0.7f;

    [SerializeField] private float hardHeight = 0.7f;

    [SerializeField] private Transform station;

    [SerializeField] private Transform net;

    [SerializeField] private Transform gameOverWindow;

    [SerializeField] private Transform highestScoresWindow;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float movmentLerpTime;
    
    [SerializeField] private float rotationLerpTime;

    [SerializeField] private float canvasLerpTime;

    private bool isMoveTowardsStaion = false;
    private bool isRotateTowardsTarget = false;
    private float movmentPrecent = 0;
    private float rotationPrecent = 0;
    private Vector3 startPos;
    private float lerpTime;
    private Transform rotationTarget;
    private Quaternion startRotation;
    private float distance;
    private float height;

    private void Start()
    {
        EventManager._instance.OnNewGame += () => setMoveTowardsStation(true);
        EventManager._instance.OnScore += () => setRotateTowardsTarget(true, station, rotationLerpTime);
        EventManager._instance.OnStationSelected += () => setMoveTowardsStation(true);
        EventManager._instance.OnPlayerInPositon += () => setMoveTowardsStation(false);
        EventManager._instance.OnFinishGame += () =>
        {
            setMoveTowardsStation(false);
            setWindowsPosition();
        };
    }

    private void setWindowsPosition()
    {
        distance = PlayerPrefs.GetString("gameDifficulty").Equals("NORMAL") ? normalDistance : hardDistance;
        height = PlayerPrefs.GetString("gameDifficulty").Equals("NORMAL") ? normalHeight : hardHeight;
        gameOverWindow.position = transform.position + ((net.position - transform.position) / distance);
        highestScoresWindow.position = transform.position + ((net.position - transform.position) / distance);
        Vector3 cameraPos = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        Vector3 direction = gameOverWindow.position - cameraPos;
        Quaternion rotation = Quaternion.LookRotation(direction);
        gameOverWindow.rotation = rotation;
        highestScoresWindow.rotation = rotation;
        setRotateTowardsTarget(true, gameOverWindow, canvasLerpTime);
    }

    private void moveFirstPersonCammera()
    {
        movmentPrecent += Time.deltaTime / lerpTime;
        Vector3 desiredPosition = station.transform.position + offset;
        transform.position = Vector3.Lerp(startPos, desiredPosition, movmentPrecent);
        if (movmentPrecent >= 1)
        {
            EventManager._instance.PlayerInPosition();
        }
    }

    private void setMoveTowardsStation(bool newValue)
    {
        isMoveTowardsStaion = newValue;
        if (isMoveTowardsStaion)
        {
            startPos = transform.position;
            movmentPrecent = 0;
            setRotateTowardsTarget(newValue, net, movmentLerpTime);
        }
    }

    private void Update()
    {
        if (isMoveTowardsStaion)
        {
            moveFirstPersonCammera();
        }
        if (isRotateTowardsTarget)
        {
            rotateFirstPersonCamera();
        }
    }

    private void rotateFirstPersonCamera()
    {
        rotationPrecent += Time.deltaTime / lerpTime;
        Vector3 relativePos = rotationTarget.position - Camera.main.transform.position;
        Quaternion targetRot = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(startRotation, targetRot, rotationPrecent);
        if (rotationPrecent >= 1)
        {
            isRotateTowardsTarget = false;
        }
    }

    private void setRotateTowardsTarget(bool newValue, Transform target, float lerpTime)
    {
        isRotateTowardsTarget = newValue;
        if (isRotateTowardsTarget)
        {
            this.lerpTime = lerpTime;
            this.rotationTarget = target;
            startRotation = transform.rotation;
            rotationPrecent = 0;
        }
    }
}
