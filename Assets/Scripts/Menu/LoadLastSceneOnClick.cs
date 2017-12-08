using UnityEngine;
 
 public class LoadLastSceneOnClick : MonoBehaviour
 {
 
     public void LoadLastScene()
     {
         MissionManager.instance.ContinueGame();
     }
 
 } 
