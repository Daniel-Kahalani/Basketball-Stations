using UnityEngine;
using System.Collections;
using System;

public class ShootBall : MonoBehaviour
{
    [SerializeField] private Transform prefab_ball;

    [SerializeField] private GameObject playerCamera;

    [SerializeField] private float ballDistanceFromCamera;

    [SerializeField] private float normalBallForceFactor;

    [SerializeField] private float hardBallForceFactor;

    [SerializeField] Vector3 offset;

    [SerializeField] [Range(10f, 80f)] private float shotAngle = 45f;

    private float ballForceFactor;
    private bool holdingBall = false;
    private bool readyToShoot = false;
    private Transform ball = null;
    private Rigidbody ballRB = null;

    private void Start()
    {
        EventManager._instance.OnNewGame += () => setBallFactor();
        EventManager._instance.OnScore += () => setReadyToShoot(false);
        EventManager._instance.OnPlayerInPositon += () => setReadyToShoot(true);
        EventManager._instance.OnFinishGame += () => setReadyToShoot(false);
    }

    private void setBallFactor()
    {
        ballForceFactor = PlayerPrefs.GetString("gameDifficulty").Equals("NORMAL") ? normalBallForceFactor : hardBallForceFactor;
    }

    private void Update()
    {
        if (readyToShoot)
        {
            if (Input.anyKey && !holdingBall)
            {
                createBall();
            }
            if (!Input.anyKey && holdingBall)
            {
                throwBall();
            }
        }
    }

    private void setReadyToShoot(bool newValue)
    {
        readyToShoot = newValue;
        holdingBall = false;
        if (ball != null)
        {
            Destroy(ball.gameObject);
            ball = null;
            ballRB = null;
        }
    }

    private void createBall()
    {
        ball = Instantiate(prefab_ball, playerCamera.transform.position + playerCamera.transform.forward * ballDistanceFromCamera, Quaternion.identity);
        ballRB = ball.GetComponent<Rigidbody>();
        holdingBall = true;
    }

    private void throwBall()
    {
        if (ball != null)
        {   
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                shootBall(hit.point);
            }
            ball = null;
            ballRB = null;
            StartCoroutine(delayBetweenShots(1f));
        }
    }

    private IEnumerator delayBetweenShots(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        holdingBall = false;
    }

    private void shootBall(Vector3 point)
    {
        Vector3 velocityDirection = ballVelocity(point, shotAngle);
        if (ballRB != null)
        {
            ballRB.velocity = velocityDirection * ballForceFactor;
            ballRB.useGravity = true;
            ball.parent = null;
        }
    }

    private Vector3 ballVelocity(Vector3 destination, float shotAngle)
    {
        Vector3 targetDirection = destination - transform.position;
        float yDifference = targetDirection.y;
        targetDirection.y = 0;
        float dist = targetDirection.magnitude;
        float angelInRadians = shotAngle * Mathf.Deg2Rad;
        targetDirection.y = dist * Mathf.Tan(angelInRadians);
        dist += yDifference / Mathf.Tan(angelInRadians);
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * angelInRadians));
        return velocity * targetDirection.normalized;
    }
}

