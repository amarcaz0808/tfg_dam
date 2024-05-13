using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestroy : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    private Animator animator;

    /**
     * METHODS
     **/
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ProjectilePlayer")
        {
            //TODO - Set Animator
            animator.SetTrigger("Break");
        }//EndOf IF
    }//EndOf method OnTriggerEnter2D

    private void Deactivate()
    {
        //This method IS called and used, just not between scripts but in Unity.
        gameObject.SetActive(false);
    }//EndOf method Destroy
}
