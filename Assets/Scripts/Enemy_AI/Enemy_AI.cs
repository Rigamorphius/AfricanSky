using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI : Actor
{
    public Transform target;

    Seeker seeker;
    public Rigidbody2D rb2D;

    Path path;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    public float nextWaypointDistance;
    public float speed;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb2D.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = (Vector2)path.vectorPath[currentWaypoint] - rb2D.position.normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb2D.MovePosition(Vector2.MoveTowards(rb2D.position,path.vectorPath[currentWaypoint], speed * Time.fixedDeltaTime));

        float distance = Vector2.Distance(rb2D.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

}
