using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovingObject : MonoBehaviour {
    public bool canMoveUp = false;
    public bool colliding = false;
    public string prefName = ""; // Padrão: identificador do objeto (MO) + _ + nome da cena + _ + identificador

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    GameObject player;
    Player script;

    float distanceWantedX = 0.4f;
    float distanceWantedY = 0.45f;
    int originalDirection;
    float originalX, originalY;

    void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<Player>();
    }
	
	void Update ()
    {
        if (script.playerAction != Player.Actions.ON_OBJECT)
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
        else
        {
            spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
        if (colliding && !MissionManager.instance.paused && !MissionManager.instance.pausedObject && !MissionManager.instance.blocked)
        {
            if (CrossPlatformInputManager.GetButton("keySpecial") && canMoveUp)
            {
                if (CrossPlatformInputManager.GetButtonDown("keyMove"))
                {
                    MoveUp();
                }
            }
            else if (script.playerAction != Player.Actions.ON_OBJECT)
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
        script.playerAction = Player.Actions.MOVING_OBJECT;
        script.animator.SetTrigger("movingObject");
    }

    public void Move()
    {
        if (!MissionManager.instance.scenerySounds2.source.isPlaying)
            MissionManager.instance.scenerySounds2.PlaySlide(1);
        //print("MOVE");
        var relativePoint = transform.InverseTransformPoint(player.transform.position);
        //para ver se esta na esquerda ou direta, em cima ou baixo
        //para nao dar problema com a colisão do MovingObject em bordas, faze-las grandes (maiores do que ele)

        if ((script.direction == 0 && relativePoint.x < 0.0)
            || (script.direction == 1 && relativePoint.x > 0.0)
            || (script.direction == 0 && relativePoint.x < 0.0 && script.wantedDirection == 1)
            || (script.direction == 1 && relativePoint.x > 0.0 && script.wantedDirection == 0)
            )
        {
            Vector3 diff = new Vector3(rb.position.x, rb.position.y) - player.transform.position;
            diff.y = 0; // ignore Y
            diff.z = 0;
            rb.position = player.transform.position + diff.normalized * distanceWantedX;
        }
        else if ((script.direction == 2 && relativePoint.y < 0.0)
            || (script.direction == 3 && relativePoint.y > 0.0)
            || (script.direction == 2 && relativePoint.y < 0.0 && script.wantedDirection == 3)
            || (script.direction == 3 && relativePoint.y > 0.0 && script.wantedDirection == 2))
        {
            Vector3 diff = new Vector3(rb.position.x, rb.position.y) - player.transform.position;
            diff.x = 0; // ignore X
            diff.z = 0;
            rb.position = player.transform.position + diff.normalized * distanceWantedY;
        }
    }

    public void EndMove()
    {
        MissionManager.instance.scenerySounds2.StopSound();
        //print("ENDMOVE");
        script.playerAction = Player.Actions.DEFAULT;
        script.animator.SetTrigger("changeDirection");
    }

    public void MoveUp()
    {
        //print("MOVEUP");
        if (script.playerAction != Player.Actions.ON_OBJECT) {
            originalDirection = script.direction;
            if (originalDirection != 3)
            {
                script.playerAction = Player.Actions.ON_OBJECT;
                originalX = player.transform.position.x;
                originalY = player.transform.position.y;
                GetComponent<Collider2D>().enabled = false;
                player.GetComponent<Collider2D>().enabled = false;
                if (originalDirection == 0)
                {
                    script.ChangePositionDefault(rb.position.x - (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y, -1);
                    script.MoveUpAnimation(this, "playerClimbEast", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), originalDirection);
                }
                else if (originalDirection == 1)
                {
                    script.ChangePositionDefault(rb.position.x + (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y, -1);
                    script.MoveUpAnimation(this, "playerClimbWest", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), originalDirection);
                }
                else if (originalDirection == 2)
                {
                    script.ChangePositionDefault(rb.position.x, rb.position.y, -1);
                    script.MoveUpAnimation(this, "playerClimbNorth", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), originalDirection);
                }
                /*else if (originalDirection == 3)
                {
                    script.ChangePositionDefault(this, rb.position.x, rb.position.y, -1);
                    script.MoveUpAnimation("playerClimbSouth", rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), originalDirection);
                }*/
            }
        }
        else {
            if (originalDirection == 0)
            {
                script.ChangePositionDefault(rb.position.x - (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y + (spriteRenderer.bounds.size.y / 4), 1);
                script.MoveDownAnimation("playerDownWest", originalX, originalY, -1);
            }
            else if (originalDirection == 1)
            {
                script.ChangePositionDefault(rb.position.x + (spriteRenderer.bounds.size.x / (float)1.5), rb.position.y + (spriteRenderer.bounds.size.y / 4), 0);
                script.MoveDownAnimation("playerDownEast", originalX, originalY, -1);
            }
            else if (originalDirection == 2)
            {
                script.ChangePositionDefault(rb.position.x, rb.position.y, 3);
                script.MoveDownAnimation("playerDownSouth", originalX, originalY, -1);
            }
            /*else if (originalDirection == 3)
            {
            script.ChangePositionDefault(this, rb.position.x, rb.position.y + (spriteRenderer.bounds.size.y / 4), 2);
                script.MoveDownAnimation("playerDownNorth", originalX, originalY, -1);
            }*/
        }
    }

    public void ChangePosition(float x, float y)
    {
        rb.position = new Vector2(x, y);
    }

}
