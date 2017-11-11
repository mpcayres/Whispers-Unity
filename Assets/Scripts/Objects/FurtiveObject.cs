using UnityEngine;

public class FurtiveObject : MonoBehaviour {
    GameObject player;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        if (colliding && !MissionManager.instance.paused)
        {
            if (Input.GetKeyDown(KeyCode.Z)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                if (player.GetComponent<Renderer>().enabled)
                {
                    player.GetComponent<Renderer>().enabled = false;
                    player.layer = LayerMask.NameToLayer("PlayerHidden");
                    player.GetComponent<MissionManager>().blocked = true;
                }
                else
                {
                    player.GetComponent<Renderer>().enabled = true;
                    player.layer = LayerMask.NameToLayer("Player");
                    player.GetComponent<MissionManager>().blocked = false;
                }
            }
        }
    }
}
