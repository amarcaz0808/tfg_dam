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
    private AudioSource audioSource;
    public static bool hasWon { get; private set; }

    [SerializeField] private AudioClip obtainSFX;

    /**
     * METHODS
     **/
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if (hasWon)
        {
            //Deactivate collisions between set layers
            Physics2D.IgnoreLayerCollision(8, 11, true); //Layer8 is Player; Layer11 is Trophy; TRUE, the collisions will be ignored
        }
        else
        {
            Physics2D.IgnoreLayerCollision(8, 11, false); //Collisions between Player and Trophy will resume
        }
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
            audioSource.clip = obtainSFX;
            audioSource.Play();
            hasWon = true; //This will be heard by all scripts that need it
        }//EndOf IF
    }//EndOf overwritten method OnTriggerEnter2D

    /*
    private IEnumerator WaitABit(float _Seconds)
    {
        yield return new WaitForSeconds(_Seconds); //Wait a bit...
    }//EndOf IEnumerator method WaitABit
    */
}
