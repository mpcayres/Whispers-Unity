using UnityEngine;

public class SceneObject : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
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
            spriteRenderer.bounds.size.x/transform.lossyScale.x, 
            spriteRenderer.bounds.size.y/transform.lossyScale.y);

    }

    public void ChangeSortingLayer(string newLayer)
    {
        spriteRenderer.sortingLayerName = newLayer;
    }

}
