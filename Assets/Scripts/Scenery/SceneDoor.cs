using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour
{
    public bool isOpened = true;

    void OnCollisionEnter2D(Collision2D other)
    {
        print("TRIGGER");
        if (other.gameObject.tag.Equals("Player"))
        {
            if (!isOpened)
            {
                MissionManager.instance.rpgTalk.NewTalk("Trancada", "TrancadaEnd", MissionManager.instance.rpgTalk.txtToParse, MissionManager.instance, "", false);
                MissionManager.instance.scenerySounds2.PlayDoorClosed();
            }
            else
            {
                ChangeScene(other);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        print("STAYTRIGGER");

        if (other.gameObject.tag.Equals("Player") && isOpened && !MissionManager.instance.paused)
        {
            ChangeScene(other);
        }
    }

    private void ChangeScene(Collision2D other)
    {
        MissionManager.instance.paused = true;

        if (other.gameObject.tag.Equals("Player"))
        {
            switch (gameObject.tag)
            {
                case "DoorToLivingroom":
                    SceneManager.LoadScene(1);
                    break;
                case "DoorToAlley":
                    SceneManager.LoadScene(2);
                    break;
                case "DoorToGarden":
                    SceneManager.LoadScene(3);
                    break;
                case "DoorToKitchen":
                    SceneManager.LoadScene(4);
                    break;
                case "DoorToMomRoom":
                    SceneManager.LoadScene(5);
                    break;
                case "DoorToKidRoom":
                    SceneManager.LoadScene(6);
                    break;
                default:
                    MissionManager.instance.paused = false;
                    break;
            }
        }
    }
}