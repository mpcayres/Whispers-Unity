﻿using UnityEngine;

public class FurtiveObject : MonoBehaviour {
    GameObject player;
    public bool colliding = false;
    SpriteRenderer spriteRenderer;
    float timeLeft;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (colliding && !MissionManager.instance.paused && !MissionManager.instance.blocked)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }

            if (player.GetComponent<Renderer>().enabled && Input.GetKeyDown(KeyCode.Z)) //GetKeyDown e GetKeyUp não pode ser usado fora do Update
            {
                player.GetComponent<Renderer>().enabled = false;
                player.layer = LayerMask.NameToLayer("PlayerHidden");
                MissionManager.instance.pausedObject = true;
                timeLeft = 3;
            }
            else if (!player.GetComponent<Renderer>().enabled && (Input.GetKeyDown(KeyCode.Z) || timeLeft <= 0))
            {
                player.GetComponent<Renderer>().enabled = true;
                player.layer = LayerMask.NameToLayer("Player");
                MissionManager.instance.pausedObject = false;
                timeLeft = 0;
            }
        }
        
    }
}
