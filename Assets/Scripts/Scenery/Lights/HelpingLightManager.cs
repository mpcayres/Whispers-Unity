using UnityEngine;

public class HelpingLightManager : MonoBehaviour
{
    private static GameObject[] lights;

    void Update()
    {
    }

    public static void KillInDarkness()
    {
        foreach (GameObject light in lights)
        {
            if (light.GetComponent<HelpingLight>().playerInside)
            {
                return;
            } 
        }
        MissionManager.instance.GameOver();
    }
}
