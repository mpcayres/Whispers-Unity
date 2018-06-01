using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour
{
    MissionManager missionManager;
    public PauseMenuOptions option;
    public enum PauseMenuOptions {BackToGame, BackToMainMenu };
    private void Awake()
    {
        missionManager = GameObject.Find("Player").GetComponent<MissionManager>();
    }
    public void OnClick()
    {
        if (option == PauseMenuOptions.BackToGame)
        {

            missionManager.paused = false;
            this.transform.parent.gameObject.SetActive(false);
        }
        else {
            MissionManager.LoadScene(0);
        }

    }
    
}
