using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]

public class Item : ScriptableObject
{
    [System.Serializable]
    public enum Type { HEALTH, ARMOR, WEAPON }
    public string itemName = "New Item";
    public bool isInteractable;
    public bool isDestructable;
    public int value;
    public Type itemType;
    public Sprite icon;
}
