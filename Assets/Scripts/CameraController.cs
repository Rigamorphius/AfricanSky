using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Speed in which the Camera follows
    public float cameraSpeed;
    //Boolean that checks whether the camera is currently following
    public static bool isFollowing;

    public float xMin, xMax;
    //GameObject that the camera will follow
    private GameObject playerTarget;
    private GameObject cam;
    //Position that the Camera follows
    private Vector3 playerXPos;

    void Awake()
    {
        //Searches for the object with the Player tag attached
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax),4.54f,-20);
        //Searches for the playerXPos position
        playerXPos = new Vector3(playerTarget.transform.position.x, 4.54f, -100f);
        //Checks if isFollowing is true and then proceeds to follow the playerXpos
        if (isFollowing == true) {
            transform.position = Vector3.Lerp(transform.position,playerXPos,cameraSpeed);
            Vector3 viewPos = transform.position;

            if (transform.position.y < playerTarget.transform.position.y)
            {
                transform.position = new Vector3(playerTarget.transform.position.x, 0f , -100f);
            }

        }
    }
}
