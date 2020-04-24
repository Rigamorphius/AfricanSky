using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStopTutorial : MonoBehaviour
{
    private GameObject otherObject;
    private GameObject Spawn;
    private GameObject Tut;
    private void Start()
    {
        otherObject = GameObject.Find("Camera Mid");
        Spawn = GameObject.Find("EnemySpawn");
        Tut = GameObject.Find("Tutorial");
        Spawn.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks to see if the otherObject has the specified tag and if so, deactivitates the object and turns isFollowing false.
        if (otherObject.tag == "CameraMidPoint")
        {
            otherObject = otherObject.transform.parent.gameObject;
            CameraController.isFollowing = false;
            gameObject.SetActive(false);
            Tut.SetActive(false);
            ETutorialController.gosign.SetActive(false);
            Spawn.SetActive(true);
        }
    }
}
