using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SideQuestObject : MonoBehaviour
{
    public int numSideQuest = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && CrossPlatformInputManager.GetButtonDown("keyInteract"))
        {
            ExtrasManager.InitSideQuest(numSideQuest);
        }
    }
}
