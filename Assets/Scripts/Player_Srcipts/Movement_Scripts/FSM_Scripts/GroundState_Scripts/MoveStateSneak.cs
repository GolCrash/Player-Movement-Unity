using UnityEngine;

public class MoveStateSneak : FSM_Move_State
{
    private float _defaultHeight;
    private float _defaultScale;

    private Vector3 inputDir;
    private Vector3 worldDir;

    public MoveStateSneak(FSM_Movement fsm_movement, PlayerContext playerContext, PlayerMotor motor) : base(fsm_movement, playerContext, motor)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Sneak");

        _defaultHeight = _playerContext.MovementConfig.DefaultHeight;
        _defaultScale = _playerContext.MovementConfig.DefaultScale;

        _playerContext.CharacterController.transform.localScale = new Vector3(1, _defaultScale / _playerContext.MovementConfig.CrouchHeightCoeff, 1); //Пока нет готовой анимации, уменьшаем кодом нашего "игрока"
        _playerContext.CharacterController.height = _defaultHeight / _playerContext.MovementConfig.CrouchHeightCoeff;

        _playerContext.MovementConfig.CanJump = false;
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Sneak");

        _playerContext.CharacterController.height = _defaultHeight;
        _playerContext.CharacterController.transform.localScale = new Vector3(1, _defaultScale, 1); //Пока нет готовой анимации, уменьшаем кодом нашего "игрока"

        _playerContext.MovementConfig.CanJump = true;
    }

    public override void UpdateState()
    {
        Vector3 origin = _playerContext.CharacterController.bounds.center;
        
        if (!_playerContext.Input.sneakPressed && CanStand())
        { 
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        inputDir = new Vector3(_playerContext.Input.moveInput.x, 0, _playerContext.Input.moveInput.y).normalized;

        worldDir = _playerContext.CharacterController.transform.TransformDirection(inputDir);

        _motor.SetHorizontalVelocity(worldDir, _playerContext.MovementConfig.SneakSpeed);
    }

    private bool CanStand()
    {
        Vector3 center = _playerContext.CharacterController.center + _playerContext.CharacterController.transform.position;
        float radius = _playerContext.CharacterController.radius;

        Vector3 bottom = center + Vector3.up * (_defaultScale / _playerContext.MovementConfig.CrouchHeightCoeff); //4 - половина от половины высоты персонажа, 0.41f - небольшое отклонение
        Vector3 top = center + Vector3.up * (_defaultScale - (radius + 0.2f));

        return !Physics.CheckCapsule(bottom, top,  radius);
    }
}
