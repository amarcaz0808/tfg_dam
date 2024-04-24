using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity Stuff
    private Animator anim;

    //Primitive Variables
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; } //Get it anywhere, set it privately
    private bool dead; //Just to make sure the dying animation won't play twice

    /**
     * METHODS
     **/
    //Unity Methods
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
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
            //TODO - IFrames
        }
        else
        {
            //Oh he just f-ing died
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }//EndOf IF/ELSE checking whether or not Player is dead
    }//EndOf method TakeDamage

    public void RecoverHealth(float _healing)
    {
        //Makes Player regain health, to be later elaborated on
        currentHealth = Mathf.Clamp(currentHealth + _healing, 0, startingHealth);
    }//EndOf method RecoverHealth
}
