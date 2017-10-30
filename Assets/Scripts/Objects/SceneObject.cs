using UnityEngine;

public class SceneObject : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public enum PositionSprite { DEFAULT, LEFT };
    public PositionSprite positionSprite;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    float sizeX, sizeY;
	float posX, posY, posXdefault;

    void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
        sizeX = boxCollider.size.x/spriteRenderer.bounds.size.x;
        sizeY = boxCollider.size.y/spriteRenderer.bounds.size.y;
		posX = spriteRenderer.bounds.size.x/2;
		posY = transform.position.y;
		posXdefault = transform.position.x;

    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (Input.GetKeyDown(KeyCode.Z) && colliding && !MissionManager.instance.paused) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
        {
            ChangeSprite();
        }
    }
    
    void ChangeSprite()
    {
        print("SceneObject");
        if (spriteRenderer.sprite == sprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

        if (positionSprite == PositionSprite.LEFT && spriteRenderer.sprite == sprite2)
        {
            transform.position = new Vector3(transform.position.x + posX, posY, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(posXdefault, posY, transform.position.z);
        }

        boxCollider.size = new Vector2(
            sizeX*spriteRenderer.bounds.size.x, 
            sizeY*spriteRenderer.bounds.size.y);
		
    }

}
