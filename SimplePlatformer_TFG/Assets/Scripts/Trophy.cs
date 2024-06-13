using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    private Animator anim;
    private GameObject winScreen;

    /**
     * METHODS
     **/
    private void Awake()
    {
        anim = GetComponent<Animator>();

        winScreen = GetComponent<GameObject>();
        winScreen.SetActive(false);
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        
    }//EndOf method Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //TODO
            /**
             * Block player movement
             * Trigger animation for winning (Trophy disappears flashily, and a text appears congratulating Player)
             * Switch to scene of next level (just a screen with "Work in Progress", no more levels for now)
             **/
            anim.SetTrigger("obtained"); //Trophy explodes
            GetComponent<PlayerMovement>().enabled = false; //Player can't move for now
            winScreen.SetActive(true); //WIN SCREEN, YOU WON!!!
            WaitABit(8); //Cherish that moment
            winScreen.SetActive(false); //get rid of this just in case
            SceneManager.LoadScene("WIP"); //Switch to the next "level", but there's none for now sooo... yeah!
        }//EndOf IF
    }//EndOf overwritten method OnTriggerEnter2D

    private IEnumerator WaitABit(float _Seconds)
    {
        yield return new WaitForSeconds(_Seconds); //Wait a bit...
    }//EndOf IEnumerator method WaitABit
}
