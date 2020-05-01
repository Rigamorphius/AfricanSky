using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This should be attached to a child of the Actor that it belongs to.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class HitBox : MonoBehaviour
{
    public float amount;
    public BoxCollider2D hitBoxCollider;

    private void Awake()
    {
        hitBoxCollider.isTrigger = true;
        hitBoxCollider.enabled = false;
    }
}
