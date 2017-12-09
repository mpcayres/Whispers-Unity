using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour {
    public int number;

    void OnTriggerEnter2D(Collider2D other)
    {
        Book.AddPage(number);
        Destroy(this.gameObject);
        
    }
}
