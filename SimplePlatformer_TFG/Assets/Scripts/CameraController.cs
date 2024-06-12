using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL ATTRIBUTES
     **/

    /* ROOM CAMERA */
    /*
    //Unity stuff
    private Vector3 velocity = Vector3.zero;

    //Primitive variables
    [SerializeField] private float speed;
    private float currentPositionX;
    */

    /* PlayerTrack Camera */
    //Primitive Variables
    [SerializeField] private Transform player; //What player to track
    [SerializeField] private float aheadDistance; //How much the camera will move once Player stops
    [SerializeField] private float cameraSpeed; //How fast the camera will get to the aheadDistance

    private float lookAhead;

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
        /* ROOM CAMERA */
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, transform.position.y, transform.position.z), ref velocity, speed);

        /* PlayerTracker Camera */
        if (!Health.dead)
        {
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z); //Set the camera's position to that of the player's, plus the horizontal lookAhead
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), cameraSpeed * Time.deltaTime); //Smoothly get the camera from the player's position to thet aheadDistance
        }
    }//EndOf method Update

    /* ROOM CAMERA */
    /*
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPositionX = _newRoom.position.x;
    }//EndOf method MovetoNewRoom
    */
}
