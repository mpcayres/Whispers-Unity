using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    float top, btm, left, right;
    public float movespeed;
    public Animator animator;
    int oldDirection = 0;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        GameObject background = GameObject.FindGameObjectWithTag("Background");
        BoxCollider2D collider = (BoxCollider2D) background.GetComponent<Collider2D>();

        top = background.transform.position.y + collider.offset.y + background.transform.lossyScale.y * (collider.size.y / 2f);
        btm = background.transform.position.y + collider.offset.y - background.transform.lossyScale.y * (collider.size.y / 2f);
        left = background.transform.position.x + collider.offset.x - background.transform.lossyScale.x * (collider.size.x / 2f);
        right = background.transform.position.x + collider.offset.x + background.transform.lossyScale.x * (collider.size.x / 2f);
    }
	
	// Update is called once per frame
	void Update () {
        int direction = oldDirection; //0 = east, 1 = west, 2 = north, 3 = south
        bool isWalking = false, isRunning = false;
        float move = movespeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = 5 * movespeed;
            isRunning = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(transform.position.x + move, transform.position.y);
            isWalking = true;
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - move, transform.position.y);
            isWalking = true;
            direction = 1;
        }
		else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + move);
            isWalking = true;
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - move);
            isWalking = true;
            direction = 3;
        }

        if(transform.position.x < left)
            transform.position = new Vector2(left, transform.position.y);
        else if (transform.position.x > right)
            transform.position = new Vector2(right, transform.position.y);

        if (transform.position.y < btm)
            transform.position = new Vector2(transform.position.x, btm);
        else if (transform.position.y > top)
            transform.position = new Vector2(transform.position.x, top);

        if (isRunning)
        {
            if (!isWalking) isRunning = false;
            else isWalking = false;
        }
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetInteger("direction", direction);
        if (oldDirection != direction)
        {
            animator.SetTrigger("changeDirection");
            oldDirection = direction;
        }

    }
}
