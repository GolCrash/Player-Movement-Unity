using UnityEngine;

public class PlayerIneractive : MonoBehaviour
{
    private PlayerContext _playerContext;
    private PlayerClick _playerClick;

    public void Initialize(PlayerContext context)
    {
        _playerContext = context;

        _playerClick = new PlayerClick(context);
    }

    public void Update()
    {
        _playerClick.Update();
    }
}