using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Inventory.InventoryItems> inventory = new List<Inventory.InventoryItems>();

    public int mission = 0;
    public int currentItem = -1;

    public float pathBird = 0;
    public float pathCat = 0;

    public int lifeTampa = 80;

    public bool mission1AssustaGato = false;
    public bool mission2ContestaMae = false;
    public bool mission4QuebraSozinho = false;
    public bool mission8BurnCorredor = false;
}