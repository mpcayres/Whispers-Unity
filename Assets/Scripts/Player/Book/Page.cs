using UnityEngine;
using CrowShadowManager;

public class Page : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            // Som de pegar página
            Book.AddPage();
            GameManager.instance.UpdateSave();
            Destroy(gameObject);
        }
    }

}
