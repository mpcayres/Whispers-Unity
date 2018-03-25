﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SceneObject : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public enum PositionSprite { DEFAULT, LEFT, RIGHT, UP, DOWN };
    public PositionSprite positionSprite = PositionSprite.DEFAULT;
    public float scale = 1;
    public bool isUp = false;
    public bool colliding = false;
    bool isActive = false, opened = false;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Player player;

    float sizeX, sizeY;
	float posX, posY, posXdefault, posYdefault;

    void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
        sizeX = boxCollider.size.x/spriteRenderer.bounds.size.x;
        sizeY = boxCollider.size.y/spriteRenderer.bounds.size.y;
		posX = spriteRenderer.bounds.size.x/scale;
        posY = spriteRenderer.bounds.size.y/scale;
        posYdefault = transform.position.y;
		posXdefault = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if ((!isUp && (player.playerState == Player.Actions.DEFAULT)) || (isUp && (player.playerState == Player.Actions.ON_OBJECT)))
        {
            if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding &&
            !MissionManager.instance.paused && !MissionManager.instance.blocked &&
            !MissionManager.instance.pausedObject) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                ChangeSprite();
            }
        }
    }
    
    void ChangeSprite()
    {
        print("SceneObject");
        if (spriteRenderer.sprite == sprite1)
        {
            spriteRenderer.sprite = sprite2;
            isActive = true;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
            isActive = false;
            opened = true;
        }

        if (positionSprite == PositionSprite.LEFT && spriteRenderer.sprite == sprite2)
        {
            transform.position = new Vector3(transform.position.x + posX, posYdefault, transform.position.z);
        }
        else if(positionSprite == PositionSprite.RIGHT && spriteRenderer.sprite == sprite2)
        {
            transform.position = new Vector3(transform.position.x - posX, posYdefault, transform.position.z);
        }
        else if (positionSprite == PositionSprite.UP && spriteRenderer.sprite == sprite2)
        {
            transform.position = new Vector3(posXdefault, transform.position.y + posY, transform.position.z);
        }
        else if (positionSprite == PositionSprite.DOWN && spriteRenderer.sprite == sprite2)
        {
            transform.position = new Vector3(posXdefault, transform.position.y - posY, transform.position.z);
        }
        else if (positionSprite != PositionSprite.DEFAULT)
        {
            transform.position = new Vector3(posXdefault, posYdefault, transform.position.z);
        }

        boxCollider.size = new Vector2(
            sizeX*spriteRenderer.bounds.size.x, 
            sizeY*spriteRenderer.bounds.size.y);
		
    }

    public bool IsActive()
    {
        return isActive;
    }

    public bool ObjectOpened()
    {
        return opened;
    }

}
