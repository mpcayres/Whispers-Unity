using UnityEngine;

public class ActionPatroller : MonoBehaviour {
    GameObject target;
    public string tagAction = "Mom";
    public string nameAction = "";
    public bool changeSizeByDirection = true;

    void Start()
    {
        if (nameAction.Equals(""))
        {
            target = GameObject.FindGameObjectWithTag(tagAction);
        }
        else
        {
            target = GameObject.Find(nameAction);
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.time);

            if (changeSizeByDirection)
            {
                int direction = target.GetComponent<Patroller>().GetDirection(); ;
                switch (direction)
                {
                    case 0:
                        GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, 0f);
                        break;
                    case 1:
                        GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, 0f);
                        break;
                    case 2:
                        GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.5f);
                        break;
                    case 3:
                        GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.5f);
                        break;
                    default:
                        GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
                        break;
                }
            }
        }
        else
        {
            if (nameAction.Equals(""))
            {
                target = GameObject.FindGameObjectWithTag(tagAction);
            }
            else
            {
                target = GameObject.Find(nameAction);
            }
        }
    }

    //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Action: " + collision.tag);
        if (collision.gameObject.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }

    }

}

