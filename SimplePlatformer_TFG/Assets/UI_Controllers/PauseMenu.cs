using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity Stuff
    public GameObject pauseMenu;
    //Primitive Variables
    public static bool isPaused;

    /**
     * METHODS
     **/
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); //We don't want the Pause Menu to be active as we start the game
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }//EndOF IF/ELSE
        }//EndOf IF
    }//EndOf method Update

    public void PauseGame()
    {
        //Paused! NOW we want the menu to be active
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //Therefore, time will stop ZA WARUDO
        isPaused = true; //For the sake of the Update method
    }//EndOf method PauseGame

    public void ResumeGame()
    {
        //Unpaused! Now we want the menu to NOT be active
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; //Therefore, time will stop TOKI WA UGOKI DESU
        isPaused = false; //For the sake of the Update method
    }//EndOf method ResumeGame

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; //Resume time
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //Load the MainMenu scene/"level"
    }//EndOf method GoToMainMenu
}
