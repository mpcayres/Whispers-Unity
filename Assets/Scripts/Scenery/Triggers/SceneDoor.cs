using UnityEngine;
using CrowShadowManager;

namespace CrowShadowScenery
{
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
                    GameManager.instance.rpgTalk.NewTalk("Trancada", "TrancadaEnd", 
                        GameManager.instance.rpgTalk.txtToParse, GameManager.instance, "", false);
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
                    GameManager.LoadScene("Sala");
                    break;
                case "DoorToAlley":
                    GameManager.LoadScene("Corredor");
                    break;
                case "DoorToGarden":
                    GameManager.LoadScene("Jardim");
                    break;
                case "DoorToKitchen":
                    GameManager.LoadScene("Cozinha");
                    break;
                case "DoorToMomRoom":
                    GameManager.LoadScene("QuartoMae");
                    break;
                case "DoorToKidRoom":
                    GameManager.LoadScene("QuartoKid");
                    break;
                case "DoorToBathroom":
                    GameManager.LoadScene("Banheiro");
                    break;
                case "DoorToBasement":
                    GameManager.LoadScene("Porao");
                    break;
                default:
                    GameManager.instance.paused = false;
                    break;
            }
        }
    }
}