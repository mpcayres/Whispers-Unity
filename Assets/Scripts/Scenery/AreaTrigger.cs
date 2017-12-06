using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("TRIGGER: " + gameObject.name);
            if (MissionManager.instance.missionSelected == 1 && MissionManager.instance.mission is Mission1)
            {
                ((Mission1)MissionManager.instance.mission).AreaTriggered(gameObject.name);
            }
        }
    }

}