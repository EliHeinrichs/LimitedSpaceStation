using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour
{
    // When a collider enters this object collider and one is a trigger this func triggers and references the other Collider2D
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        //if the other colliders object has the Player tag
        if ( collision . tag == "Player" )
        {
            // Find the Player script on the other colliders gameObject and perform the pickup func
            collision . GetComponent<Player> () . Pickup ();
            //Add tp the battery total of the Gamemanagers single instance 
            GameManager . instance . batteryTotal += 1;
            //Destroy the pickup object
            Destroy (gameObject);
        }
    }
}
