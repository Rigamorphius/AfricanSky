using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static int enemiesKilled;
    public static int totalEnemiesKilled;
    // Update is called once per frame

    private void Start()
    {
        Debug.Log(CameraController.isFollowing);

    }
    void FixedUpdate()
    {
        if (enemiesKilled == 6 && CameraController.isFollowing == false) {
            CameraController.isFollowing = true;
            ETutorialController.gosign.SetActive(true);
            enemiesKilled = 0;
        }

        if (totalEnemiesKilled >= 18) {
            SceneManager.LoadScene("WinScene");
        }

        if (PlayerController.lives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
