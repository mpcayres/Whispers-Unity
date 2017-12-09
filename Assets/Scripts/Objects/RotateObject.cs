using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;
    public float rotateSpeed = 10f;
    float lastPosX, lastPosY, difPosX, difPosY, somaDif = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        lastPosX = rigidBody.position.x;
        lastPosY = rigidBody.position.y;
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        difPosX = lastPosX - rigidBody.position.x;
        difPosY = lastPosY - rigidBody.position.y;
        if (difPosX != 0 || difPosY != 0)
        {
            somaDif += difPosX + difPosY;
            transform.rotation = Quaternion.Euler(0, 0, rotateSpeed*180*somaDif);
        }
        lastPosX = rigidBody.position.x;
        lastPosY = rigidBody.position.y;
    }
}
