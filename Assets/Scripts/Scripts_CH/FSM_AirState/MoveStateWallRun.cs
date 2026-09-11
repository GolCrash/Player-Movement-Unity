using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState
{
    public class MoveStateWallRun : FSM_Air_State
    {
        private float _wallRunSpeed;
        private float _wallRunStartSpeed;

        public MoveStateWallRun(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
        {
        }

        public override void EnterState()
        {
            _wallRunSpeed = 1.2f * _playerContext.Motor._currentSpeed;
            _wallRunStartSpeed = _wallRunSpeed;

            _playerContext.StateMachine.Ground.isActive = false;

            Debug.Log("Вход в состояние Wall Run");
        }

        public override void ExitState()
        {
            _playerContext.StateMachine.Ground.isActive = true;

            Debug.Log("Выход из состояния Wall Run");
        }

        public override void UpdateState()
        {
            if (_playerContext.Input.jumpPressed)
            {
                _playerContext.Motor.SetHorizontalVelocity(_playerContext.CharacterController.transform.forward - Vector3.back * 1, _playerContext.MovementConfig.WalkSpeed);
                _fsm_air.SetState<AirStateJumping>();
                return;
            }

            if (_wallRunSpeed == 0)
            {
                _fsm_air.SetState<AirStateFall>();
                return;
            }

            if (_wallRunSpeed >= _wallRunStartSpeed)
                _playerContext.Motor.SetVerticalVelocity(Mathf.Sqrt(5f * _playerContext.MovementConfig.Gravity));

            Vector3 localDir = _playerContext.Motor._currentVelocity.normalized;

            _wallRunSpeed = Mathf.MoveTowards(_wallRunSpeed, 0, Time.deltaTime);

            _playerContext.Motor.SetHorizontalVelocity(localDir, _wallRunSpeed);
        }
    }
}
