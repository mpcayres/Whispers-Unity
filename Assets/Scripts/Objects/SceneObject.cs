using UnityEngine;

public class SceneObject : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    float sizeX, sizeY;
	public bool leftSide = false;
	float posX, posY, posZ, posXdefault;

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
		posZ = transform.position.z;
		posXdefault = transform.position.x;

    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (Input.GetKeyDown(KeyCode.Z) && colliding) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
        {
            ChangeSprite();
        }
		posZ = transform.position.z;
    }
    
    void ChangeSprite()
    {
		posZ = transform.position.z;
        print("SceneObject");
        if (spriteRenderer.sprite == sprite1)
        {
            spriteRenderer.sprite = sprite2;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }
		if (leftSide && spriteRenderer.sprite == sprite2 )
			transform.position = new Vector3 (transform.position.x+posX, posY, posZ);
		else
			transform.position = new Vector3 (posXdefault, posY, posZ);

        boxCollider.size = new Vector2(
            sizeX*spriteRenderer.bounds.size.x, 
            sizeY*spriteRenderer.bounds.size.y);
		
    }

}
