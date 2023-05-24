using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HearthSystem : MonoBehaviour
{
    private int maxHearthAmount = 5;
    public int startHearths = 4;
    // each hearth is 3 integers, maximum of 5 hearths, 4 enabled at the moment
    public int curHealth = 0;
    //private int curHealth = 0;
    //private int maxHealth;
    private int healthPerHearth = 3;

    public Image[] healthImages;
    public Sprite[] healthSprites;

    void Start()
    {
        curHealth = PlayerStats.health;
        //curHealth = startHearths * healthPerHearth;
        //maxHealth = maxHearthAmount * healthPerHearth;
        //AddHearthContainer();
        checkHealthAmount();
    }
    /*
    private void Update()
    {
        curHealth = PlayerStats.health;
    }
    */
    void checkHealthAmount()
    {
        for (int i = 0; i < maxHearthAmount; i++)
        {
            if (startHearths <= i)
            {
                healthImages[i].enabled = false;
            }
            else 
            {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearths();
    }

    void UpdateHearths()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else 
            {
                i++;
                if (curHealth >= i * healthPerHearth)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else 
                {
                    int currentHeartHealth = (int)(healthPerHearth - (healthPerHearth * i - curHealth));
                    int healthPerImage = healthPerHearth / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }


    public void ChangeHealth(int amount)
    {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHearths * healthPerHearth);
        PlayerStats.health = curHealth;
        UpdateHearths ();
    }

    public void AddHearthContainer()
    {
        startHearths++;
        startHearths = Mathf.Clamp(startHearths, 0, maxHearthAmount);

        //curHealth = startHearths * healthPerHearth;
        //maxHealth = maxHearthAmount * healthPerHearth;

        checkHealthAmount();
    }
}
