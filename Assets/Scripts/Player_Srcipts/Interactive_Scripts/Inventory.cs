using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<TestItem> items = new List<TestItem>();
    public bool isActive = false;

    private InventoryUI _inventoryUI;
    public GameObject inventory;

    public void Initialized(PlayerContext playerContext)
    {
        _inventoryUI = new InventoryUI(playerContext);
    }

    void Start()
    {

    }

    void Update()
    {
        _inventoryUI?.ActivateInventory();

        inventory.SetActive(isActive);
    }

    public void InventoryEnable()
    { 
    
    }

    public void InventoryDisable()
    {

    }
}
