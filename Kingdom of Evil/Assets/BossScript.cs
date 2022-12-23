using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
     public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //play hurt animation
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Boss died");
       
       GetComponent<Collider2D>().enabled = false;
       this.enabled = false;//disable the enemy
       myRigidbody.simulated = false;
      
    }
    
}
