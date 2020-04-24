using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatComponent : MonoBehaviour
{
    [SerializeField] Text healthText;

    public static float health = 100;

    public void DamagedBy(float damage)
    {
        health -= damage;
        //Debug.Log($"{gameObject.name} health: {health}");
    }

    public void HealedBy(float heal)
    {
        health += heal;
    }
   
    public void UpdateText()
    {
        //Debug.Log($"{this.gameObject.name} health: {health}");
       // healthText.text = $"{this.gameObject.name} Health: {health}";  //{this.gameObject.name}
    }

    private void Update()
    {
        UpdateText();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if(health <= 0)
        {
            Debug.Log($"{this.gameObject.name} death");
            PlayerController.lives = 0;
            gameObject.SetActive(false);        
        }

    }

}
