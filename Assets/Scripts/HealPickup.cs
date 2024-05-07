using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if ( collision . tag == "Player" )
        {
            collision . GetComponent<Player> () . Pickup ();
            GameManager . instance . timerOxy += Random.Range(3,12);
            Destroy (gameObject);
        }
    }
}
