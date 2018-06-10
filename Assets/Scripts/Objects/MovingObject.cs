using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;
using CrowShadowPlayer;

namespace CrowShadowObjects
{
    public class MovingObject : MonoBehaviour
    {
        public bool canMoveUp = false;
        public bool colliding = false;
        public string prefName = ""; // Padrão: identificador do objeto (MO) + _ + nome da cena + _ + identificador
        public float scaleMoveUp = 4f;

        SpriteRenderer spriteRenderer;
        Rigidbody2D rb;

        GameObject player;
        Player scriptPlayer;

        float distanceWantedX = 0.4f;
        float distanceWantedY = 0.45f;
        int originalDirection;
        float originalX, originalY;
        float aproxTimerMult = 1f;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            scriptPlayer = player.GetComponent<Player>();
        }

        void Update()
        {
            if (scriptPlayer.playerAction != Player.Actions.ON_OBJECT)
            {
                spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
            }
            else
            {
                spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }
            if (colliding && !GameManager.instance.paused && !GameManager.instance.pausedObject && !GameManager.instance.blocked)
            {
                if (CrossPlatformInputManager.GetButton("keySpecial") && canMoveUp)
                {
                    if (CrossPlatformInputManager.GetButtonDown("keyMove"))
                    {
                        MoveUp();
                    }
                }
                else if (scriptPlayer.playerAction != Player.Actions.ON_OBJECT)
                {
                    if (CrossPlatformInputManager.GetButtonDown("keyMove")) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
                    {
                        InitMove();
                    }
                    else if (CrossPlatformInputManager.GetButton("keyMove"))
                    {
                        Move();
                    }
                    else if (CrossPlatformInputManager.GetButtonUp("keyMove"))
                    {
                        EndMove();
                    }
                }
            }
        }

        public void InitMove()
        {
            //print("INITMOVE");
            scriptPlayer.playerAction = Player.Actions.MOVING_OBJECT;
            scriptPlayer.animator.SetTrigger("movingObject");
            aproxTimerMult = 0f;
        }

        public void Move()
        {
            if (scriptPlayer.playerAction != Player.Actions.MOVING_OBJECT)
            {
                InitMove();
            }

            if (!GameManager.instance.scenerySounds2.source.isPlaying)
            {
                GameManager.instance.scenerySounds2.PlaySlide(1);
            }
            //print("MOVE");
            var relativePoint = transform.InverseTransformPoint(player.transform.position);
            //para ver se esta na esquerda ou direta, em cima ou baixo
            //para nao dar problema com a colisão do MovingObject em bordas, faze-las grandes (maiores do que ele)

            if ((scriptPlayer.direction == 0 && relativePoint.x < 0.0)
                || (scriptPlayer.direction == 1 && relativePoint.x > 0.0)
                || (scriptPlayer.direction == 0 && relativePoint.x < 0.0 && scriptPlayer.wantedDirection == 1)
                || (scriptPlayer.direction == 1 && relativePoint.x > 0.0 && scriptPlayer.wantedDirection == 0)
                )
            {
                Vector3 diff = new Vector3(rb.position.x, rb.position.y) - player.transform.position;
                diff.y = 0; // ignore Y
                diff.z = 0;
                rb.position = Vector2.Lerp(rb.position, player.transform.position + diff.normalized * distanceWantedX, aproxTimerMult * Time.deltaTime);
            }
            else if ((scriptPlayer.direction == 2 && relativePoint.y < 0.0)
                || (scriptPlayer.direction == 3 && relativePoint.y > 0.0)
                || (scriptPlayer.direction == 2 && relativePoint.y < 0.0 && scriptPlayer.wantedDirection == 3)
                || (scriptPlayer.direction == 3 && relativePoint.y > 0.0 && scriptPlayer.wantedDirection == 2))
            {
                Vector3 diff = new Vector3(rb.position.x, rb.position.y) - player.transform.position;
                diff.x = 0; // ignore X
                diff.z = 0;
                rb.position = Vector2.Lerp(rb.position, player.transform.position + diff.normalized * distanceWantedY, aproxTimerMult * Time.deltaTime);
            }

            if (aproxTimerMult < 100f)
            {
                aproxTimerMult += (10 * Time.deltaTime);
            }
        }

        public void EndMove()
        {
            GameManager.instance.scenerySounds2.StopSound();
            //print("ENDMOVE");
            scriptPlayer.playerAction = Player.Actions.DEFAULT;
            scriptPlayer.animator.SetTrigger("changeDirection");
        }

        public void MoveUp()
        {
            //print("MOVEUP");
            scriptPlayer.StopMovement();
            if (scriptPlayer.playerAction != Player.Actions.ON_OBJECT)
            {
                originalDirection = scriptPlayer.direction;
                if (originalDirection != 3)
                {
                    scriptPlayer.playerAction = Player.Actions.ANIMATION;
                    originalX = player.transform.position.x;
                    originalY = player.transform.position.y;
                    GetComponent<Collider2D>().enabled = false;
                    player.GetComponent<Collider2D>().enabled = false;
                    if (originalDirection == 0)
                    {
                        scriptPlayer.ChangePositionDefault(rb.position.x - (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y, -1);
                        scriptPlayer.MoveUpAnimation(this, "playerClimbEast", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), originalDirection);
                    }
                    else if (originalDirection == 1)
                    {
                        scriptPlayer.ChangePositionDefault(rb.position.x + (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y, -1);
                        scriptPlayer.MoveUpAnimation(this, "playerClimbWest", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), originalDirection);
                    }
                    else if (originalDirection == 2)
                    {
                        scriptPlayer.ChangePositionDefault(rb.position.x, rb.position.y, -1);
                        scriptPlayer.MoveUpAnimation(this, "playerClimbNorth", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), originalDirection);
                    }
                    /*else if (originalDirection == 3)
                    {
                        scriptPlayer.ChangePositionDefault(this, rb.position.x, rb.position.y, -1);
                        scriptPlayer.MoveUpAnimation("playerClimbSouth", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), originalDirection);
                    }*/
                }
            }
            else
            {
                if (originalDirection == 0)
                {
                    scriptPlayer.ChangePositionDefault(rb.position.x - (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), 1);
                    scriptPlayer.MoveDownAnimation("playerDownWest", originalX, originalY, -1);
                }
                else if (originalDirection == 1)
                {
                    scriptPlayer.ChangePositionDefault(rb.position.x + (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y + (spriteRenderer.bounds.size.y / scaleMoveUp), 0);
                    scriptPlayer.MoveDownAnimation("playerDownEast", originalX, originalY, -1);
                }
                else if (originalDirection == 2)
                {
                    scriptPlayer.ChangePositionDefault(rb.position.x, rb.position.y, 3);
                    scriptPlayer.MoveDownAnimation("playerDownSouth", originalX, originalY, -1);
                }
                /*else if (originalDirection == 3)
                {
                scriptPlayer.ChangePositionDefault(this, rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), 2);
                    scriptPlayer.MoveDownAnimation("playerDownNorth", originalX, originalY, -1);
                }*/
            }
        }

        public void ChangePosition(float x, float y)
        {
            rb.position = new Vector2(x, y);
        }

    }
}