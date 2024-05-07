using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource effectsSource, musicSource;


    public AudioClip Music1;
    public AudioClip Music2;
    public AudioClip Music3;
    public AudioClip final;
    public AudioClip chase;
    private bool finalBool;
    private void Awake ( )
    {
        
        if ( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad (gameObject);

        }
        else
        {
            Destroy (gameObject);
        }
    }

    private void Start ( )
    {
 

    }

    private void Update ( )
    {

        if( GameManager . instance . finalLvl <= GameManager . instance . room )
        {
            finalBool = true;
        }
        else
        {
            finalBool = false;
        }
        if ( GameManager . instance . room <= 9 && finalBool != true )
        {
            ChangeMusic (Music1);
        }

        if ( GameManager . instance . room % 10 == 0 && finalBool != true )
        {
            ChangeMusic (chase);
        }
        if ( GameManager . instance . room >= 11 && GameManager . instance . room < 20 && finalBool != true )
        {
            ChangeMusic (Music2);
        }
        if ( GameManager . instance . room >= 21 && finalBool != true && GameManager . instance. room % 10 != 0 )
        {
            ChangeMusic (Music3);
        }
       

        if (finalBool == true)
        {
            ChangeMusic (final);
        }

    }

    public void PlaySound ( AudioClip clip )
    {
       
        effectsSource . PlayOneShot (clip);
       
     
    }

    public void ChangeMusic ( AudioClip music )
    {
        if(musicSource.clip == music )
        {
            return;
        }
        else
        {
            musicSource . clip = music;
            musicSource . Play ();
            musicSource . loop = true;
        }
      
    }

    
}
