using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera main;
    public Transform objectToFollow;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        if (objectToFollow == null)
            objectToFollow = Transform.FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        main.transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y,main.transform.position.z);
    }
}
