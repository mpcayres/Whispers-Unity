using UnityEngine;

public class HelpingLight : MonoBehaviour
{
    public float speed;
    public Vector3[] targets;

    public bool emitter = false;
    public bool stoped = false;
    public bool active = false;
    public bool destroyEndPath = false;
    public bool stopEndPath = false;
    public bool playerInside = true;

    private int destPoint = 0;

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
        if (gameObject.activeSelf && collision.gameObject.tag.Equals("Player"))
        {
            playerInside = true;
            if (emitter)
            {
                // emitir corvbabies
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.activeSelf && collision.gameObject.tag.Equals("Player"))
        {
            playerInside = false;
        }
        HelpingLightManager.KillInDarkness();
    }
}
