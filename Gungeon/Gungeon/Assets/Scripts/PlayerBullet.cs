/*
 * Controls Player Bullets
 * 
 * by Wendy Slattery
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D bulletRB;
    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .1f);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, .1f);
            Destroy(gameObject);
        }
    }    
}
