using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public enum Actions { DEFAULT, MOVING_OBJECT };
    public Actions playerState;
    public float movespeed;
    public Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public int direction = 0, wantedDirection = 0, oldDirection; //0 = east, 1 = west, 2 = north, 3 = south

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        bool isWalking = false, isRunning = false;
        float move = movespeed;

        //Ordem do layer determinada pelo eixo y
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

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
            //else isWalking = false;
        }
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        if(playerState == Actions.MOVING_OBJECT)
        {
            wantedDirection = direction;
            direction = oldDirection;
        }
        animator.SetInteger("direction", direction);
        if (oldDirection != direction)
        {
            animator.SetTrigger("changeDirection");
            oldDirection = direction;
        }
    }

	private void OnTriggerEnter2D (Collider2D other)
	{

		switch (other.tag) {
		case "DoorToLivingRoom":
			SceneManager.LoadScene (0);
			break;
		case "DoorToAlley":
			SceneManager.LoadScene (1);
			break;

		case "DoorToGarden":
			SceneManager.LoadScene (2);
			break;

		case "DoorToKitchen":
			SceneManager.LoadScene (3);
			break;


		case "DoorToMomRoom":
			SceneManager.LoadScene (4);
			break;

		case "DoorToKidRoom":
			SceneManager.LoadScene (5);
			break;
		
		default:
			SceneManager.LoadScene (0);
			break;

		}

	}

}
