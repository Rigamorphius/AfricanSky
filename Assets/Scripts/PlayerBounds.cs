using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public float yMin, yMax;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, Camera.main.transform.position.x - 11.5f, Camera.main.transform.position.x + 11.5f);
        viewPos.y = Mathf.Clamp(viewPos.y, yMin, yMax);
        transform.position = viewPos;
    }
}
