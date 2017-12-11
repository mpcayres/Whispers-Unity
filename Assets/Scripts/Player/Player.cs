using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {
    public enum Actions { DEFAULT, MOVING_OBJECT, ON_OBJECT };
    public Actions playerState;
    public float movespeed;
    public float runningFactor = 3f;
    public Animator animator;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    MovingObject auxOnObject;

    public int direction = 0, wantedDirection = 0;
    private string lastSceneGameOver = "";
    private float corvoPositionX, corvoPositionY;
    private string corvoScene;
    int oldDirection; //0 = east, 1 = west, 2 = north, 3 = south

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {

        if (!MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            bool isWalking = false, isRunning = false;
            float move = movespeed;

            //Ordem do layer determinada pelo eixo y
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

            if (playerState != Actions.ON_OBJECT)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    move = runningFactor * movespeed;
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
                }
                animator.SetBool("isWalking", isWalking);
                animator.SetBool("isRunning", isRunning);

                if (playerState == Actions.MOVING_OBJECT)
                {
                    wantedDirection = direction;
                    direction = oldDirection;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        auxOnObject.MoveUp();
                    }
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    direction = 0;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = 1;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    direction = 2;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    direction = 3;
                }
            }

            animator.SetInteger("direction", direction);
            if (oldDirection != direction)
            {
                animator.SetTrigger("changeDirection");
                oldDirection = direction;
            }

        }
        else if (oldDirection != -1)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetTrigger("changeDirection");
            oldDirection = -1;
        }
    }

	private void OnTriggerEnter2D (Collider2D other)
	{
        MissionManager.instance.paused = true;

		switch (other.tag) {
			case "DoorToLivingroom":
				SceneManager.LoadScene(1);
			    break;
		    case "DoorToAlley":
			    SceneManager.LoadScene(2);
			    break;
		    case "DoorToGarden":
			    SceneManager.LoadScene(3);
			    break;
		    case "DoorToKitchen":
			    SceneManager.LoadScene(4);
			    break;
		    case "DoorToMomRoom":
			    SceneManager.LoadScene(5);
			    break;
		    case "DoorToKidRoom":
			    SceneManager.LoadScene(6);
			    break;
		default:
			MissionManager.instance.paused = false;
			    break;
		}

    }

    public void ChangeDirection(int newDirection)
    {
        print("CHANGE" + newDirection);
        direction = newDirection;
        GetComponent<Animator>().SetInteger("direction", direction);
        GetComponent<Animator>().SetTrigger("changeDirection");
        oldDirection = direction;
    }

    public void ChangePositionDefault(float x, float y, int dir)
    {
        GetComponent<Rigidbody2D>().position = new Vector2(x, y);
        if (dir != -1) ChangeDirection(dir);
    }

    public void ChangePosition()
    {
        playerState = Actions.DEFAULT;

        string previousSceneName = "";
        if (MissionManager.instance.previousSceneName.Equals("GameOver"))
        {
            previousSceneName = lastSceneGameOver;
        }
        else if(!MissionManager.instance.currentSceneName.Equals("GameOver"))
        {
            lastSceneGameOver = MissionManager.instance.previousSceneName;
            previousSceneName = MissionManager.instance.previousSceneName;
        }

        if (!MissionManager.instance.currentSceneName.Equals(MissionManager.instance.previousSceneName))
        {
            if (MissionManager.instance.currentSceneName.Equals("Corredor"))
            {
                if (previousSceneName.Equals("Sala"))
                {
                    rb.position = new Vector2((float)-9.8, (float)-0.6);
                }
                else if (previousSceneName.Equals("QuartoMae"))
                {
                    rb.position = new Vector2((float)-1.6, (float)-0.3);
                    ChangeDirection(3);
                }
                else if (previousSceneName.Equals("Cozinha"))
                {
                    rb.position = new Vector2((float)2.95, (float)-0.6);
                }
                //else if (previousSceneName.Equals("QuartoKid"))
                else
                {
                    rb.position = new Vector2((float)11.9, (float)-0.3);
                    ChangeDirection(3);
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
                if (previousSceneName.Equals("Jardim"))
                {
                    rb.position = new Vector2((float)2.35, (float)-2.0);
                }
                //else if (previousSceneName.Equals("Corredor"))
                else
                {
                    rb.position = new Vector2((float)-3.15, (float)0.85);
                    ChangeDirection(3);
                }
            }
            else if (MissionManager.instance.currentSceneName.Equals("GameOver"))
            {
                //rb.position = new Vector2((float)-3.8, (float)-0.45);
            }
            MissionManager.instance.paused = false;
        }

        if (Cat.instance != null)
        {
            Cat.instance.ChangePosition(rb.position.x - 0.6f, rb.position.y - 0.3f);
        }

        if ((MissionManager.instance.mission is Mission8) &&
            !MissionManager.instance.previousSceneName.Equals("GameOver") &&
            !MissionManager.instance.currentSceneName.Equals("GameOver"))
        {
            corvoPositionX = rb.position.x;
            corvoPositionY = rb.position.y;
            corvoScene = MissionManager.instance.currentSceneName;
            if (Corvo.instance != null)
            {
                Corvo.instance.gameObject.SetActive(false);
                Invoke("ChangeCorvoPosition", 2f);
            }
        }
        
    }

    public void ChangeCorvoPosition()
    {
        if (MissionManager.instance.currentSceneName.Equals(corvoScene) && Corvo.instance != null)
        {
            Corvo.instance.ChangePosition(corvoPositionX, corvoPositionY);
            Corvo.instance.gameObject.SetActive(true);
        }
    }

    public float GetCorvoPositionX()
    {
        return corvoPositionX;
    }

    public float GetCorvoPositionY()
    {
        return corvoPositionY;
    }

    public void MoveUpAnimation(MovingObject aux, string anim, float x, float y, int dir)
    {
        auxOnObject = aux;
        animator.Play(anim);
        //animator.GetCurrentAnimatorStateInfo(0).length
        StartCoroutine(WaitCoroutine(2, x, y, dir, false));
    }

    public void MoveDownAnimation(string anim, float x, float y, int dir)
    {
        animator.Play(anim);
        StartCoroutine(WaitCoroutine(1, x, y, dir, true));
    }

    IEnumerator WaitCoroutine(float time, float x, float y, int dir, bool down)
    {
        Debug.Log("about to yield return WaitForSeconds("+ time + ")");
        yield return new WaitForSeconds(time);
        Debug.Log("Animation ended");
        ChangePositionDefault(x, y, dir);
        if (down)
        {
            playerState = Actions.DEFAULT;
            auxOnObject.GetComponent<Collider2D>().enabled = true;
        }
        yield break;
        Debug.Log("You'll never see this"); // produces a dead code warning
    }

}
