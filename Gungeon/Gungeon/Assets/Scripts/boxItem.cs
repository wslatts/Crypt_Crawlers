using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxItem : MonoBehaviour
{
    //public Transform spawnPoint;
    public GameObject item;
    private bool broken;
    public AudioClip boxBreak;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !broken)
        {
            audioSource.PlayOneShot(boxBreak, 0.1F);
            broken = true;
            GetComponent<Animator>().SetTrigger("break");
            item.SetActive(true);
        }
    }
}