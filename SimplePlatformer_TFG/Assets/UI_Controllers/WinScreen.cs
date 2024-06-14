using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    public GameObject winScreen;

    /**
     * METHODS
     **/
    private void Awake()
    {

    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if(Trophy.hasWon)
        {
            winScreen.SetActive(true);
        }//EndOf IF
    }//EndOf method Update

    public void SwitchSceneWIP()
    {
        SceneManager.LoadScene("WIP");
    }//EndOf method SwitchSceneWIP

    //TODO - Make a method for switching to any given Scene, useful for when more levels are added
}
