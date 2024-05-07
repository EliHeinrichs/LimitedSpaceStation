using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;
public class SpaceShip : MonoBehaviour
{



    public Transform stationTransform; //transform for the space shuttle
    public Transform movePointStation; // transform where the shuttle will go

    public Animator animator; // animator for the suttle
    public int clickAmt; // int to track how many times the player clicks for the quicktime event
    private bool survived; // bool to determine if theplayer survives the quicktime event
    public bool movestationTime; // bool that states when the station is to animate 
    public bool eventActive; // bool for when the quicktime event is active 
    public GameObject display; 

    public AudioClip door; // audio that plays when entering shuttle
    public AudioClip escape; // audio for when the player escapes and wins
    public AudioClip destroyed; // audio when the player is destroyed in shuttle

    private void Start ( )
    {

        stationTransform = GameObject . Find ("Board") . transform; // finds the board transform
    }

    // Event triggers when Collider with trigger enters 
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        // if the colliders object contains the tag Player then the player object and movement script is destroyed and the end sequence animation begins
        if ( collision . tag == "Player" )
        {
            animator . SetTrigger ("Door");
            // The sound manager instance plays the door sound
            SoundManager . Instance . PlaySound (door);
            Destroy (collision.gameObject);
        }
    }

    private void Update ( )
    {
       
        if(movestationTime == true)
        {
            //moves the station off the screen towards the new vector 
            stationTransform. transform . position = Vector3 . MoveTowards (stationTransform . transform . position ,new Vector2(-40,-40), 7 * Time . deltaTime);
           
            StartCoroutine (WaitForLeaveFrame ());
        }
        

        // If the quicktime event is active, start the coroutine to spawn the E key
        if(eventActive== true)
        {
            StartCoroutine (SpamE ());
            Debug . Log (clickAmt);
        }

        
    }


    IEnumerator SpamE()
    {
        // If the e key is pressed and the eventActive is still true, then add to the total click amount
        if(Input.GetKeyDown(KeyCode.E) && eventActive == true)
        {
            clickAmt += 1;
        }
        // show the UI visual that displays to the player that they must spam the E key
        display . SetActive (true);

        // Yield on new instruction that lasts for 6 seconds
        yield return new WaitForSeconds (6f);
        //stop the quicktime event and end all addition clicks counted and disable the UI visual for pressing E
        eventActive = false;
        display . SetActive (false);
        //If the player clicks E over 27 times in the eventTime period set survived to true
        if(clickAmt >= 27)
        {
            survived = true;
        }
        // proceed to the end game
        endGame ();


    }


    void endGame()
    {
        // if the player survives play the survive animation and start the Escape Coroutine
        if(survived == true)
        {
            animator . SetBool ("Live" , true);
            StartCoroutine (escaped ());
        }
        else
        {
            // if the player failed to escape play the destruction animation and start the Coroutine for lost in space
            animator . SetBool ("Live" , false);
            StartCoroutine (lostInSpace ());
            
        }

    }

    IEnumerator lostInSpace()
    {
        //Wait for the animation to finish and play the destroyed sound
        SoundManager . Instance . PlaySound (destroyed);
        yield return new WaitForSeconds (6f);
        //proceed to the death scene 
        SceneManager . LoadScene ("Death");
    }


    IEnumerator escaped ( )
    {
        // play the escape sound and wait 10 seconds for the animation to complete
        SoundManager . Instance . PlaySound (escape);
        yield return new WaitForSeconds (10f);
        //proceed to the End victory scene
        SceneManager . LoadScene ("End");
    }
    IEnumerator WaitForLeaveFrame()
    {
        //Waits for the station to leave the camera view and shows the void hand animation that attacks the ship 
        yield return new WaitForSeconds (5.5f);
        animator . SetTrigger ("Hand");

    }
}
