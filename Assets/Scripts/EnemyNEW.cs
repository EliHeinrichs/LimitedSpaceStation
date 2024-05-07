using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNEW : MonoBehaviour
{
    public Transform target; // The player's transform to follow
    public float speed = 3f; // AI movement speed
    public float attackInterval = 2.0f; // time between attacks
    public float attackRange = 1.0f; // range of attack



    private float lastAttackTime; // time of last attack
    public Animator animator;

    private Rigidbody2D rb;

    public AudioClip atkSFX;

    private void Start ( )
    {
      
        speed = Random . Range (1.1f , 2f);
        attackInterval = Random . Range (0.3f , 2f);
        StartCoroutine (startSpeed (speed));
        rb = GetComponent<Rigidbody2D> ();
        if ( target == null )
        {
            // Find the player GameObject if not assigned
            target = GameObject . FindGameObjectWithTag ("Player") . transform;
        }
    }

    IEnumerator startSpeed ( float ms)
    {
        speed = speed / 5;
        yield return new WaitForSeconds (2.4f);
        speed = ms;
    }

    private void Update ( )
    {

 
     
        if ( target == null )
        {
            animator . SetBool ("Walking" , false);
            return; // If no player target is found, do nothing

        }
       
        // Calculate the direction from AI to the player
        Vector2 direction = target.position - transform.position;
        Attack (target . gameObject);

        animator . SetBool ("Walking" , true);
        // Normalize the direction vector (make its length 1)
        direction . Normalize ();

        // Move the AI towards the player
        rb . velocity = direction * speed;
    }
    public IEnumerator Hit ( GameObject enemy )
    {
        yield return new WaitForSeconds (0.3f);
        var enemyHealth = enemy.GetComponent<Player>();
        if ( enemyHealth != null )
        {
            SoundManager . Instance . PlaySound (atkSFX);
            enemyHealth . TakeDamage (Random . Range (1 , 12));
        }
    }
    public void Attack ( GameObject enemy )
    {

        // check if enough time has passed to attack again
        if ( Time . time - lastAttackTime >= attackInterval )
        {
         
            // check if enemy is in attack range
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= attackRange && GameManager . instance . health > 0)
            {
                animator . SetBool ("Walking" , false);
                animator . SetTrigger ("Attack");

                StartCoroutine (Hit (enemy));

                // update last attack time
                lastAttackTime = Time . time;
            }
        }
    }

}
