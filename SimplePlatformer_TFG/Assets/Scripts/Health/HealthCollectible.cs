using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    /* Primitive Variables */
    [SerializeField] private float healthValue;

    /**
     * METHODS
     **/
    /* Unity Methods */
    // Start is called before the first frame update
    void Start()
    {
        
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        
    }//EndOf method Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().RecoverHealth(healthValue);
            gameObject.SetActive(false);
        }//EndOf IF checking if what collisionned with Collectible was Player
    }//EndOf method OnTriggerEnter2D
}
