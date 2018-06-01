using UnityEngine;
using CrowShadowManager;

namespace CrowShadowMenu
{
    public class PauseMenu : MonoBehaviour
    {
        GameManager gameManager;
        public PauseMenuOptions option;
        public enum PauseMenuOptions { BackToGame, BackToMainMenu };
        private void Awake()
        {
            gameManager = GameObject.Find("Player").GetComponent<GameManager>();
        }
        public void OnClick()
        {
            if (option == PauseMenuOptions.BackToGame)
            {

                gameManager.paused = false;
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                GameManager.LoadScene(0);
            }

        }

    }
}