using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float amount;
    public BoxCollider2D hitBoxCollider;

    private void Awake()
    {
        hitBoxCollider.enabled = false;
    }
}
