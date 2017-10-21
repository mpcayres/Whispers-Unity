using UnityEngine;

public class SceneObject : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    float sizeX, sizeY;

    void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
        sizeX = boxCollider.size.x;
        sizeY = boxCollider.size.y;
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (Input.GetKeyDown(KeyCode.Z) && colliding) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
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
        boxCollider.size = new Vector2(
            sizeX*spriteRenderer.bounds.size.x/transform.lossyScale.x, 
            sizeY*spriteRenderer.bounds.size.y/transform.lossyScale.y);
    }

}
