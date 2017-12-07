using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomAction : MonoBehaviour {
    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Mom");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.time);

        int direction = target.GetComponent<Mom>().GetDirection();
        switch (direction)
        {
            case 0:
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, 0f);
                break;
            case 1:
                GetComponent<BoxCollider2D>().offset = new Vector2(0.5f, 0f);
                break;
            case 2:
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.5f);
                break;
            case 3:
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.5f);
                break;
            default:
                break;
        }
    }

    //Interacoes estao por trigger em vista de nao serem possiveis de identificacao em objeto kinematic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("MomAction: " + collision.tag);
        if (collision.gameObject.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }

    }

}

