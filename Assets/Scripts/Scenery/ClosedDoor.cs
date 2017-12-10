using UnityEngine;
using System.Collections;

public class ClosedDoor : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag.Equals("Player"))
        MissionManager.instance.rpgTalk.NewTalk("Trancada", "TrancadaEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
        MissionManager.instance.scenerySounds2.PlayDoorClosed();
    }
}