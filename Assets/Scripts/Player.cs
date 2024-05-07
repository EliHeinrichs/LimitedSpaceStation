using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;
using UnityEngine . UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;
    private Vector2 moveDirection;

    public bool facingRight;
    public GameObject cam1;

    public GameObject cam2;

    public float immuneTIme;
    public Text hpText;
    public Text timeText;
    public Slider timeSlider;
    public Slider fuelSlider;
    public Text roomNum;
    public Text batteryAmt;

    public GameObject error;
    private bool errorStarted;


    public AudioClip deathSFX;
    public AudioClip damageSFX;
    public AudioClip pickup;
 

    private void Start ( )
    {
     
        immuneTIme = 1.4f;
        timeSlider . maxValue = 100;
        fuelSlider . maxValue = GameManager . instance . maxHealth;
        
       
        
        animator . SetTrigger ("Start");
        StartCoroutine (camOne (moveSpeed,0.8f));
        cam1 = GameObject . Find ("Camera");

    }

    IEnumerator camOne(float ms, float time)
    {

 
       
        moveSpeed = 0;

        yield return new WaitForSeconds (time);
      
        moveSpeed = ms; 

    }

    public void Pickup()
    {

        SoundManager . Instance . PlaySound (pickup);
        animator . SetTrigger ("Pickup");

    }    
    void Update ( )
    {
        roomNum . text = "Room " + GameManager . instance . room . ToString ();
        hpText . text = GameManager . instance . health . ToString () + "%";
        timeText . text = GameManager . instance . timerOxy . ToString ("F0") + "%";
        immuneTIme -= Time . deltaTime;
        timeSlider . value = GameManager . instance . timerOxy;
        fuelSlider . value = GameManager . instance . health;
        


      
        if ( GameManager . instance . health <= 10 || GameManager . instance . timerOxy <= 15)
        {
            if(errorStarted != true )
            {
                StartCoroutine (errorFlash ());
            }
        }
  
        if(GameManager.instance.batteryTotal >= 15)
        {
         
            batteryAmt . text = "Find Ship!";
        }
        else
        {
            batteryAmt . text = GameManager . instance . batteryTotal . ToString () + "/15";
        }
  
        
        ProcessInputs ();


        if ( GameManager.instance.timerOxy <= 0 )
        {
            animator . SetBool ("Dead" , true);
            StartCoroutine (dead ());
        }


    }

    IEnumerator errorFlash ( )
    {
        errorStarted = true;
        error . SetActive (true);

        yield return new WaitForSeconds (5f);
       
        error . SetActive (false);

        yield return new WaitForSeconds (15f);
        errorStarted = false;
    }

    public void TakeDamage(int amount)
    {

        if(immuneTIme <= 0)
        {
            GameManager . instance . health -= amount;
            SoundManager . Instance . PlaySound (damageSFX);
            animator . SetTrigger ("Damage");
            immuneTIme = 0.5f;
        }
        
        if( GameManager . instance . health <= 0)
        {
            animator . SetBool ("Dead",true);
            StartCoroutine (dead ());
            return;
        }
       
        return;
    }

    public IEnumerator dead()
    {

        cam1 . SetActive (false);
        SoundManager . Instance . PlaySound (deathSFX);
        cam2 . SetActive (true);
        moveSpeed = 0;

        yield return new WaitForSeconds (5.5f);
        GameManager . instance . ResetPlayer ();
        SceneManager . LoadScene ("Death");
       
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if(moveX != 0)
        {
          
            animator . SetBool ("Walking" , true);
        }
        else if(moveY != 0)
        {
         
            animator . SetBool ("Walking" , true);
        }
        else
        {
            animator . SetBool ("Walking", false);
        }

        if(moveX < 0 && facingRight || moveX > 0 && !facingRight)
        {
            Flip ();
        }
      


        moveDirection = new Vector2 (moveX , moveY).normalized;
    }



    void Flip()
    {
        gameObject . GetComponent<SpriteRenderer> () . flipX = !gameObject . GetComponent<SpriteRenderer> () . flipX;
        facingRight = !facingRight;
        
    }
  
    private void Move ( )
    {

        rb . velocity = new Vector2 (moveDirection . x * moveSpeed , moveDirection . y * moveSpeed);
    }
    void FixedUpdate ( )
    {
        Move ();
      

    }




}
