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
    private BoxCollider2D boxCollider;
    private AudioSource audioSrc;

    //Primitive stuff
    //Serializing fields (attributes) make them editable in Unity directly, handy for testing purposes
    [SerializeField] private float speed; //speed value to alter the Player's speed, Serialized so it's editable from Unity
    [SerializeField] private float jumpPower; //speed value to alter the Player's jump power
    [SerializeField] private LayerMask groundLayer; //Another layer for the ground, it'll help us with all entity's physics and collisions
    [SerializeField] private LayerMask wallLayer; //Same as groundLayer, just checking for wall contact instead of the floor
    [SerializeField] private AudioClip jumpSFX;

    private float wallJumpCooldown; //This will help us control how much the player will need to wait until making a walljump, as to avoid making it indefinitely
    private float defaultGravityScale; //To have the original gravityScale always in handy, later initialized as Player's RigidBody2D's GravityScale
    private float horizontalInput;

    //private bool grounded;

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
        boxCollider = GetComponent<BoxCollider2D>();

        defaultGravityScale = body.gravityScale; //Tadaaa!!!
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        /**
         * Both the "Awake" and "Start" methods are called only ONCE when the game starts/loads
         * Therefore, in this method we shall program everything we want to be able to happen while the game is running
         * Things like, player movement, health checks, overworld changes, characters talking to each other, etc.
         **/
        
        if (!Trophy.hasWon)
        {
            //Make later calls to the horizontal input easier
            horizontalInput = Input.GetAxis("Horizontal");

            //Check player's moving right (interpreted as horizontalInput greater than Zero)
            // ^ Which implies moving left is interpreted as horizontalInput less than Zero
            if (horizontalInput > 0.01f)
                //This will make the player face Right (--->)
                transform.localScale = Vector2.one;
            else if (horizontalInput < -0.01f)
                //This will make the palyer face Left (<---)
                transform.localScale = new Vector3(-1, 1, 1);
            //EndOf IF/ELSE for where the player faces

            //Set the Animator's parameters
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("grounded", isGrounded());

            //Manage the walljump mechanic
            if (wallJumpCooldown > 0.2f)
            {
                //A Vector2 is basically what allows us to move an object in a 2-axis way, horizontal (x) and vertical (y)
                //There exists a Vector3 and a Vector4, but those are for other kinds of games (3D, of course for the added axis of depth, and 4D, which is WAY out of our league right now)
                //
                //Now, to allow for horizontal movement, we want to set body's velocity a new Vector2, and registering the input for the X axis, the Horizontal one
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); //body's vertical velocity remains unchanged, we do not intend to alter it here

                if (onWall() && !isGrounded())
                {
                    body.gravityScale = 0f; //If on wall AND jumping, deactivate gravity, so the player does not fall as quick (or at all) when pressing against a wall
                    body.velocity = Vector2.zero; //Reset the player's horizontal velocity to zero
                }
                else
                {
                    body.gravityScale = defaultGravityScale; //Return to the original gravity scale prior to touching the wall
                }//EndOf IF/ELSE checking the player is against a wall AND jumping

                //Now, to check on the keys pressed, we add an IF-Statement which will trigger when the Spacebar key is pressed down ("KeyCode" is a convenient way to get all keyboard keys)
                if (Input.GetKey(KeyCode.Space))
                {
                    Jump(); //Moved the code that used to be here and inserted it inside a new method called "Jump", which we updated
                            //Thanks to this, the "Update" method will be a little bit more readable, and this piece of code easier to edit and expand
                }//EndOf IF
            }
            else
            {
                wallJumpCooldown += Time.deltaTime;
            }//EndOf IF/ELSE for walljump
        }
    }//EndOf method Update

    private void Jump()
    {
        if(isGrounded())
        {
            //contrary than what we wanted before, now we only want to alter the Vertical speed. And since vertically the Player will start on a halt, we don't multiply (because it would equal speed*0)
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
            //Old Code //grounded = false; //While we're jumping, we're not on the ground
            audioSrc.clip = jumpSFX;
            audioSrc.Play();
        }else if (onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                //IF the player is touching the wall, but not moving horizontally, they'll just get pushed off the wall
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 8, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                // ^ Negative horizontal speed than where the player is facing, multiplied by the force to which they'll push away from it
                // ^ Lastly, add the vertical force to which Player will be pushed upwards when walljumping
            }
            else
            {
                //IF the player is touching the wall AND moving horizontally, they'll get pushed off the wall AND upwards
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                // ^ Negative horizontal speed than where the player is facing, multiplied by the force to which they'll push away from it
                // ^ Lastly, add the vertical force to which Player will be pushed upwards when walljumping
            }//EndOf IF/ELSE

            wallJumpCooldown = 0f;
        }//EndOf IF/ELSE checking jumping conditions
    }//EndOf method Jump

    private bool isGrounded()
    {
        /*
         * In order to check if the player is grounded or not, we cast a ray from the player's feet and straight downwards
         * For this specific case we use a BoxCast, matching the position and size of the player
         * Also, we need to specify the ray's angle, direction, how long it'll go from the player and what layer it is gonna be in (the ground's, in this latter case)
         * The layer system works like overlapping papers: Together they make a single image, but each is on a different layer (paper)
         * ^ (Think of the scene in the first Iron Man movie in which Tony Stark shows that guy in the cave what he's planning to do with the missile' scraps)
         */
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null; //IF hitting the ground, collider with equal NULL, thus returning FALSE
    }//EndOf private bool-returning method isGrounded

    private bool onWall()
    {
        //Most parameters of this raycast remain the same as the ground's, we just add a whole new vector that only grows horizontally as the direction for the ray itself
        //Quite smart if you ask me, we don't want that ray to just go Right of Left
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }//EndOf private bool-returning method onWall

    public bool canAttack()
    {
        //Say we want to be able to attack whie the player is not moving, on the ground and not on a wall
        return !onWall();
    }//EndOf public bool-returning method canAttack
}
