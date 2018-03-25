using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WindowTrigger : MonoBehaviour {

	public Sprite aberto;
	public Sprite fechado;
	public Sprite monstro;
    public bool colliding = false;
	public bool scare = false;
    SpriteRenderer spriteRenderer;
	BoxCollider2D boxCollider;
	float sizeX, sizeY;
	float posXdefault;
    bool gameOver = false;
    float timeLeft = 5;

    void Start ()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = aberto;
        }
		sizeX = boxCollider.size.x/spriteRenderer.bounds.size.x;
		sizeY = boxCollider.size.y/spriteRenderer.bounds.size.y;

		posXdefault = transform.position.x;
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding && !MissionManager.instance.paused && !MissionManager.instance.blocked) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
		{
			ChangeSprite();
		}
        if (gameOver)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                MissionManager.instance.GameOver();
            }
        }
	}

	void ChangeSprite()
	{
		if (spriteRenderer.sprite == aberto)
		{
			spriteRenderer.sprite = fechado;
		}
		else if (spriteRenderer.sprite == fechado)
		{
			spriteRenderer.sprite = aberto;
		}

		boxCollider.size = new Vector2(
			sizeX*spriteRenderer.bounds.size.x, 
			sizeY*spriteRenderer.bounds.size.y);
	}

    public void ScareTrigger()
    {
        if (spriteRenderer.sprite == aberto && scare && !Flashlight.GetState())
        {
            spriteRenderer.sprite = monstro;
            MissionManager.instance.scenerySounds.PlayScare(1);
            MissionManager.instance.scenerySounds.PlayBird(1);
            MissionManager.instance.scenerySounds.PlayBird(4);
            transform.Find("BirdEmitter").gameObject.SetActive(true);
            MissionManager.instance.blocked = true;
            gameOver = true;
        }

        /*if (spriteRenderer.sprite == monstro)
        {
            transform.position = new Vector3(posXdefault, (float)(transform.position.y + posY / 2 + 0.1), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(posXdefault, posYdefault, transform.position.z);
        }*/
    }

}
