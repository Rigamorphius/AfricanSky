using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls how the enemy moves and behaves.
/// Should have a reference to the Enemy Script
/// Should have different types of patrols?
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public float maxPatrolLength;

    public enum PatrolStates { None, Horizontal, Vertical, Random};

    public PatrolStates patrolState;

    private void Update()
    {
        
        
    }

}
