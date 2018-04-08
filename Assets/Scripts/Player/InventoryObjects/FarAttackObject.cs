using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class FarAttackObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public float maxDistance = 10f;
    public bool attacking = false, hitSuccess = false;

    float distance = 0, startX = 0, startY = 0;
    float speed = 2f, translateSpeed = 5f;
    float arcHeight = 0.8f; // altura
    float timeLeftPedra = 0, maxTimePedra = 0.2f;
    int directionAttack = 0;
    bool initAttack = false;
    Vector3 posAttack = new Vector3(0,0,0), oldParentPosition;

    Player player;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GetComponentInParent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attacking = false;
    }

    void Update()
    {
        if (timeLeftPedra > 0)
        {
            timeLeftPedra -= Time.deltaTime;
        }

        if (hitSuccess)
        {
            EndThrow();
            // adiciona outra no local de transform.position
            // som acerto
        }
        else if (attacking && timeLeftPedra <= 0)
        {
            EndThrow();
            // som chão
        }

        if (Inventory.GetCurrentItemType() == item && !initAttack && !attacking &&
            !MissionManager.instance.paused && !MissionManager.instance.blocked && !MissionManager.instance.pausedObject)
        {
            if (CrossPlatformInputManager.GetButtonDown("keyUseObject"))
            {
                spriteRenderer.enabled = true;
                spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            }
            if (CrossPlatformInputManager.GetButton("keyUseObject") && distance < maxDistance)
            {
                distance += speed * Time.deltaTime;
            }
            else if (CrossPlatformInputManager.GetButtonUp("keyUseObject"))
            {
                posAttack = new Vector3(transform.position.x, transform.position.y, 0);

                switch (player.direction)
                {
                    case 0:
                        directionAttack = 0;
                        break;
                    case 1:
                        directionAttack = 1;
                        break;
                    case 2:
                        directionAttack = 2;
                        break;
                    case 3:
                        directionAttack = 3;
                        break;
                    default:
                        break;
                }

                transform.localPosition = new Vector3(0, 0, 0);
                if (directionAttack == 0 || directionAttack == 1)
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.75f);
                }
                else
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
                }
                initAttack = true;
                startX = transform.position.x;
                startY = transform.position.y;
            }
        }

        if (initAttack)
        {
            if (directionAttack == 0 || directionAttack == 1)
            {
                float dist = posAttack.x - startX;
                float nextX = Mathf.MoveTowards(transform.position.x, posAttack.x, translateSpeed * Time.deltaTime);
                float baseY = Mathf.Lerp(startY, posAttack.y, (nextX - startX) / dist);
                float arc = arcHeight * (nextX - startX) * (nextX - posAttack.x) / (-0.25f * dist * dist);
                Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
                transform.rotation = LookAt2D(nextPos - transform.position);
                transform.position = nextPos;

                if (Vector3.Distance(transform.position, posAttack) <= 1f) EndAnimation();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, posAttack, translateSpeed * Time.deltaTime);
                if (Mathf.Abs(transform.position.y) < Mathf.Abs(posAttack.y / 2))
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f + (Mathf.Abs(transform.position.y) / Mathf.Abs(posAttack.y)));
                }
                else
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f - (Mathf.Abs(transform.position.y) / Mathf.Abs(posAttack.y) / 2));
                }

                if (transform.position == posAttack) EndAnimation();
            }

        }
        else if (attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, posAttack, translateSpeed * Time.deltaTime);
        }
        else
        {
            switch (player.direction)
            {
                case 0:
                    transform.localPosition = new Vector3(distance, 0, 0);
                    break;
                case 1:
                    transform.localPosition = new Vector3(-distance, 0, 0);
                    break;
                case 2:
                    transform.localPosition = new Vector3(0, distance, 0);
                    break;
                case 3:
                    transform.localPosition = new Vector3(0, -distance, 0);
                    break;
                default:
                    break;
            }
        }
    }

    private Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    private void EndAnimation()
    {
        initAttack = false;
        hitSuccess = false;
        attacking = true;
        timeLeftPedra = maxTimePedra;
    }

    private void EndThrow()
    {
        spriteRenderer.enabled = false;
        transform.position = new Vector3(0, 0, 0);
        distance = 0;
        hitSuccess = false;
        attacking = false;
        timeLeftPedra = 0;
        // deleta uma pedra
    }
}
