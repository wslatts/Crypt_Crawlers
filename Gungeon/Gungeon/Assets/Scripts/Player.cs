/*
 * Scripts associated with player damage taken by bullet hits
 * Instantiates the death effect animation and removes player
 * after health reaches zero.
 * 
 * by Wendy Slattery
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject deathReaction;
    public GameObject gameOver;

    public void TakeDamage (int damage)
    {
        GameObject.Find("Player").GetComponent<HearthSystem>().ChangeHealth(-damage);
        if (PlayerStats.health <= 0)
        {
            Die();
            GameOver();
        }
    }

    void Die ()
    {
        GameObject reaction = Instantiate(deathReaction, transform.position, Quaternion.identity);
        Destroy(reaction, 1.4f);
        Destroy(gameObject);        
    }

    // Written by Sheikh Khaled
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
