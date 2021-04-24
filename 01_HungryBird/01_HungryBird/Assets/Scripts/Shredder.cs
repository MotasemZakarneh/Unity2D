using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    //Carrot
    void OnTriggerEnter2D(Collider2D other)
    {
        Carrot c = other.GetComponent<Carrot>();
        if(c != null)
        {
            c.DestroyCarrot();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
