﻿using UnityEngine;
using CrowShadowManager;

namespace CrowShadowScenery
{
    public class SideQuestMap : MonoBehaviour
    {

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
