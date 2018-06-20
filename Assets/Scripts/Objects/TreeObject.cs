using UnityEngine;
using CrowShadowManager;

namespace CrowShadowObjects
{
    public class TreeObject : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        float offset;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GameObject player = GameManager.instance.gameObject;
            offset = GetComponent<BoxCollider2D>().offset.y
                - player.GetComponent<SpriteRenderer>().size.y * player.transform.lossyScale.y / 2;
        }

        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y + offset) * 100f) * -1;
        }
    }
}