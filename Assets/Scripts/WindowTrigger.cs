using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour {

	public Sprite aberto;
	public Sprite fechado;
	public Sprite monstro;
	public bool colliding = false;
	public bool scare = false;
    SpriteRenderer spriteRenderer;
	BoxCollider2D boxCollider;
	float sizeX, sizeY;
	float posXdefault, posY, posYdefault;

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
		posY = spriteRenderer.bounds.size.y/2;
		posYdefault = transform.position.y;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z) && colliding && !MissionManager.instance.paused && !MissionManager.instance.blocked) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
		{
			ChangeSprite();
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
        if (spriteRenderer.sprite == aberto && scare && !Flashlight.enable)
        {
            spriteRenderer.sprite = monstro;
            transform.Find("BirdEmitter").gameObject.SetActive(true);
            MissionManager.instance.blocked = true;
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
