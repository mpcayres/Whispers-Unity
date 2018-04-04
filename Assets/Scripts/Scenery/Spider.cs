using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour {
    public Animation animationSpider { get { return GetComponent<Animation>(); } }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            animationSpider.Play("Spider");
        }
    }
}
