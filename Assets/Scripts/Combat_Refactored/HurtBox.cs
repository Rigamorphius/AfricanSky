using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
/// <summary>
/// This should be attached to a child of the Actor that it belongs to.
/// </summary>
public class HurtBox : MonoBehaviour, IDamageable
{
    [SerializeField]
    BoxCollider2D boxCollider2D;
    public Actor attacker, parent;
    IHaveHealth healthAmount;

    public event EventHandler OnHealthChanged;

    private void Awake()
    {
        this.gameObject.SetActive(true);
        boxCollider2D.isTrigger = true;
        parent = GetComponentInParent<Actor>();
        Debug.Log("The parent is: " + parent.name);

        //reference to the current health of the parent object's IHaveHealth Health property
        healthAmount = GetComponentInParent<IHaveHealth>();
    }

    private void Start()
    {
        if (parent == null)
            parent = GetComponentInParent<Actor>();
        Debug.Log("The parent is: " + parent.name);
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
            return;
        }
        //reduces parent object's health by the amount variable
        healthAmount.Health -= amount;
        if (OnHealthChanged != null)
            OnHealthChanged(this, EventArgs.Empty); //triggers event for the ui to see
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
            //Hit(attackerHitBox.amount);
            parent.TakeDamage(attackerHitBox.amount);
            //Debug.Log($"Hurtbox: " + parent.name + " was hit for: " + attackerHitBox.amount + "damage.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        attacker = null;
    }
}
