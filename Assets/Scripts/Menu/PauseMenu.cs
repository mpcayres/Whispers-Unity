using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;
    public PauseMenuOptions option;
    public enum PauseMenuOptions {BackToGame, BackToMainMenu };
    private void Awake()
    {
        gameManager = GameObject.Find("Player").GetComponent<GameManager>();
    }
    public void OnClick()
    {
        if (option == PauseMenuOptions.BackToGame)
        {

            gameManager.paused = false;
            this.transform.parent.gameObject.SetActive(false);
        }
        else {
            GameManager.LoadScene(0);
        }

    }
    
}
