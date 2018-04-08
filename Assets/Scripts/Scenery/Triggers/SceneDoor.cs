using UnityEngine;

public class SceneDoor : MonoBehaviour
{
    public bool isOpened = true;

    void OnCollisionEnter2D(Collision2D other)
    {
        //print("TRIGGER");
        if (other.gameObject.tag.Equals("Player"))
        {
            if (!isOpened)
            {
                MissionManager.instance.rpgTalk.NewTalk("Trancada", "TrancadaEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
                MissionManager.instance.scenerySounds2.PlayDoorClosed();
            }
            else
            {
                ChangeScene();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        //print("STAYTRIGGER");
        if (other.gameObject.tag.Equals("Player") && isOpened && !MissionManager.instance.paused)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        MissionManager.instance.paused = true;

        switch (gameObject.tag)
        {
            case "DoorToLivingroom":
                MissionManager.LoadScene(1);
                break;
            case "DoorToAlley":
                MissionManager.LoadScene(2);
                break;
            case "DoorToGarden":
                MissionManager.LoadScene(3);
                break;
            case "DoorToKitchen":
                MissionManager.LoadScene(4);
                break;
            case "DoorToMomRoom":
                MissionManager.LoadScene(5);
                break;
            case "DoorToKidRoom":
                MissionManager.LoadScene(6);
                break;
            case "DoorToBathroom":
                MissionManager.LoadScene(9);
                break;
            case "DoorToBasement":
                MissionManager.LoadScene(11);
                break;
            default:
                MissionManager.instance.paused = false;
                break;
        }
    }
}