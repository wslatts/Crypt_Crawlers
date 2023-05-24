using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class ChangeLevel : MonoBehaviour
{

    public string level;
    private string currentLevel;
    public static int currentLevelChangeMoney = 0;


    void Start()
    {
     
        currentLevel = SceneManager.GetActiveScene().name;
        Debug.LogWarning("currentLevel name:" + SceneManager.GetActiveScene().name);

        if (currentLevel == "Level 1")
        {
            currentLevelChangeMoney = 10; //100
        }
        else if (currentLevel == "Level 2")
        {
            currentLevelChangeMoney = 25; //250
        }
        else if (currentLevel == "Level 3")
        {
            currentLevelChangeMoney = 50; //500
        }

        Debug.LogWarning("Current scene number coin needed to advance is " + currentLevelChangeMoney);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (PlayerStats.money >= currentLevelChangeMoney))
        {
            if (currentLevel == "Level 3")
            {
                // pause and print some message
                Debug.LogWarning("End of game");
                return;
            }
            Debug.Log("Entered trigger");
            SceneManager.LoadScene(level);
        }
    }
}


/*
public class ChangeLevel : MonoBehaviour
{ 

    public string level;
    public static int levelNumber = 0;
    public static int changeLevelMoney = 10; // I'd put this at 100 for play
    public static int currentLevelChangeMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        ++levelNumber;
        Debug.LogWarning("Current level number is " + levelNumber);
        currentLevelChangeMoney = levelNumber * changeLevelMoney;
        Debug.LogWarning("Current level number coin needed to advance is " + currentLevelChangeMoney);
    }

        void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("Player") && (PlayerStats.money >= currentLevelChangeMoney))
        {
            if (levelNumber == 3)
            {
                    // pause and print some message
                    Debug.LogWarning("End of game");
                    return;
            }
            Debug.Log("Entered trigger");
            SceneManager.LoadScene(level);
        }
    }

}
*/