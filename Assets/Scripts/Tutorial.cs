using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameManager.enemiesKilled == 1 && CameraController.isFollowing == false)
        {
            CameraController.isFollowing = true;
            ETutorialController.gosign.SetActive(true);
            GameManager.enemiesKilled = 0;
        }
    }
}
