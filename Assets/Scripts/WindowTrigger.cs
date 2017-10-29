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
	public bool upSide = false;
	float posX, posY, posZ, posYdefault;

	void Start ()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null)
			spriteRenderer.sprite = aberto;
		sizeX = boxCollider.size.x/spriteRenderer.bounds.size.x;
		sizeY = boxCollider.size.y/spriteRenderer.bounds.size.y;

		posX = transform.position.x;
		posY = spriteRenderer.bounds.size.y/2;
		posZ = transform.position.z;
		posYdefault = transform.position.y;
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
		if (spriteRenderer.sprite == aberto && !scare)
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
		


	private void OnTriggerEnter2D (Collider2D other)
	{
		
		/*if (spriteRenderer.sprite == aberto && scare)
		{
			spriteRenderer.sprite = fechado;
		}*/

		if (spriteRenderer.sprite == aberto && scare && other.tag == "Player" ){
			spriteRenderer.sprite = monstro;
			MissionManager.instance.paused = true;
		}

	
		if (upSide && spriteRenderer.sprite == monstro )
			transform.position = new Vector3 (posX, (float)(transform.position.y + posY/2 + 0.1), posZ);
		else
			transform.position = new Vector3 (posX, posYdefault, posZ);
		
	}
}
