using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sheikh KHaled

public class Instructions : MonoBehaviour
{
    // correspond to instruction menu sub-scenes
    public enum InstStates { instructions, about, howTo, collectibles, enemies };
    public InstStates currentState;

    // make panels as game objects
    public GameObject instructionsCanvas;
    public GameObject aboutPanel;
    public GameObject howToPanel;
    public GameObject collectiblesPanel;
    public GameObject enemiesPanel;

    // ensures first scene always active is the Instructions scene
    public void Awake()
    {
        currentState = InstStates.instructions;
    }

    //  check current active state
    public void Update()
    {
        switch (currentState)
        {
            case InstStates.instructions:
                // set active INstructions
                instructionsCanvas.SetActive(true);
                aboutPanel.SetActive(false);
                howToPanel.SetActive(false);
                collectiblesPanel.SetActive(false);
                enemiesPanel.SetActive(false);
                break;
            case InstStates.about:
                // set active about CC
                aboutPanel.SetActive(true);
                instructionsCanvas.SetActive(true);
                howToPanel.SetActive(false);
                collectiblesPanel.SetActive(false);
                enemiesPanel.SetActive(false);
                break;
            case InstStates.howTo:
                // set active how to play
                howToPanel.SetActive(true);
                instructionsCanvas.SetActive(true);
                aboutPanel.SetActive(false);
                collectiblesPanel.SetActive(false);
                enemiesPanel.SetActive(false);
                break;
            case InstStates.collectibles:
                // set active collectibles
                collectiblesPanel.SetActive(true);
                instructionsCanvas.SetActive(true);
                aboutPanel.SetActive(false);
                howToPanel.SetActive(false);
                enemiesPanel.SetActive(false);
                break;
            case InstStates.enemies:
                // set active enemies
                enemiesPanel.SetActive(true);
                instructionsCanvas.SetActive(true);
                aboutPanel.SetActive(false);
                howToPanel.SetActive(false);
                collectiblesPanel.SetActive(false);
                break;
           
        }
        
    }

    // the following functions make sure the correct panel is displayed 
    // for its corresponding button pressed from the instruction menu

    public void onAbout()
    {
        currentState = InstStates.about;
    }

    public void onHowTo()
    {
        currentState = InstStates.howTo;
    }

    public void onCollectibles()
    {
        currentState = InstStates.collectibles;
    }

    public void onEnemies()
    {
        currentState = InstStates.enemies;
    }

    // back button goes back to instruction page
    public void instrBack()
    {
        currentState = InstStates.instructions;
    }
}
