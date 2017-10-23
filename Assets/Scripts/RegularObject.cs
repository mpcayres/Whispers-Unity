using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularObject : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
