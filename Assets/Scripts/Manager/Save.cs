using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Inventory.DataItems> inventory = new List<Inventory.DataItems>();

    public int mission = 0;
    public int currentItem = -1;
}