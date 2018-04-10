using UnityEngine;

public class SideQuestMap : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            MissionManager.instance.GameOver();
        }
    }
}
