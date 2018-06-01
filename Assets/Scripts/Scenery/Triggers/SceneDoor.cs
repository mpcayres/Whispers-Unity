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
                GameManager.instance.rpgTalk.NewTalk("Trancada", "TrancadaEnd", GameManager.instance.rpgTalk.txtToParse, GameManager.instance, "", false);
                GameManager.instance.scenerySounds2.PlayDoorClosed();
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
        if (other.gameObject.tag.Equals("Player") && isOpened && !GameManager.instance.paused)
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        GameManager.instance.paused = true;

        switch (gameObject.tag)
        {
            case "DoorToLivingroom":
                GameManager.LoadScene(1);
                break;
            case "DoorToAlley":
                GameManager.LoadScene(2);
                break;
            case "DoorToGarden":
                GameManager.LoadScene(3);
                break;
            case "DoorToKitchen":
                GameManager.LoadScene(4);
                break;
            case "DoorToMomRoom":
                GameManager.LoadScene(5);
                break;
            case "DoorToKidRoom":
                GameManager.LoadScene(6);
                break;
            case "DoorToBathroom":
                GameManager.LoadScene(9);
                break;
            case "DoorToBasement":
                GameManager.LoadScene(11);
                break;
            default:
                GameManager.instance.paused = false;
                break;
        }
    }
}