using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb, rbPlayer;
    Vector2 offset;
    Player script;
    bool blockedMove = false;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        script = player.GetComponent<Player>();
    }
	
	void Update ()
    {
		
	}

    public void SetOffset()
    {
        print("OFFSET");
        offset = new Vector2(Mathf.Abs(rbPlayer.position.x - rb.position.x), 
            Mathf.Abs(rbPlayer.position.y - rb.position.y));
        script.playerState = Player.Actions.MOVING_OBJECT;
    }

    public void Move()
    {
        print("MOVE");
        if (!blockedMove)
        {
            var relativePoint = transform.InverseTransformPoint(player.transform.position);

            if (script.direction == 0 && relativePoint.x < 0.0)
            {
                print("EAST");
                rb.position = new Vector2(rbPlayer.position.x + offset.x, rb.position.y);
            }
            else if (script.direction == 1 && relativePoint.x > 0.0)
            {
                print("WEST");
                rb.position = new Vector2(rbPlayer.position.x - offset.x, rb.position.y);
            }
            else if (script.direction == 2 && relativePoint.y < 0.0)
            {
                print("NORTH");
                rb.position = new Vector2(rb.position.x, rbPlayer.position.y + offset.y);
            }
            else if (script.direction == 3 && relativePoint.y > 0.0)
            {
                print("SOUTH");
                rb.position = new Vector2(rb.position.x, rbPlayer.position.y - offset.y);
            }
        }
    }

    public void EndMove()
    {
        script.playerState = Player.Actions.DEFAULT;
    }

    public void ChangeSortingLayer(string newLayer)
    {
        spriteRenderer.sortingLayerName = newLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            print(collision.gameObject.tag);
            blockedMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            print(collision.gameObject.tag);
            blockedMove = false;
        }
    }

}
