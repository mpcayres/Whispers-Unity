using UnityEngine;
using CrowShadowManager;

public class CrowBabiesCollision : MonoBehaviour {
    //ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        //ps = GetComponent<ParticleSystem>();
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
                    GameObject.Find("Tampa").gameObject.GetComponent<ProtectionObject>().DecreaseLife();
                }
                else if (Inventory.GetCurrentItemType() == Inventory.InventoryItems.ESCUDO)
                {
                    GameObject.Find("Escudo").gameObject.GetComponent<ProtectionObject>().DecreaseLife();
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
