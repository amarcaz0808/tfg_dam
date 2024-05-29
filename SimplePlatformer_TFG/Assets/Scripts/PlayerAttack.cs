using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /**
     * ATTRIBUTE / GLOBAL VARIABLES
     **/
    //Unity stuff
    private Animator anim;
    private PlayerMovement playerMovement;

    //Primivite variables
    [SerializeField] private float attackCooldown; //Editable through Unity
    [SerializeField] private Transform firePoint; //The point from which the fireball projectile will be shot
    [SerializeField] private GameObject[] fireballs;

    private float cooldownTimer;

    /**
     * METHODS
     **/
    private void Awake()
    {
        //Get the references for the Player's Animator and it's Movement script
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = Mathf.Infinity;
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetMouseButton(0)
            && cooldownTimer > attackCooldown
            && playerMovement.canAttack())
            {
                // ^ IF Player hit the attack button, and enough time has passed since the last attack, AND they are able to attack
                // v THEN Attack
                Attack();
            }//EndOf IF checking for a LeftClick AND for there to be enough cooldown for the attack

            cooldownTimer += Time.deltaTime; //Update the cooldown timer every frame, with a consistent per-second sum thanks to Time.deltaTime
        }//EndOf IF fixing the error of shooting a fireball when game is paused
    }//EndOf method Update

    private void Attack()
    {
        anim.SetTrigger("attack"); //Set Player's animation to the one called "attack"
        cooldownTimer = 0; //When ATTACK is triggered, reset the cooldown timer

        fireballs[FindFireball()].transform.position = firePoint.position; //Set the shooting point
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x)); //Set the fireball's direction as that of where the Player's facing
        // ^ Do that for every fireball
    }//EndOf method Attack

    private int FindFireball()
    {
        //This loop checks for all fireballs in the array set in Unity for availability
        //As soon as it detects an available fireball, it will return it
        for(int i = 0; i<fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }//EndOf inner IF
        }//EndOf FOR-Loop

        //By default, this returns 0 (more as to avoid an error message than anything)
        return 0;
    }//EndOf int-returning method FindFireball
}
