using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine . SceneManagement;

public class ButtonDeath : MonoBehaviour
{

    public GameObject Instructionsl;
    public void End ( )
    {
        Application . Quit ();
    }


    public void COntinue()
    {
      
        SceneManager . LoadScene ("GameScene");
    }

    public void RestartAttempt()
    {
        GameManager . instance . ResetPlayer ();
        SceneManager . LoadScene ("GameScene");
    }


    public void help()
    {
        Instructionsl . SetActive(!Instructionsl.activeInHierarchy);
    }

    public void CREDITS()
    {

        Instructionsl . SetActive (!Instructionsl . activeInHierarchy);
    }

  
}
