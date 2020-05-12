using System;
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

    private AudioSource punch1Source;


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

    [SerializeField] public float chaseRange = 10f;

    public float Health { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        SetUpComponents();
    }

    void SetUpComponents()
    {
        punch1Source = GetComponent<AudioSource>();
        playerTransform = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        animator.SetBool("isMoving", false);
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

    public event EventHandler OnDeath;
    void CheckForDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            OnDeath?.Invoke(this,EventArgs.Empty);

            CameraController.isFollowing = true;
            Tutorial.gosign.SetActive(true);

            DropPickup();
            this.gameObject.SetActive(false);
        }
        else if (currentHealth > 0)
            isDead = false;
        animator.SetBool("isDead", isDead);
        
    }

    /// <summary>
    /// These gameobjects are the prefabs the enemy will drop on death
    /// </summary>
    [SerializeField] GameObject lifePickup, healthPickup;
    void DropPickup()
    {
        Instantiate(lifePickup,this.transform);
        lifePickup.transform.parent = null;
        
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
        PlaySound();

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

    void PlaySound()
    {
        punch1Source.Play();
    }
}
