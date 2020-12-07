using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStation : MonoBehaviour
{
    private bool isPossibleToSelectStation = false;

    void Start()
    {
        EventManager._instance.OnNewGame += () => isPossibleToSelectStation = false;
        EventManager._instance.OnScore += () => isPossibleToSelectStation = true;
        EventManager._instance.OnStationSelected += () => isPossibleToSelectStation = false;
        EventManager._instance.OnFinishGame += () => isPossibleToSelectStation = false;
    }

    void Update()
    {
        if((isPossibleToSelectStation)&&(Input.anyKeyDown))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if(hit.transform.name.Equals("Station"))
                {
                    EventManager._instance.StationSelected();
                }
            }
        }
    }
}
