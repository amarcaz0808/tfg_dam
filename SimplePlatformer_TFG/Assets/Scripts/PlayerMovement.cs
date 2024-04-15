using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
     * Attributes
    **/
    //Unity Stuff
    private Rigidbody2D body; //To be later used by all methods, potentially
    private Animator anim;

    //Primitive stuff
    [SerializeField] private float speed; //speed value to alter the Player's speed, Serialized so it's editable from Unity

    private bool grounded;

    /**
     * METHODS
     **/

    //the "Awake" method is called once, when the game loads
    private void Awake()
    {
        //Since this whole script is linked to the Player, we need to get the instance of its RigidBody2D
        body = GetComponent<Rigidbody2D>(); //Get a component of the Rigidbody2D type, in this case the one given to Player
        //Also get the instance of the Animator
        anim = GetComponent<Animator>();
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        /**
         * Both the "Awake" and "Start" methods are called only ONCE when the game starts/loads
         * Therefore, in this method we shall program everything we want to be able to happen while the game is running
         * Things like, player movement, health checks, overworld changes, characters talking to each other, etc.
         **/
        //Make later calls to the horizontal input easier
        float horizontalInput = Input.GetAxis("Horizontal");
        //A Vector2 is basically what allows us to move an object in a 2-axis way, horizontal (x) and vertical (y)
        //There exists a Vector3 and a Vector4, but those are for other kinds of games (3D, of course for the added axis of depth, and 4D, which is WAY out of our league right now)
        //
        //Now, to allow for horizontal movement, we want to set body's velocity a new Vector2, and registering the input for the X axis, the Horizontal one
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); //body's vertical velocity remains unchanged, we do not intend to alter it here

        //Check player's moving right (interpreted as horizontalInput greater than Zero)
        // ^ Which implies moving left is interpreted as horizontalInput less than Zero
        if (horizontalInput > 0.01f)
            //This will make the player face Right (--->)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            //This will make the palyer face Left (<---)
            transform.localScale = new Vector2(-1, 1);
        //EndOf IF/ELSE for 

        //Now, to check on the keys pressed, we add an IF-Statement which will trigger when the Spacebar key is pressed down ("KeyCode" is a convenient way to get all keyboard keys)
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump(); //Moved the code that used to be here and inserted it inside a new method called "Jump", which we updated
            //Thanks to this, the "Update" method will be a little bit more readable, and this piece of code easier to edit and expand
        }//EndOf IF

        //Set the Animator's parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }//EndOf method Update

    private void Jump()
    {
        //contrary than what we wanted before, now we only want to alter the Vertical speed. And since vertically the Player will start on a halt, we don't multiply (because it would equal speed*0)
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false; //While we're jumping, we're not on the ground
    }//EndOf method Jump

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") //This checks if we're colliding (touching) the ground object, given we gave it the "Ground" tag
            grounded = true; //If so, "grounded" boolean will be set to True
    }//EndOf Overwritten method OnCollisionEnter2D
}
