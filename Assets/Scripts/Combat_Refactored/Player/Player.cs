using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : Actor, IHaveHealth, IHaveLives
{
    public Transform playerTransform;
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    [Tooltip("This should be at the character's feet, it collides with the environment.")]
    public BoxCollider2D environmentCollider;

    [SerializeField]
    public float walkSpeed;
    [SerializeField]
    int currentLives;
    [SerializeField]
    int startingLives; //the lives the player starts with
    [SerializeField]
    private float currentHealth; //the health the player starts with
    [SerializeField]
    public float maxHealth = 1f; //the max health the player can have, starts out with this


    #region Animator Variables
    [HideInInspector]
    public bool isFlipped = false;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public float moveHorizontal;
    [HideInInspector]
    public float moveVertical;
    [HideInInspector]
    public bool isJumpedPressed;
    [HideInInspector]
    public bool isAttackPressed;
    #endregion

    public float Health 
    { 
        get => currentHealth;
        set => currentHealth = value;
    }


    public int Lives { get => currentLives; set => currentLives = value; }

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        Lives = startingLives;
    }

    private void Update()
    {
        HandleInput();
        CheckForMovement();
        CheckForDeath();
        KeepHealthFromExceedingMax();
    }

    void Respawn()
    {
        if(Lives > 0)
        {
            Health = maxHealth;
            animator.ResetTrigger("IsDead");
        }
        else //Game over stuff goes here, should probably be moved to a game manager and use an event to tell it the player died
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    void KeepHealthFromExceedingMax()
    {
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    void HandleInput()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        isJumpedPressed = Input.GetKeyDown(KeyCode.Space);
        isAttackPressed = Input.GetKeyDown(KeyCode.Z);
    }

    public void Attack()
    {
        if(isAttackPressed)
            animator.SetBool("Punch", true);
    }

    public void CheckForJump()
    {
        if (isJumpedPressed)
            animator.SetBool("Jump", true);
    }

    void CheckForMovement()
    {
        if (moveHorizontal == 0 && moveVertical == 0)
            isMoving = false;
        else if(moveHorizontal != 0 || moveVertical != 0)
            isMoving = true;
        animator.SetBool("IsMoving", isMoving);
    }

    /// <summary>
    /// This sets the player sprite to face the direction the player is moving
    /// </summary>
    public void LookInDirectionMoving()
    {
        //Checks for input and sets the player to look that way
        if(rigidbody2D.velocity.x > 0 && isFlipped)
        {
            Flip();
        }
        else if(moveHorizontal < 0 && !isFlipped)
        {
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

    //This event should be observed by the lives counter in the UI
    public event EventHandler OnLivesChanged;
    void CheckForDeath()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            OnLivesChanged(this, EventArgs.Empty);
        }
        else if (currentHealth > 0)
            isDead = false;
        animator.SetBool("IsDead", isDead);
    }

    //This event should be observed by the health bar system
    public event EventHandler OnHealthChanged;
    /// <summary>
    /// This function is called when the hurtbox collides with a hitbox, it reduces the health of the object the hurtbox belongs to by the value of the int amount
    /// </summary>
    /// <param name="amount">value to reduce health by when function is called, the amount belongs to the hitbox that collides with the hurtbox</param>
    public override void TakeDamage(int amount)
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