using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    Animator anim;
    //used to check whether the Player is in sight of Enemy
    public bool inSight;
    public GameObject player;
    private Rigidbody2D rigidBody;

    //Speed in which the Enemy moves.
    private float movementSpeed;
    public float walkMoveSpeed = 2;

    //Position in which the Enemy is facing.
    private bool facingRight;
    private bool facingLeft;

    //Max X and Y movement constraints for Enemy
    public float xMin, xMax, yMin, yMax;

    public GameObject target;
    private GameObject frontTarget;
    private GameObject backTarget;
    public float targetDistance;
    public float frontTargetDistance;
    public float backTargetDistance;

    public GameObject attackBox1, attackBox2, attackBox3;
    public Sprite attack1Hitframe, attack2Hitframe, attack3Hitframe;
    SpriteRenderer currentSprite;

    void Awake()
    {
        anim = GetComponent<Animator>();  
    //Searches for the object with the Player tag
    player = GameObject.FindGameObjectWithTag("Player");
        
        rigidBody = GetComponent<Rigidbody2D>();
        movementSpeed = walkMoveSpeed;
        currentSprite = GetComponent<SpriteRenderer>();
        frontTarget = GameObject.Find("Enemy Front Target");
        backTarget = GameObject.Find("Enemy Back Target");
    }

    void FixedUpdate()
    {

        UpdateAttackBoxes();

        //Finds and follows the players position while keeping the Enemy in X and Y constraints
        Vector2 direction = player.transform.position - transform.position;
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        rigidBody.position = new Vector2(Mathf.Clamp(rigidBody.position.x, xMin, xMax), Mathf.Clamp(rigidBody.position.y, yMin, yMax));

        // Flips the direction the Enemy is looking
        if (transform.position.x < player.transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > player.transform.position.x && facingRight)
        {
            Flip();
        }

        frontTargetDistance = Vector3.Distance(frontTarget.transform.position, gameObject.transform.position);
        backTargetDistance = Vector3.Distance(backTarget.transform.position, gameObject.transform.position);

        if (frontTargetDistance < backTargetDistance) {
            target = frontTarget;
            transform.position = Vector2.MoveTowards(transform.position, frontTarget.transform.position, movementSpeed * Time.deltaTime);
        } else if (frontTargetDistance > backTargetDistance) {
            target = backTarget;
            transform.position = Vector2.MoveTowards(transform.position, backTarget.transform.position, movementSpeed * Time.deltaTime);
        }

        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        
    }

    private void UpdateAttackBoxes()
    {
        //Checks to see if the attack frame is equal to the current in-game frame and runs code.
        if (attack1Hitframe == currentSprite.sprite)
        {
            attackBox1.gameObject.SetActive(true);
        }
        else
        {
            attackBox1.gameObject.SetActive(false);
        }

        if (attack2Hitframe == currentSprite.sprite)
        {
            attackBox2.gameObject.SetActive(true);
        }
        else
        {
            attackBox2.gameObject.SetActive(false);
        }

        if (attack3Hitframe == currentSprite.sprite)
        {
            attackBox3.gameObject.SetActive(true);
        }
        else
        {
            attackBox3.gameObject.SetActive(false);
        }
    }

    // Activates as the Player enters the inSight trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            inSight = true;
            anim.Play("Walk");
           Destroy(gameObject);
        }
    }

    // deactivates as the Player exits the inSight trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            inSight = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.enemiesKilled = GameManager.enemiesKilled + 1;
        GameManager.totalEnemiesKilled = GameManager.totalEnemiesKilled + 1;
    }

    //Allows for the Enemy to flip directions when needed.
    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
