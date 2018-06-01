using UnityEngine;

namespace CrowShadowPlayer
{
    public class FarAttackMiniGameObject : FarAttackObject
    {
        public bool miniGameBlocked = true;
        public bool miniGameUnlocked = false;
        public bool achievedGoal = false;

        protected bool init = false;
        protected MiniGameObject miniGameObject;
        protected GameObject fireHolder, fire;

        protected new void Start()
        {
            base.Start();
            miniGameObject = GetComponent<MiniGameObject>();
            fireHolder = transform.Find("FirePaperHolder").gameObject;
            fire = fireHolder.transform.Find("Fire").gameObject;
            if (!miniGameBlocked)
            {
                addObject = false;
                SetMiniGame();
            }
        }

        protected new void Update()
        {
            if (!miniGameUnlocked)
            {
                base.Update();
                if (initPositioning)
                {
                    fire.SetActive(true);
                    fire.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
                else if (attacking && !init)
                {
                    init = true;
                }
                else if (init && !attacking)
                {
                    EndFire();
                }
                else if (achievedGoal && !initAttack && !attacking)
                {
                    if (Inventory.GetCurrentItemType() != item)
                    {
                        EndThrow();
                        EndFire();
                    }
                    switch (player.direction)
                    {
                        case 0:
                            fire.SetActive(true);
                            fire.transform.localPosition = new Vector3(-0.3f, -0.6f, 0f);
                            break;
                        case 1:
                            fire.SetActive(false);
                            break;
                        case 2:
                            fire.SetActive(true);
                            fire.transform.localPosition = new Vector3(0.3f, -0.6f, 0f);
                            break;
                        case 3:
                            fire.SetActive(true);
                            fire.transform.localPosition = new Vector3(-0.3f, -0.6f, 0f);
                            break;
                        default:
                            break;
                    }

                }
            }
            else
            {
                miniGameObject.posFlareX = transform.position.x;
                miniGameObject.posFlareY = transform.position.y;
                achievedGoal = miniGameObject.achievedGoal;
                if (achievedGoal)
                {
                    SetMiniGame(false);
                    ShowFire();
                }
            }
        }

        public void UnlockMiniGame()
        {
            miniGameBlocked = false;
            addObject = false;
            SetMiniGame();
        }

        protected void SetMiniGame(bool e = true)
        {
            miniGameUnlocked = e;
            miniGameObject.enabled = e;
        }

        protected void ShowFire(bool e = true)
        {
            fireHolder.SetActive(e);
        }

        protected void EndFire()
        {
            if (achievedGoal)
            {
                SetMiniGame();
            }
            achievedGoal = false;
            ShowFire(false);
        }
    }
}