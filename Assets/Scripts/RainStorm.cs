using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStorm : MonoBehaviour
{


    public float spawnSpeed = 0.3f;
    public GameObject dmgRain;
    // Start is called before the first frame update

    void Update()
    {

        spawnSpeed -= Time . deltaTime;
        if(spawnSpeed <= 0)
        {
            Instantiate (dmgRain , new Vector3 (Random . Range (0 , 9) , Random . Range (0, 9) , 0) , Quaternion . identity);
            Instantiate (dmgRain , new Vector3 (Random . Range (0 , 9) , Random . Range (0 , 9) , 0) , Quaternion . identity);
            Instantiate (dmgRain , new Vector3 (Random . Range (0 , 9) , Random . Range (0 , 9) , 0) , Quaternion . identity);
            spawnSpeed = 0.4f;
        }
 
    }



}
