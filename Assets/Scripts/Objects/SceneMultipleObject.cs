using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using CrowShadowManager;

public class SceneMultipleObject : MonoBehaviour {

    public Sprite[] sprite1;
    public Sprite[] sprite2;
    public enum PositionSprite { DEFAULT, LEFT, RIGHT, UP, DOWN };
    public PositionSprite positionSprite;
    public float scale = 1;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    bool sprite1Selected = true;
    int cont = 0;
    float sizeX, sizeY;
    float posX, posY, posXdefault, posYdefault;
    float timeLeft = 0;
    public float timeMax = (float) 0.25;
    public AudioClip noise;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = sprite1[0];
        sizeX = boxCollider.size.x / spriteRenderer.bounds.size.x;
        sizeY = boxCollider.size.y / spriteRenderer.bounds.size.y;
        posX = spriteRenderer.bounds.size.x / scale;
        posY = spriteRenderer.bounds.size.y / scale;
        posYdefault = transform.position.y;
        posXdefault = transform.position.x;
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }

        if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding &&
            !GameManager.instance.paused && !GameManager.instance.blocked &&
            !GameManager.instance.pausedObject) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
        {
            ChangeSprite();
        } else if(!sprite1Selected && sprite2.Length > 1 && timeLeft <= 0){
            spriteRenderer.sprite = sprite2[cont++];
            if (cont >= sprite2.Length) cont = 0;
            timeLeft = timeMax;
        } else if (sprite1Selected && sprite1.Length > 1 && timeLeft <= 0)
        {
            spriteRenderer.sprite = sprite1[cont++];
            if (cont >= sprite1.Length) cont = 0;
            timeLeft = timeMax;
        }
        if (!source.isPlaying && !sprite1Selected)
        {
           source.PlayOneShot(noise);
        }
    }

    public void ChangeSprite()
    {
        print("SceneMultipleObject");
        cont = 0;
        timeLeft = timeMax;
        sprite1Selected = !sprite1Selected;
        if (sprite1Selected)
        {
            spriteRenderer.sprite = sprite1[cont++];
            if (cont >= sprite1.Length) cont = 0;
        }
        else
        {
            spriteRenderer.sprite = sprite2[cont++];
            if (cont >= sprite2.Length) cont = 0;
        }

        if (positionSprite == PositionSprite.LEFT && !sprite1Selected)
        {
            transform.position = new Vector3(transform.position.x + posX, posYdefault, transform.position.z);
        }
        else if (positionSprite == PositionSprite.RIGHT && !sprite1Selected)
        {
            transform.position = new Vector3(transform.position.x - posX, posYdefault, transform.position.z);
        }
        else if (positionSprite == PositionSprite.UP && !sprite1Selected)
        {
            transform.position = new Vector3(posXdefault, transform.position.y + posY, transform.position.z);
        }
        else if (positionSprite == PositionSprite.DOWN && !sprite1Selected)
        {
            transform.position = new Vector3(posXdefault, transform.position.y - posY, transform.position.z);
        }
        else if (positionSprite != PositionSprite.DEFAULT)
        {
            transform.position = new Vector3(posXdefault, posYdefault, transform.position.z);
        }

        boxCollider.size = new Vector2(
            sizeX * spriteRenderer.bounds.size.x,
            sizeY * spriteRenderer.bounds.size.y);

    }

}
