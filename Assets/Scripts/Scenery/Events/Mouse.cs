using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Animation animationMouse { get { return GetComponent<Animation>(); } }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            animationMouse.Play("Mouse");
        }
    }
}
