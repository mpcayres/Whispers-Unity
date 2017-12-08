using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("TRIGGER: " + gameObject.name);
            MissionManager.instance.mission.AreaTriggered(gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("EXITTRIGGER: " + gameObject.name);
            MissionManager.instance.mission.AreaTriggered("Exit" + gameObject.name);
        }
    }

}