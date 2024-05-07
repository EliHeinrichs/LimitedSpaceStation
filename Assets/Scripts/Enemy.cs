using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackInterval = 2.0f; // time between attacks
    public float attackRange = 1.0f; // range of attack



    private float lastAttackTime; // time of last attack

    public Animator animator;



    private void OnDrawGizmosSelected ( )
    {
        Gizmos . color = Color . red;
        Gizmos . DrawWireSphere (transform . position , attackRange);
    }

    private void Start ( )
    {

        moveDirection = Vector2 . right; // start moving to the right
        startspeed = Random . Range (0.4f , 2f);
        attackInterval = Random . Range (0.3f , 2f);
        moveSpeed = startspeed;
    }

    public void Attack ( GameObject enemy )
    {

        // check if enough time has passed to attack again
        if ( Time . time - lastAttackTime >= attackInterval )
        {
            animator . SetTrigger ("Attack");
            // check if enemy is in attack range
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= attackRange )
            {

                
                StartCoroutine (Hit (enemy));

                // update last attack time
                lastAttackTime = Time . time;
            }
        }
    }


    private void Update ( )
    {

        if( GameManager . instance . health > 0 )
        {
            Destroy (gameObject);
        }
        // find the nearest enemy with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        foreach ( GameObject enemy in enemies )
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= attackRange && distance < nearestDistance )
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        // attack nearest enemy
        if ( nearestEnemy != null )
        {
            Attack (nearestEnemy);

        }

        moveSpeed = startspeed;
        // find the nearest enemy with the tag "Enemy"
        foreach ( GameObject enemy in enemies )
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if ( distance <= detectionRange && distance < nearestDistance )
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        if ( nearestEnemy != null )
        {
          
            // move towards nearest enemy if not in attack range
            float distanceToEnemy = Vector2.Distance(transform.position, nearestEnemy.transform.position);
            if ( distanceToEnemy > minDistanceToEnemy )
            {
                moveDirection = ( nearestEnemy . transform . position - transform . position * moveSpeed ) . normalized;
                animator . SetBool ("Walking" , true);
            }
            else
            {
                // attack if in attack range
                
              
                   animator . SetBool ("Walking" , false);
                  

                
            }
        }
        else
        {
            // patrol randomly if no enemies detected
            moveDirection = Vector2 . zero;
            animator . SetBool ("Walking" , false);
        }
    }



    public IEnumerator Hit ( GameObject enemy )
    {
        yield return new WaitForSeconds (0.3f);
        var enemyHealth = enemy.GetComponent<Player>();
        if ( enemyHealth!= null )
        {

            enemyHealth . TakeDamage (Random . Range (3 , 15));
        }
    }

    public float moveSpeed = 5.0f; // speed of movement
    public float detectionRange = 10.0f; // range for detecting enemies
    public float minDistanceToEnemy = 2.0f; // minimum distance to enemy before attacking

    public float startspeed;
    private Vector2 moveDirection;



    


    private void FixedUpdate ( )
    {

        transform . Translate (moveDirection * moveSpeed * Time . deltaTime);
        // move in current direction
   


    }
}
