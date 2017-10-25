using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    float offset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        offset = GetComponent<BoxCollider2D>().offset.y 
            - player.GetComponent<SpriteRenderer>().size.y*player.transform.lossyScale.y/2;
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y + offset) * 100f) * -1;
    }
}
