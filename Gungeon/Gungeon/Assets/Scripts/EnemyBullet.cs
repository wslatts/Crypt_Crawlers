/*
 * Controls enemy bullet
 * 
 * by Wendy Slattery
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int damage = 1;
    public Rigidbody2D bulletRB;
    public Vector2 bulletDirection;
    Player target;
    
    public GameObject hitEffect;
    public bool hasEffect = true;

    void Start()
    {
        target = GameObject.FindObjectOfType<Player>();
        bulletDirection = (target.transform.position - transform.position).normalized * bulletSpeed;
        bulletRB.velocity = new Vector2(bulletDirection.x, bulletDirection.y);
        Destroy(gameObject, 3f);        
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            if(hasEffect == true)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, .1f);
            }            
            player.TakeDamage(damage);
            Destroy(gameObject);            
        }

        if (hitInfo.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .1f);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .1f);
        }
    }
}
