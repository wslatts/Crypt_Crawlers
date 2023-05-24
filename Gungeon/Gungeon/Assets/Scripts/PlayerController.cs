/*
 * Aspects pertaining to player movements, animations, shoot mechanisms related to mouse pointer, 
 * weapons triggers, and associated sounds are included here.
 * 
 * by Daniel Powley, Wendy Slattery, Luca Labruna, Steven Berkowitz
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    // movements
    public float speed;
    Vector2 movement;
    Rigidbody2D playerRB;       
    public Animator playerAnimator;
    private int count;

    // mouse aim
    public Vector2 mousePos;
    public GameObject crosshair;
    public Camera cam;

    // weapon
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public Animator weapon;

    // audio
    public AudioClip Shoot1Sound;
    AudioSource audioSource;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        count = 0;
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);        
        SetCrosshairToMouse();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * speed * Time.fixedDeltaTime);
        Vector2 lookDirection = mousePos - playerRB.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // ensure that player is facing mouse aim
        // Left Facing: Fip Player 
        if (playerRB.transform.position.x > mousePos.x) 
        {
            playerRB.transform.rotation = Quaternion.Euler(0, 180, 0); // turn left facing

            if (lookAngle > 90 && lookAngle <= 180) //TL Quadrant 2
            {
                lookAngle = lookAngle - 180;
                if (lookAngle < -50)
                {
                    lookAngle = -50f;
                }                
                weapon.transform.rotation = Quaternion.Euler(0f, 180f, -lookAngle);
            }
            
            if (lookAngle < -90 && lookAngle >= -180) // BL Quadrant 3
            {
                lookAngle = lookAngle + 180;
                if (lookAngle > 50)
                {
                    lookAngle = 50f;
                }
                weapon.transform.rotation = Quaternion.Euler(0f, 180f, -lookAngle);
            }                          
        }
        else
        {
            // Right Facing: mouse to right of player
            playerRB.transform.rotation = Quaternion.Euler(0, 0, 0); // keep right facing
            
            // limit fire range
            if (lookAngle > 50)  // TR Quadrant 1
            {
                lookAngle = 50f;
            }
            if (lookAngle < -50)  // BR Quadrant 4
            {
                lookAngle = -50f;
            }
            // aim weapon at mouse
            weapon.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }        
    }

    void SetCrosshairToMouse()
    {
        if (movement != Vector2.zero)
        {
            crosshair.transform.localPosition = mousePos;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        weapon.SetTrigger("Shoot");
        audioSource.PlayOneShot(Shoot1Sound, 0.7F);
    }

}
