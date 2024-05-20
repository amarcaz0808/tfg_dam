using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_MainMenu : MonoBehaviour
{
    /**
     * METHODS
     **/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }//EndOf method Play
}
