using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . UI;

public class TItleAnims : MonoBehaviour
{

    public Image title;

    public GameObject showAfter;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (disableTitle ());
    }

    IEnumerator disableTitle ( )
    {
        yield return new WaitForSeconds (5f);
      
        
            // loop over 1 second backwards
            for ( float i = 1 ; i >= 0 ; i -= Time . deltaTime)
            {
                // set color with i as alpha
                title . color = new Color (1 , 1 , 1 , i);
                yield return null;
            }
        showAfter . SetActive (true);
        
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
