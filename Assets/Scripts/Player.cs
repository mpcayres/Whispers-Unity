using UnityEngine;

public class Player : MonoBehaviour {
    public float movespeed;
    Animator animator;
    Rigidbody2D rb;
    int oldDirection = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	void Update ()
    {
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
            rb.position = new Vector2(rb.position.x + move, rb.position.y);
            isWalking = true;
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.position = new Vector2(rb.position.x - move, rb.position.y);
            isWalking = true;
            direction = 1;
        }
		else if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.position = new Vector2(rb.position.x, rb.position.y + move);
            isWalking = true;
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.position = new Vector2(rb.position.x, rb.position.y - move);
            isWalking = true;
            direction = 3;
        }

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
