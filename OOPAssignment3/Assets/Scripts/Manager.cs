using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Transform mainMenu, instructionsMenu;

    public void QuitGame()
    {
        //allows the pplayer to quit from the main menu
        Application.Quit();
    }

    public void InstructionsMenu(bool clicked)
    {
        //switches between instruction menu and start menu when buttons are clicked
        if(clicked == true)
        {
            instructionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            instructionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
}
