using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    GameObject player;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Player script;
    float distanceWantedX = 0.4f;
    float distanceWantedY = 0.45f;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        script = player.GetComponent<Player>();
    }
	
	void Update ()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (colliding && !MissionManager.instance.paused)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                InitMove();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                Move();
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                EndMove();
            }
        }
    }

    public void InitMove()
    {
        print("INITMOVE");
        script.playerState = Player.Actions.MOVING_OBJECT;
        script.animator.SetTrigger("movingObject");
    }

    public void Move()
    {
        print("MOVE");
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
        print("ENDMOVE");
        script.playerState = Player.Actions.DEFAULT;
        script.animator.SetTrigger("changeDirection");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

}
