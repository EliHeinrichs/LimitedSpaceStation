using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueRoom : MonoBehaviour
{


    public GameObject spaceShip;
    public GameObject chase;
    public GameObject rain;
    public RoomGenerator generatorScript;

    public void SpawnSpaceShip()
    {
        Instantiate (spaceShip , spaceShip . transform .position, Quaternion . identity);
    }

    public void SpawnChase ( )
    {
        Instantiate (chase, chase. transform . position , Quaternion . identity);
      

    }

    public void RainStorm ( )
    {
        Instantiate (rain , rain . transform . position , Quaternion . identity);
    }














    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
