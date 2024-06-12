using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity Stuff
    private Animator anim;

    //Primitive Variables
    [Header("Health")] //This is merely for organizational purposes, it will separate different fields in "categories" in Unity, making it easier to see where everything is
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } //Get it anywhere, set it privately
    public static bool dead { get; set; } //Just to make sure the dying animation won't play twice

    [Header("iFrames")]
    //Against how I was getting used to organize and comment my code, after a Header the first field has to be either public or a SerializeField. No biggie tho!
    [SerializeField] private float iFrameDuration; //For how long will the flashes take place
    [SerializeField] private int flashesAmount; //How many flashes there will be
    private SpriteRenderer spriteRenderer; //Make the flashes happen

    /**
     * METHODS
     **/
    //Unity Methods
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dead = false;
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        /*
         //That works, cool
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }//EndOf IF for a testing button to damage Player

        if (Input.GetKeyDown(KeyCode.R))
        {
            RecoverHealth(1);
        }//EndOf IF for a testing button to heal Player
        */
    }//EndOf method Update

    //Non-Unity Methods
    public void TakeDamage(float _damage)
    {
        //Makes the range of possible health values between the startingHealth and 0, while setting the currentHealth depending on how much damage we received
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            //Still alive!
            anim.SetTrigger("hurt");
            //IFrames
            StartCoroutine(Invulnerability()); //Start this method in a separate thread
        }
        else
        {
            //Oh he just f-ing died
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }//EndOf IF checking bool dead == false
        }//EndOf IF/ELSE checking whether or not Player is dead
    }//EndOf method TakeDamage

    public void RecoverHealth(float _healing)
    {
        //Makes Player regain health, to be later elaborated on
        currentHealth = Mathf.Clamp(currentHealth + _healing, 0, startingHealth);
    }//EndOf method RecoverHealth

    private IEnumerator Invulnerability()
    {
        //Deactivate collisions between set layers
        Physics2D.IgnoreLayerCollision(8, 9, true); //Layer8 is Player; Layer9 is Enemy; TRUE, the collisions will be ignored

        for (int i = 0; i < flashesAmount; i++)
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f); //RED, GREEN, BLUE, ALPHA (Transparency)
            yield return new WaitForSeconds(iFrameDuration / (flashesAmount * 2)); //Wair for a bit before continuing
            spriteRenderer.color = Color.white; //Go back to the regular colors (not actually white, it's kinda weird but, play a bit with Unity's Sprite Color parameters and you'll see)
            yield return new WaitForSeconds(iFrameDuration / (flashesAmount * 2)); //Wait just once more (before the loop laps over, that is)
        }//EndOf FOR-Loop

        //Reactivate collisions between set layers
        Physics2D.IgnoreLayerCollision(8, 9, false); //Same as before, just re-activate collisions
    }//EndOf IEnumerator Invulnerability
}
