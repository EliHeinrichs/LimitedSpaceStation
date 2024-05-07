using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{

 
    public Transform checkpoint;   // Reference to the checkpoint's transform.
    public float moveSpeed = 5f;  // Speed at which the enemy moves.

    private void Update ( )
    {
        // Calculate the direction to move towards the checkpoint along the Y-axis.
        float directionToCheckpoint = Mathf.Sign(checkpoint.position.y - transform.position.y);

        // Move the enemy towards the checkpoint continuously along the Y-axis.
        transform . Translate (Vector2 . up * directionToCheckpoint * moveSpeed * Time . deltaTime);

    }



    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if(collision.tag == "Player")
        {
            collision . GetComponent<Player> () . TakeDamage (1000);
            Destroy (gameObject);
        }
    }

}
