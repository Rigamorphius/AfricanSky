using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Actor : MonoBehaviour
{
    public virtual void TakeDamage(int amount)
    { }
}
