using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public List<Item> items = new List<Item>();

    public void addToInventory(Item item)
    {
        items.Add(item);
    }
    public void removeFromInventory(Item item)
    {
        items.Remove(item);
    }

}