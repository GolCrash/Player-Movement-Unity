using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState
{
    public class MoveStateWallRun : FSM_Air_State
    {
        private readonly PlayerStateMachine _playerFSM;

        private float _wallRunSpeed;
        private float _wallRunStartSpeed;

        public MoveStateWallRun(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor, PlayerStateMachine playerFSM) : base(fsm_air, playerContext, motor)
        {
            _playerFSM = playerFSM;
        }

        public override void EnterState()
        {
            _wallRunSpeed = 1.2f * _motor._currentSpeed;
            _wallRunStartSpeed = _wallRunSpeed;

            _playerFSM.Ground.isActive = false;

            Debug.Log("Вход в состояние Wall Run");
        }

        public override void ExitState()
        {
            _playerFSM.Ground.isActive = true;

            Debug.Log("Выход из состояния Wall Run");
        }

        public override void UpdateState()
        {
            if (_playerContext.Input.jumpPressed)
            {
                _motor.SetHorizontalVelocity(_playerContext.CharacterController.transform.forward - Vector3.back * 1, _playerContext.MovementConfig.WalkSpeed);
                _fsm_air.SetState<AirStateJumping>();
                return;
            }

            if (_wallRunSpeed == 0)
            {
                _fsm_air.SetState<AirStateFall>();
                return;
            }

            if (_wallRunSpeed >= _wallRunStartSpeed)
                _motor.SetVerticalVelocity(Mathf.Sqrt(5f * _playerContext.MovementConfig.Gravity));

            Vector3 localDir = _motor._currentVelocity.normalized;

            _wallRunSpeed = Mathf.MoveTowards(_wallRunSpeed, 0, Time.deltaTime);

            _motor.SetHorizontalVelocity(localDir, _wallRunSpeed);
        }
    }
}
