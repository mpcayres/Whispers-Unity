using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FarAttackMiniGameObject : FarAttackObject {
    public bool miniGameUnlocked = false;
    public bool achievedGoal = false;

    protected MiniGameObject miniGameObject;

    protected new void Start()
    {
        base.Start();
        miniGameObject = GetComponent<MiniGameObject>();
        UnlockMiniGame();
    }

    protected new void Update()
    {
        if (!miniGameUnlocked || (miniGameUnlocked && achievedGoal))
        {
            base.Update();
        }
        else
        {
            achievedGoal = miniGameObject.achievedGoal;
            if (achievedGoal)
            {
                UnlockMiniGame(false);
            }
        }
    }

    public void UnlockMiniGame(bool e = true)
    {
        miniGameUnlocked = e;
        miniGameObject.enabled = e;
    }
}
