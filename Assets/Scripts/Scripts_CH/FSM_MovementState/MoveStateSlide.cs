using UnityEngine;
using UnityEngine.UIElements;

public class MoveStateSlide : FSM_Move_State
{
    private float _slideStartSpeed;

    private float _defaultHeight;
    private float _defaultScale;

    public MoveStateSlide(FSM_Movement fsm_movement, PlayerContext playerContext) : base(fsm_movement, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("¬ход в состо€ние Slide");

        _slideStartSpeed = 1.2f * _playerContext.Motor._currentSpeed; 
        
        _defaultHeight = _playerContext.MovementConfig.DefaultHeight;
        _defaultScale = _playerContext.MovementConfig.DefaultScale;

        _playerContext.CharacterController.transform.localScale = new Vector3(1, _defaultScale / _playerContext.MovementConfig.CrouchHeightCoeff, 1); //ѕока нет готовой анимации, уменьшаем кодом нашего "игрока"
        _playerContext.CharacterController.height = _defaultHeight / _playerContext.MovementConfig.CrouchHeightCoeff;

        _playerContext.Camera.SetSensMultiplie(0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("¬ыход в состо€ние Slide");

        _playerContext.Camera.SetSensMultiplie(1f);

        if (CanStand())
        {
            _playerContext.CharacterController.height = _defaultHeight;
            _playerContext.CharacterController.transform.localScale = new Vector3(1, _defaultScale, 1);


        }
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed)
        {
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        if (_playerContext.Input.sneakPressed && _slideStartSpeed == 3)
        {
            _fsm_movement.SetState<MoveStateSneak>();
            return;
        }

        if (_slideStartSpeed == 3)
        {
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        if (!CanStand() && _slideStartSpeed == 3)
        {
            _fsm_movement.SetState<MoveStateSneak>();
            return;
        }

        Vector3 localDir = _playerContext.Motor._currentVelocity.normalized;

        _slideStartSpeed = Mathf.MoveTowards(_slideStartSpeed, 3, 7 * Time.deltaTime);

        _playerContext.Motor.SetHorizontalVelocity(localDir, _slideStartSpeed);

    }

    private bool CanStand()
    {
        Vector3 center = _playerContext.CharacterController.center + _playerContext.CharacterController.transform.position;
        float radius = _playerContext.CharacterController.radius;

        Vector3 bottom = center + Vector3.up * (_defaultScale / _playerContext.MovementConfig.CrouchHeightCoeff); //4 - половина от половины высоты персонажа, 0.41f - небольшое отклонение
        Vector3 top = center + Vector3.up * (_defaultScale - (radius + 0.2f));

        return !Physics.CheckCapsule(bottom, top, radius);
    }
}
