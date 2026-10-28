using Unity.VisualScripting;

public class InventoryUI
{
    private PlayerContext _playerContext;
    private Inventory _inventory;

    public InventoryUI(PlayerContext playerContext)
    {
        _playerContext = playerContext;
        _inventory = playerContext.Inventory;
    }

    public void ActivateInventory()
    {
        if (!_playerContext.Input.activeInventory)
            return;

        _inventory.isActive = !_inventory.isActive;
        

    }
}