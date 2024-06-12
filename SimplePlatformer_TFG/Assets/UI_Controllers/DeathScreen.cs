using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    public GameObject deathScreen;

    /**
     * METHODS
     **/
    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false); //We don't want this to trigger right away
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if(Health.dead == true)
        {
            StartCoroutine(WaitABit()); //You cannot use "yield" in Update for some reason
        }//EndOf IF
    }//EndOf method Update

    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(2); //Wait a bit...
        deathScreen.SetActive(true); //Now set the menu true
    }//EndOf IEnumerator method WaitABit

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }//EndOf method RestartLevel
}
