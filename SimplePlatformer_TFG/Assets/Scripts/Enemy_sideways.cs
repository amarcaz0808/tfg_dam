using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sideways : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity Stuff

    //Primitive Variables
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    /**
     * METHODS
     **/
    /*Unity Methods*/
    private void Awake()
    {
        //Set the Left and Right limits to where the saw will be able to move
        leftEdge = transform.position.x - movementDistance; //How far can the Enemy move to said distance, take Enemy's og position and subtract how much we want it to move
        rightEdge = transform.position.x + movementDistance; // ^ Same but add, not subtract
    }//EndOf method Awake

    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, 
                    transform.position.y, 
                    transform.position.z); //Decrease horizontal position, gradually, in order to move to the Left
            }
            else
            {
                movingLeft = false;
            }//EndOf inner IF/ELSE for when Enemy is moving Left
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z); //Increase horizontal position, gradually, in order to move to the Right
            }
            else
            {
                movingLeft = true;
            }//EndOf inner IF/ELSE for when Enemy is moving Right
        }//EndOf outer IF/ELSE checking in which direction the Enemy is moving
    }//EndOf method Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }//EndOf IF checking that what stepped into the Enemy was Player
    }//EndOf method OnTriggerEnter2D
}
