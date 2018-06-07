using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowNPCs;
using CrowShadowObjects;

namespace CrowShadowPlayer
{
    public class Player : MonoBehaviour
    {
        public enum Actions { DEFAULT, MOVING_OBJECT, ON_OBJECT };
        [Header("Conditions")]
        public Actions playerAction;
        public enum States { DEFAULT, FLASHLIGHT, PROTECTED_TAMPA, PROTECTED_ESCUDO };
        public States playerState;

        [Header("Variables")]
        public float movespeed = 0.01f;
        public float runningFactor = 3f;
        public float invertControlsTime = 0;

        [Header("Data")]
        public bool hidden = false;
        public bool isRunning = false;
        public int direction = 0, wantedDirection = 0;

        [Header("External Sources")]
        public Animator animator;
        public AudioClip steps, stepsGrass;
        public AudioSource source { get { return GetComponent<AudioSource>(); } }

        private float stepsControl = 0.5f;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private MovingObject auxOnObject;

        private string lastSceneGameOver = "", corvoScene = "";
        private float corvoPositionX, corvoPositionY;
        private int oldDirection; //0 = east, 1 = west, 2 = north, 3 = south

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            source.playOnAwake = false;
        }

        void Update()
        {
            if (invertControlsTime > 0)
            {
                invertControlsTime -= Time.deltaTime;
            }

            if (!GameManager.instance.paused && !GameManager.instance.blocked && !GameManager.instance.pausedObject)
            {
                bool isWalking = false;
                float move = movespeed;

                //Ordem do layer determinada pelo eixo y
                spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

                if (playerAction != Actions.ON_OBJECT)
                {
                    if (CrossPlatformInputManager.GetButton("keyRun"))
                    {
                        move = runningFactor * movespeed;
                        isRunning = true;
                    }
                    else
                    {
                        isRunning = false;
                    }

                    if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
                    {
                        if (invertControlsTime > 0)
                        {
                            transform.position = new Vector2(transform.position.x - move, transform.position.y);
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x + move, transform.position.y);
                        }
                        isWalking = true;
                        direction = 0;
                    }
                    else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
                    {
                        if (invertControlsTime > 0)
                        {
                            transform.position = new Vector2(transform.position.x + move, transform.position.y);
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x - move, transform.position.y);
                        }
                        isWalking = true;
                        direction = 1;
                    }
                    else if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
                    {
                        if (invertControlsTime > 0)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y - move);
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y + move);
                        }
                        isWalking = true;
                        direction = 2;
                    }
                    else if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
                    {
                        if (invertControlsTime > 0)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y + move);
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y - move);
                        }
                        isWalking = true;
                        direction = 3;
                    }

                    if (isRunning)
                    {
                        if (!isWalking) isRunning = false;
                    }
                    animator.SetBool("isWalking", isWalking);
                    animator.SetBool("isRunning", isRunning);

                    if (playerAction == Actions.MOVING_OBJECT)
                    {
                        wantedDirection = direction;
                        direction = oldDirection;
                    }

                    //barulho dos passos
                    if (GameManager.instance.currentSceneName.Equals("Jardim"))
                    {
                        source.clip = stepsGrass;
                        stepsControl = 0.1f;
                    }
                    else
                    {
                        source.clip = steps;
                        stepsControl = 0.2f;
                    }
                    if (isWalking && !source.isPlaying)
                    {
                        source.PlayDelayed(stepsControl * 2);
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

        public void StopMovement()
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        public void ChangeState(int newState)
        {
            playerState = (States)newState;
            GetComponent<Animator>().SetInteger("playerState", newState);
            GetComponent<Animator>().SetTrigger("changeDirection");
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
            playerAction = Actions.DEFAULT;

            string previousSceneName = "";
            if (GameManager.instance.previousSceneName.Equals("GameOver"))
            {
                previousSceneName = lastSceneGameOver;
                invertControlsTime = 0;
                movespeed = 0.01f;
            }
            else if (!GameManager.instance.currentSceneName.Equals("GameOver"))
            {
                lastSceneGameOver = GameManager.instance.previousSceneName;
                previousSceneName = GameManager.instance.previousSceneName;
            }

            if (!GameManager.instance.currentSceneName.Equals(GameManager.instance.previousSceneName))
            {
                if (GameManager.instance.currentSceneName.Equals("Corredor"))
                {
                    if (previousSceneName.Equals("Sala"))
                    {
                        transform.position = new Vector2((float)-9.8, (float)-0.6);
                    }
                    else if (previousSceneName.Equals("QuartoMae"))
                    {
                        transform.position = new Vector2((float)-1.6, (float)-0.3);
                        ChangeDirection(3);
                    }
                    else if (previousSceneName.Equals("Cozinha"))
                    {
                        transform.position = new Vector2((float)2.95, (float)-0.6);
                    }
                    else if (previousSceneName.Equals("Banheiro"))
                    {
                        transform.position = new Vector2((float)-11.3, (float)-0.3);
                        ChangeDirection(3);
                    }
                    //else if (previousSceneName.Equals("QuartoKid"))
                    else
                    {
                        transform.position = new Vector2((float)11.9, (float)-0.3);
                        ChangeDirection(3);
                    }
                }
                else if (GameManager.instance.currentSceneName.Equals("Cozinha"))
                {
                    transform.position = new Vector2((float)1.5, (float)0.7);
                }
                else if (GameManager.instance.currentSceneName.Equals("Jardim"))
                {
                    if (previousSceneName.Equals("Porao"))
                    {
                        transform.position = new Vector2((float)6.0, (float)2.5);
                        ChangeDirection(3);
                    }
                    else
                    {
                        transform.position = new Vector2((float)3.25, (float)2.3);
                    }
                }
                else if (GameManager.instance.currentSceneName.Equals("Porao"))
                {
                    transform.position = new Vector2((float)3.2, (float)0.5);
                    ChangeDirection(3);
                }
                else if (GameManager.instance.currentSceneName.Equals("QuartoKid"))
                {
                    transform.position = new Vector2((float)1.75, (float)0.65);
                    ChangeDirection(3);
                }
                else if (GameManager.instance.currentSceneName.Equals("QuartoMae"))
                {
                    transform.position = new Vector2((float)-3.8, (float)-0.45);
                    ChangeDirection(3);
                }
                else if (GameManager.instance.currentSceneName.Equals("Banheiro"))
                {
                    transform.position = new Vector2((float)2.171, (float)0.284);
                    ChangeDirection(3);
                }
                else if (GameManager.instance.currentSceneName.Equals("Sala"))
                {
                    if (previousSceneName.Equals("Jardim"))
                    {
                        transform.position = new Vector2((float)2.35, (float)-2.0);
                    }
                    //else if (previousSceneName.Equals("Corredor"))
                    else
                    {
                        transform.position = new Vector2((float)-3.15, (float)0.85);
                        ChangeDirection(3);
                    }
                }
                else if (GameManager.instance.currentSceneName.Equals("SideQuest"))
                {
                    transform.position = new Vector2(GameManager.instance.sideQuest.sideX, GameManager.instance.sideQuest.sideY);
                    ChangeDirection(GameManager.instance.sideQuest.sideDir);
                }
                GameManager.instance.paused = false;
            }

            if (Cat.instance != null)
            {
                Cat.instance.ChangePosition(transform.position.x - 0.6f, transform.position.y - 0.3f);
            }

            if ((GameManager.instance.mission is Mission8) &&
                !GameManager.instance.previousSceneName.Equals("GameOver") &&
                !GameManager.instance.currentSceneName.Equals("GameOver"))
            {
                if (rb == null) rb = GetComponent<Rigidbody2D>();
                corvoPositionX = transform.position.x;
                corvoPositionY = transform.position.y;
                corvoScene = GameManager.instance.currentSceneName;
                if (Crow.instance != null)
                {
                    Crow.instance.gameObject.SetActive(false);
                    Invoke("ChangeCorvoPosition", 2f);
                }
            }

        }

        public void ChangeCorvoPosition()
        {
            if (GameManager.instance.currentSceneName.Equals(corvoScene) && Crow.instance != null)
            {
                Crow.instance.ChangePosition(corvoPositionX, corvoPositionY);
                Crow.instance.gameObject.SetActive(true);
                Crow.instance.LookAtPlayer();
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
            Debug.Log("about to yield return WaitForSeconds(" + time + ")");
            yield return new WaitForSeconds(time);
            Debug.Log("Animation ended");
            ChangePositionDefault(x, y, dir);
            if (down)
            {
                playerAction = Actions.DEFAULT;
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
}