using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity stuff
    private BoxCollider2D boxCollider; //Get the Projectile's hitbox
    private Animator animator; //Get its Animator, and thus the state and ability to change it at will

    //Primitive variables
    [SerializeField] private float speed; //Unity-editable speed for the projectile

    private float direction; //The direction the projectile will be shot at
    private bool hit; //Whether or not it has hit something
    private float lifetime; //How long the projectile keeps going before self-destructing

    /**
     * METHODS
     **/
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if (hit) return; //Don't go any further with this code if Projectile has hit anything

        float movementSpeed = speed * Time.deltaTime * direction; 
        // ^ The Projectile speed will be the same, regardless of framerate (thanks to our saviour, Time.deltaTime). Although its direction will depend on the Player's position
        transform.Translate(movementSpeed, 0, 0); //ACTUALLY set the projectile's speed to it

        lifetime += Time.deltaTime;
        if(lifetime > 3f) gameObject.SetActive(false);
    }//EndOf method Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If Projectile hit anything
        hit = true; //It has obviously hit
        boxCollider.enabled = false; //its hitbox will be disabled

        animator.SetTrigger("explode"); //And the explosion animation will play
    }//EndOf private method OnTriggerEnter2D

    public void SetDirection(float _direction)
    {
        //This method is used to actually shoot the projectile, and to determine where it shall
        lifetime = 0f;
        direction = _direction; //Set the global variable's direction to that of this instance
        gameObject.SetActive(true); //Activate the current projectile
        hit = false; //The projectile won't hit something first-frame, not SO quick at least
        boxCollider.enabled = true; //It will need a hitbox to know if it has hit something, thus triggering "OnTriggerEnter2D"

        //Change the projectile's direction according to where the player's facing
        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            //IF the projectile's direction doesn't match the correct orientation, then flip it!
            localScaleX = -localScaleX;
        }//EndOf IF

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); //After all checks for correct orientation have been done, assign it
    }//EndOf public method SetDirection

    private void Deactivate()
    {
        gameObject.SetActive(false); //"Delete" the current projectile
    }//EndOf method Deactivate
}
