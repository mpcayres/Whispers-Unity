using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FarAttackObject : MonoBehaviour {
    public Inventory.InventoryItems item;
    public float maxDistance = 10f;
    public bool attacking = false;

    float distance = 0;
    float speed = 2f, translateSpeed = 5f;
    float arcHeight = 1f; // altura
    int directionAttack = 0;
    bool initAttack = false;
    Vector3 posAttack = new Vector3(0,0,0);

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
                switch (player.direction)
                {
                    case 0:
                        posAttack = new Vector3(distance, 0, 0);
                        directionAttack = 0;
                        break;
                    case 1:
                        posAttack = new Vector3(-distance, 0, 0);
                        directionAttack = 1;
                        break;
                    case 2:
                        posAttack = new Vector3(0, distance, 0);
                        directionAttack = 2;
                        break;
                    case 3:
                        posAttack = new Vector3(0, -distance, 0);
                        directionAttack = 3;
                        break;
                    default:
                        break;
                }

                transform.localPosition = new Vector3(0, 0, 0);
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                initAttack = true;
            }
        }

        if (initAttack)
        {
            if (directionAttack == 0 || directionAttack == 1)
            {
                print("ATTACK01");
                float x0 = 0;
                float x1 = posAttack.x;
                float dist = x1 - x0;
                float nextX = Mathf.MoveTowards(transform.localPosition.x, x1, translateSpeed * Time.deltaTime);
                float baseY = Mathf.Lerp(0, posAttack.y, (nextX - x0) / dist);
                float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.localPosition.z);
                transform.rotation = LookAt2D(nextPos - transform.localPosition);
                transform.localPosition = nextPos;
            }
            else
            {
                print("ATTACK23");
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, posAttack, translateSpeed * Time.deltaTime);
                if (transform.localPosition.y < posAttack.y/2)
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f + transform.localPosition.y / posAttack.y);
                }
                else
                {
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f - transform.localPosition.y / posAttack.y / 2);
                }
            }

            if (transform.localPosition == posAttack) EndAnimation();
            
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
                    transform.position = new Vector3(0, -distance, 0);
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
        attacking = true;
        distance = 0;
        // deleta uma pedra
        // animação + som
        //spriteRenderer.enabled = false;
    }
}
