﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Actor, IHaveHealth
{
    public Transform playerTransform;
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    public float attackRange;
    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    [HideInInspector]
    public bool isFlipped = false;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool isJumpedPressed;
    [HideInInspector]
    public bool isAttackPressed;
    [HideInInspector]
    public bool isDead = false;

    public float Health { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        SetUpComponents();
    }

    void SetUpComponents()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        this.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        SetUpComponents();
    }

    // Update is called once per frame
    void Update()
    {
        //LookInDirectionMoving();
        //ChasePlayer();
        CheckForDeath();
        if(!isDead)
            LookAtPlayer();
    }

    void CheckForDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        else if (currentHealth > 0)
            isDead = false;
        animator.SetBool("isDead", isDead);
    }


    public void LookInDirectionMoving()
    {
        //Checks for input and sets the player to look that way
        if (rigidbody2D.velocity.x > 0 && isFlipped)
        {
            //spriteRenderer.flipX = false;
            Flip();
        }
        else if (rigidbody2D.velocity.x < 0 && !isFlipped)
        {
            //spriteRenderer.flipX = true;
            Flip();
        }
    }

    void Flip()
    {
        isFlipped = !isFlipped;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void LookAtPlayer()
    {
        Vector2 flipped = transform.localScale;
        if(transform.position.x > playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        if(transform.position.x < playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    //This event should be observed by the health bar system
    public event EventHandler OnHealthChanged;
    /// <summary>
    /// This function is called when the hurtbox collides with a hitbox, it reduces the health of the object the hurtbox belongs to by the value of the int amount
    /// </summary>
    /// <param name="amount">value to reduce health by when function is called, the amount belongs to the hitbox that collides with the hurtbox</param>
    public override void TakeDamage(float amount)
    {
        //this runs to make sure Health doesn't fall into the negatives
        if (Health <= 0)
        {
            Health = 0;
            return;
        }
        //reduces parent object's health by the amount variable
        Health -= amount;
        if (OnHealthChanged != null)
            OnHealthChanged(this, EventArgs.Empty); //triggers event for the ui to see

        Debug.Log($"The {name} was hit for {amount} damage");
    }
}
