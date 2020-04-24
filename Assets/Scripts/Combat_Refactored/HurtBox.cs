using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour, IDamageable
{
    [SerializeField]
    BoxCollider2D boxCollider2D;
    [HideInInspector] public Actor attacker, parent;
    IHaveHealth healthAmount;

    private void Awake()
    {
        this.gameObject.SetActive(true);

        parent = GetComponentInParent<Actor>();
        Debug.Log("The parent is: " + parent.name);

        //reference to the current health of the parent object's IHaveHealth Health property
        healthAmount = GetComponentInParent<IHaveHealth>();
    }

    /// <summary>
    /// This function is called when the hurtbox collides with a hitbox, it reduces the health of the object the hurtbox belongs to by the value of the int amount
    /// </summary>
    /// <param name="amount">value to reduce health by when function is called, the amount belongs to the hitbox that collides with the hurtbox</param>
    public void Hit(int amount)
    {
        //this runs to make sure Health doesn't fall into the negatives
        if(healthAmount.Health <= 0 )
        {
            healthAmount.Health = 0;
            //this.gameObject.SetActive(false);
            return;
        }
        //reduces parent object's health by the amount variable
        healthAmount.Health -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Finds the hitbox that collides with this hurtbox, used to determine how much damage the hitbox deals to the hurtbox's parent
        HitBox attackerHitBox = collision.gameObject.GetComponent<HitBox>();
        attacker = collision.gameObject.GetComponentInParent<Actor>();

        if (attackerHitBox == null)
            return;

        //Checks to make sure the parent of the hurtbox is not on the same layer as the parent of the attacking hitbox,
        //if they are on the same layer no damage is taken
        if (attacker.gameObject.layer != parent.gameObject.layer)
        {
            Hit(attackerHitBox.amount);
            Debug.Log($"Hurtbox: " + parent.name + " was hit for: " + attackerHitBox.amount + "damage.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attacker = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        attacker = null;
    }
}
