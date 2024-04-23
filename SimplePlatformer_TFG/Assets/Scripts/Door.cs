using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    /*
    //Unity Stuff
    [SerializeField] CameraController cam;

    //Primitive Variables
    [SerializeField] Transform previousRoom; //This one and nextRoom are dependant on which door we're in, so we should make this editable from Unity so each door leads to the correct collindant rooms
    [SerializeField] Transform nextRoom;
    */

    /**
     * METHODS
     **/
    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        
    }//EndOf method Update

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") //Triggers when Player touches the door, so its collision touches the door's
        {
            if (collision.transform.position.x < transform.position.x) //Player's horizontal position is less than the door's (Player coming from the Left)
                cam.MoveToNewRoom(nextRoom); //Move Camera to next room
            else //Player is coming from the right (Player's horizontal position is more than the door's)
                cam.MoveToNewRoom(previousRoom); //Move Camera to previous room
            //EndOf IF/ELSE checking all possible directions from which Player can interact with the door
        }///EndOf IF checking the Door's collision trigger equals to "Player"
    }//EndOf overwritten method OnTriggerEnter2D
    */
}
