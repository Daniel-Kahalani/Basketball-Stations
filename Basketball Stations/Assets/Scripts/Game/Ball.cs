using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void Start()
    {
        transform.parent = Camera.main.transform;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        transform.Rotate(0, transform.parent.rotation.eulerAngles.y - 180, 0);
        EventManager._instance.OnFinishGame += () =>
        {
            if (this != null)
            {
                Destroy(gameObject);
            }
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
        {
            Destroy(gameObject, 2.5f);
        }
        if (collision.transform.name.Equals("Floor"))
        {
            EventManager._instance.HitFloor();
        }
        else if (collision.transform.name.Equals("Frame"))
        {
            EventManager._instance.HitBoard();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Score"))
        {
            Destroy(gameObject, 0.2f);
            EventManager._instance.Score();
        }
    }
}
