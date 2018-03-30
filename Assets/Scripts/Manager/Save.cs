using System.Collections.Generic;

[System.Serializable]
public class Save
{
    // MISSÕES
    public int currentMission = 0;
    public int unlockedMission = 0;

    // INVENTÁRIO
    public List<Inventory.InventoryItems> inventory = new List<Inventory.InventoryItems>();
    public int currentItem = -1;
    public int lifeTampa = 80;

    // COLECIONÁVEIS
    public int numberPages = 0;

    // MISSÕES EXTRAS
    public int sideQuests = 0;

    // ESCOLHAS
    public float pathBird = 0;
    public float pathCat = 0;

    // ESCOLHAS ESPECÍFICAS
    public bool mission1AssustaGato = false;
    public bool mission2ContestaMae = false;
    public bool mission4QuebraSozinho = false;
    public bool mission8BurnCorredor = false;
}