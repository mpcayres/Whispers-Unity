using UnityEngine;

namespace CrowShadowObjects
{
    public class RegularObject : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
    }
}