using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class HealthPickup : Pickup
{
    [SerializeField]
    int healthToRestore;
    Player player;
    [Tooltip("This should be a trigger and should be on the layer Pickups.")]
    BoxCollider2D collider2D; //should be a trigger

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        collider2D = GetComponent<BoxCollider2D>();

        collider2D.isTrigger = true;
        this.gameObject.SetActive(true);
    }

    public override void PickUp(int amountToRestore)
    {
        player.Health += amountToRestore;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && player.Health < player.maxHealth)
        {
            this.gameObject.SetActive(false);
            PickUp(healthToRestore);
        }
    }
}
