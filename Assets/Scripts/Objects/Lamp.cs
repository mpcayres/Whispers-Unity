using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Lamp : MonoBehaviour {
    public string prefName = ""; // Padrão: identificador do objeto (L) + _ + nome da cena + _ + identificador
    public bool colliding = false;
    private bool change = false;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;

        if (CrossPlatformInputManager.GetButtonDown("keyInteract") && colliding &&
            !GameManager.instance.paused && !GameManager.instance.blocked && !GameManager.instance.pausedObject)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            GetComponent<CircleCollider2D>().enabled = GetComponent<Light>().enabled;
            change = true;
        }
    }

    public bool Changed()
    {
        return change;
    }
}
