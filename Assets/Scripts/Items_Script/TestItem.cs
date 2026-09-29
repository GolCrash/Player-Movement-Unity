using UnityEngine;

public class TestItem : MonoBehaviour, IInteractable
{
    public string itemName;
    public string itemType;
    public int itemMass;
    public int itemCount;
    public int itemStack;
    public string pathSprite;
    public string pathPrefab;

    public void TakeToInventory(TestItem item, Inventory inventory)
    {
        inventory.items.Add(item);
        Destroy(gameObject);
    }
}
