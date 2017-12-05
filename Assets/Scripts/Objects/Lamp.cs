using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public bool colliding = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (Input.GetKeyDown(KeyCode.Z) && colliding &&
            !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }
    }
}
