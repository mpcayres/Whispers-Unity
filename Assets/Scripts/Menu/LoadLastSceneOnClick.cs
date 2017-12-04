using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastSceneOnClick : MonoBehaviour
{

    public void LoadLastScene()
    {
        MissionManager.instance.ContinueGame();
    }

}