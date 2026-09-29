using UnityEngine;

public class PlayerClick
{
    protected readonly PlayerContext _playerContext;

    public float interactionDistance = 3f;
    public float interactionRadius = 0.15f;

    public PlayerClick(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }

    public void Update()
    {
        if (_playerContext.Input.leftClick)
            TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = new Ray(_playerContext.Camera.transform.position, _playerContext.Camera.transform.forward);

        if (!Physics.SphereCast(ray, interactionRadius, out RaycastHit hit, interactionDistance))
            return;

        IInteractable interactable = hit.transform.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        TestItem item = hit.transform.GetComponent<TestItem>();
        interactable.TakeToInventory(item, _playerContext.Inventory);
    }
}
