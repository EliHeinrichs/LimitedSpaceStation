using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTImer : MonoBehaviour
{

    private void Start ( )
    {
        StartCoroutine (death ());
    }

    IEnumerator death ( )
    {
        yield return new WaitForSeconds (2f);
        Destroy (gameObject);
    }
}
