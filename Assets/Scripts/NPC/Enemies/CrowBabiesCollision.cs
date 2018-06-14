using UnityEngine;
using CrowShadowManager;
using CrowShadowPlayer;

public class CrowBabiesCollision : MonoBehaviour {
    //ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    ProtectionObject tampa, escudo;

    void OnEnable()
    {
        //ps = GetComponent<ParticleSystem>();
        tampa = GameManager.instance.gameObject.transform.Find("Tampa").gameObject.GetComponent<ProtectionObject>();
        escudo = GameManager.instance.gameObject.transform.Find("Escudo").gameObject.GetComponent<ProtectionObject>();
    }

    void OnParticleCollision(GameObject TargetedParticle)
    {
        print("PARTCOL: " + TargetedParticle.tag);
        if (TargetedParticle.tag == "Player")
        {
            if (GameManager.instance.playerProtected)
            {
                //print(Inventory.GetCurrentItemType().ToString());
                if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.TAMPA)
                {
                    tampa.DecreaseLife();
                }
                else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.ESCUDO)
                {
                   escudo.DecreaseLife();
                }
            }
            else
            {
                GameManager.instance.scenerySounds.StopSound();
                GameManager.instance.scenerySounds2.StopSound();
                GameManager.instance.GameOver();
            }
        }
    }

}
