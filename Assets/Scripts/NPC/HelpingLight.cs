using UnityEngine;
using System.Collections;

public class HelpingLight : MonoBehaviour
{
    public float speed;
    public bool stoped = false;
    public Vector3[] targets;

    public bool active = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;

    private int destPoint = 0;

    public static bool PlayerInside;
  
    // Update is called once per frame
    void Update()
    {
        if (active) {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        float step = speed * Time.deltaTime;
        if (targets.Length == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, targets[destPoint], step);

        float dist = Vector3.Distance(targets[destPoint], transform.position);
        if (dist < 0.4f)
        {
            if (destPoint + 1 == targets.Length && destroyEndPath)
            {
                Destroy(gameObject);
            }
            else if (destPoint + 1 == targets.Length && stopEndPath)
            {
                Stop();
            }
            else
            {
                destPoint = (destPoint + 1) % targets.Length;
            }
        }
    }

    public void Stop()
    {
        active = false;
        stoped = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (gameObject.activeSelf && collision.gameObject.tag.Equals("Player"))
        {
            PlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
