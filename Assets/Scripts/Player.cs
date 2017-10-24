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
        if (!MissionManager.instance.paused)
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
            if (playerState == Actions.MOVING_OBJECT)
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
    }

	private void OnTriggerEnter2D (Collider2D other)
	{
        int index = 0;
        MissionManager.instance.paused = true;

		switch (other.tag) {
			case "DoorToLivingroom":
				SceneManager.LoadScene (1);
				index = 1;
			    break;
		    case "DoorToAlley":
			    SceneManager.LoadScene(2);
                index = 2;
			    break;
		    case "DoorToGarden":
			    SceneManager.LoadScene(3);
                index = 3;
			    break;
		    case "DoorToKitchen":
			    SceneManager.LoadScene(4);
                index = 4;
			    break;
		    case "DoorToMomRoom":
			    SceneManager.LoadScene(5);
                index = 5;
			    break;
		    case "DoorToKidRoom":
			    SceneManager.LoadScene(6);
                index = 6;
			    break;
		    default:
			    SceneManager.LoadScene(0);
			    break;
		}

    }

    public void ChangeDirection(int newDirection)
    {
        print("CHANGE" + newDirection);
        direction = newDirection;
        animator.SetInteger("direction", direction);
        animator.SetTrigger("changeDirection");
        oldDirection = direction;
    }

    public void ChangePosition()
    {
        playerState = Actions.DEFAULT;
        if (!MissionManager.instance.currentSceneName.Equals(MissionManager.instance.previousSceneName))
        {
            if (MissionManager.instance.currentSceneName.Equals("Corredor"))
            {
                if (MissionManager.instance.previousSceneName.Equals("Sala"))
                {
                    rb.position = new Vector2((float)-9.8, (float)-0.6);
                }
                else if (MissionManager.instance.previousSceneName.Equals("QuartoMae"))
                {
                    rb.position = new Vector2((float)-1.6, (float)-0.3);
                    ChangeDirection(3);
                }
                else if (MissionManager.instance.previousSceneName.Equals("Cozinha"))
                {
                    rb.position = new Vector2((float)2.95, (float)-0.6);
                }
                else if (MissionManager.instance.previousSceneName.Equals("QuartoKid"))
                {
                    rb.position = new Vector2((float)11.9, (float)-0.3);
                    ChangeDirection(3);
                }
                else
                {
                    //rb.position = new Vector2((float)10, (float)-0.45);
                }
            }
            else if (MissionManager.instance.currentSceneName.Equals("Cozinha"))
            {
                rb.position = new Vector2((float)1.5, (float)0.7);
            }
            else if (MissionManager.instance.currentSceneName.Equals("Jardim"))
            {
                rb.position = new Vector2((float)3.25, (float)2.3);
            }
            else if (MissionManager.instance.currentSceneName.Equals("QuartoKid"))
            {
                rb.position = new Vector2((float)1.75, (float)0.65);
                ChangeDirection(3);
            }
            else if (MissionManager.instance.currentSceneName.Equals("QuartoMae"))
            {
                rb.position = new Vector2((float)-3.8, (float)-0.45);
                ChangeDirection(3);
            }
            else if (MissionManager.instance.currentSceneName.Equals("Sala"))
            {
                if (MissionManager.instance.previousSceneName.Equals("Corredor"))
                {
                    rb.position = new Vector2((float)-3.15, (float)0.85);
                }
                else if (MissionManager.instance.previousSceneName.Equals("Jardim"))
                {
                    rb.position = new Vector2((float)2.35, (float)-2.0);
                }
                else
                {
                    //rb.position = new Vector2((float)2.65, (float)-1.15);
                }
            }
            MissionManager.instance.paused = false;
        }
    }

}
