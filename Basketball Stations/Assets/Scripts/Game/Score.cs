using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject scoreEffect;

    private void Start()
    {
        EventManager._instance.OnNewGame += () => gameObject.GetComponent<SphereCollider>().enabled = true;
        EventManager._instance.OnScore += () =>
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            scoreEffect.SetActive(true);
            scoreEffect.GetComponent<ParticleSystem>().Play();
        };
        EventManager._instance.OnPlayerInPositon += () => gameObject.GetComponent<SphereCollider>().enabled = true;
        EventManager._instance.OnFinishGame += () => gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
