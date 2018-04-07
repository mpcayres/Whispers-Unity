using UnityEngine;

public class EscapeSideQuest : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            ChangeScene();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player") && !MissionManager.instance.paused)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        MissionManager.instance.paused = true;
        MissionManager.instance.sideQuest.EndSideQuest();
    }
}
