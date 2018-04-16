using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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
    
    public AudioClip steps, stepsGrass;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    float stepsControl = 0.5f;

    void Start ()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source.playOnAwake = false;
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
                if (CrossPlatformInputManager.GetButton("keyRun"))
                {
                    move = runningFactor * movespeed;
                    isRunning = true;
                }
                if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
                {
                    rb.position = new Vector2(rb.position.x + move, rb.position.y);
                    isWalking = true;
                    direction = 0;
                }
                else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
                {
                    rb.position = new Vector2(rb.position.x - move, rb.position.y);
                    isWalking = true;
                    direction = 1;
                }
                else if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                {
                    rb.position = new Vector2(rb.position.x, rb.position.y + move);
                    isWalking = true;
                    direction = 2;
                }
                else if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
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

                //barulho dos passos
                if (MissionManager.instance.currentSceneName.Equals("Jardim"))
                {
                    source.clip = stepsGrass;
                    stepsControl = 0.1f;
                }
                else
                {
                    source.clip = steps;
                    stepsControl = 0.2f;
                }
                if ( isWalking && !source.isPlaying)
                {
                    source.PlayDelayed(stepsControl*2);
                }
                else if (isRunning && !source.isPlaying)
                {
                    source.PlayDelayed(stepsControl);
                }
                else if (!(isWalking || isRunning))
                {
                    source.Stop();
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButton("keySpecial"))
                {
                    if (CrossPlatformInputManager.GetButtonDown("keyMove"))
                    {
                        auxOnObject.MoveUp();
                    }
                }
                if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
                {
                    direction = 0;
                }
                else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
                {
                    direction = 1;
                }
                else if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                {
                    direction = 2;
                }
                else if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
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

    public void ChangeDirection(int newDirection)
    {
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
                if (previousSceneName.Equals("Porao"))
                {
                    rb.position = new Vector2((float)6.0, (float)2.5);
                    ChangeDirection(3);
                }
                else
                {
                    rb.position = new Vector2((float)3.25, (float)2.3);
                }
            }
            else if (MissionManager.instance.currentSceneName.Equals("Porao"))
            {
                rb.position = new Vector2((float)3.2, (float)0.5);
                ChangeDirection(3);
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
            else if (MissionManager.instance.currentSceneName.Equals("Banheiro"))
            {
                rb.position = new Vector2((float)2.171, (float)0.284);
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
            else if (MissionManager.instance.currentSceneName.Equals("SideQuest"))
            {
                rb.position = new Vector2(MissionManager.instance.sideQuest.sideX, MissionManager.instance.sideQuest.sideY);
                ChangeDirection(MissionManager.instance.sideQuest.sideDir);
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
            if (rb == null) rb = GetComponent<Rigidbody2D>();
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
            Corvo.instance.LookAtPlayer();
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
        StartCoroutine(WaitCoroutineUpDown(2, x, y, dir, false));
    }

    public void MoveDownAnimation(string anim, float x, float y, int dir)
    {
        animator.Play(anim);
        StartCoroutine(WaitCoroutineUpDown(1, x, y, dir, true));
    }

    public void PlayAnimation(string anim, float time = 1f)
    {
        print("2:" + anim + time);
        animator.Play(anim);
        StartCoroutine(WaitCoroutineAnim(time));
    }

    IEnumerator WaitCoroutineUpDown(float time, float x, float y, int dir, bool down)
    {
        Debug.Log("about to yield return WaitForSeconds("+ time + ")");
        yield return new WaitForSeconds(time);
        Debug.Log("Animation ended");
        ChangePositionDefault(x, y, dir);
        if (down)
        {
            playerState = Actions.DEFAULT;
            auxOnObject.GetComponent<Collider2D>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
        yield break;
        //Debug.Log("You'll never see this"); // produces a dead code warning
    }

    IEnumerator WaitCoroutineAnim(float time)
    {
        Debug.Log("about to yield return WaitForSeconds(" + time + ")");
        yield return new WaitForSeconds(time);
        Debug.Log("Animation ended");
        animator.SetTrigger("changeDirection");
        yield break;
        //Debug.Log("You'll never see this"); // produces a dead code warning
    }

}
