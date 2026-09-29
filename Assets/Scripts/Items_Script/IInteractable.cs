using static UnityEditor.Progress;

public interface IInteractable
{
	void TakeToInventory(TestItem item, Inventory inventory);
}