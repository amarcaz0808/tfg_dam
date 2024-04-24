using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Manually added imports
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    /**
     * ATTRIBUTES / GLOBAL VARIABLES
     **/
    //Unity Stuff
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    /**
     * METHODS
     **/
    //Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 10; //Same as in Update, but this happens only once
    }//EndOf method Start

    // Update is called once per frame
    void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10; //Divided by 10 since the Healthbar works with decimal values between 0 and 1 (0% and 100%)
    }//EndOf method Update
}
