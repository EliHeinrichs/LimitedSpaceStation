using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class GameManager : MonoBehaviour
{

    public int room = 0;
    public RoomGenerator generatorScript;
    public UniqueRoom roomScript;
    public static GameManager instance = null; // static instance of gameManager so that any script can access it
    public float restartLevelDelay;
    public int batteryTotal;
    public int health;
    public int maxHealth;
    public float timerOxy = 100f;
    //public GameObject player;
    public int finalLvl = 30;
    public int roomEffect;
    public GameObject darkness;
    public bool uniqueRoom;
    public int rainRooms;

    public AudioClip doorOpen;
    

   
    void Awake ( )
    {
        if ( instance == null )// if there is no gamemanager instance set this script as the instance GameManager
        {
            instance = this;
        }
        else if ( instance != this )// if the instance already exists and isnt this then destroy that instance, assuuring us that theres only one gameManager
        {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);

        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        if(sceneName == "End" || sceneName == "Death" )
        {
            ResetPlayer ();
        }
    
      
      

        generatorScript = GetComponent<RoomGenerator> ();
        roomEffect = Random . Range (1 , 4);
        rainRooms = Random . Range (1 , 6);
        InitGame ();
        health = maxHealth;
        

        instance . uniqueRoom = false;
    }


    // Start is called before the first frame update
    void Start( )
    {

      

    }


    public void ResetPlayer()
    {
        health = maxHealth;
        timerOxy = 90f;
        room = 0;
        batteryTotal = 0;
        
        finalLvl = 100;
        
    }
    private void Restart ( )
    {
        SceneManager .LoadScene("GameScene");
        
    }

    

    void InitGame()
    {
   


        if(rainRooms == 1 && uniqueRoom != true && instance.room < instance. finalLvl &&  instance . room % 10 != 0 )
        {
            instance . uniqueRoom = true;
            roomScript . RainStorm ();
        }
      
        if ( instance . room >= instance . finalLvl )
        {
           instance.uniqueRoom = true;
            roomScript . SpawnSpaceShip ();
        }

       
        if ( instance.room % 10 == 0 && instance . uniqueRoom != true && instance . room != instance . finalLvl && instance.room != 0)
        {
            instance.uniqueRoom = true;
            roomScript . SpawnChase ();
            


        }

        generatorScript . SetUpScene (room);

      
     


        switch ( roomEffect )
        {

            case 1:
                if(instance.uniqueRoom != true && instance.room < instance.finalLvl)
                {
                    Instantiate (darkness , darkness . transform . position , Quaternion . identity);
                }

                break;
            case 2:
   
                break;
            case 3:
                if ( instance . uniqueRoom != true && instance . room < instance . finalLvl )
                {
                    Instantiate (darkness , darkness . transform . position , Quaternion . identity);
                }
                break;
            case 4:
          
                break;
        }




    }

    public void NextRoom()
    {
        room += 1;
        if ( instance . batteryTotal >= 15 && instance . room >= 20 )
        {
            instance . finalLvl = Random . Range (instance . room , instance . room + 5);

        }
        Invoke ("Restart" , restartLevelDelay);
        SoundManager.Instance.PlaySound(doorOpen);
      

    }

    // Update is called once per frame
    void Update()
    {
     
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application . Quit ();
        }
        timerOxy -= Time . deltaTime;


        if ( instance.timerOxy >= 90 )
        {
            instance.timerOxy = 90;
        }

        if ( instance.health > 100 )
        {
            instance . health = 100;
        }


    }

}
