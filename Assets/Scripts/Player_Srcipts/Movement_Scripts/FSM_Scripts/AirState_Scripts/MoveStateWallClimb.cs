using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState
{
    public class MoveStateWallClimb : FSM_Air_State
    {
        private readonly PlayerStateMachine _playerFSM;

        private float _climbSpeed;

        public MoveStateWallClimb(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor, PlayerStateMachine playerFSM) : base(fsm_air, playerContext, motor)
        {
            _playerFSM = playerFSM;
        }

        public override void EnterState()
        {
            Debug.Log("Вход в состояние Climb");
            BlockedGroundMove();

            _playerContext.Camera.SetRotationLimit(45f);

            _climbSpeed = 10f;
        }

        public override void ExitState()
        {
            Debug.Log("Выход из состояния Climb");

            _playerContext.Camera.ClearRotationLimit();
            _playerFSM.Ground.isActive = true;
        }

        public override void UpdateState()
        {
            if (_playerContext.Input.jumpPressed)
            {
                _fsm_air.SetState<AirStateFall>();
                return;
            }

            if (_playerContext.Input.jumpPressed && _playerContext.Input.moveInput.y == -1)
            {
                _playerContext.Camera.Rotate180();

               _fsm_air.SetState<AirStateJumping>();
                return;
            }

            if (_playerContext.CharacterController.isGrounded)
            {
                _fsm_air.SetState<AirStateGrounded>();
                return;
            }

            
            if (IsLedge())
            {
                _fsm_air.SetState<MoveStateWallLedge>();
                return;
            }


            
            _climbSpeed = Mathf.MoveTowards(_climbSpeed, 0, 7 * Time.deltaTime);

            if (_motor._currentVelocity.y > 0)
                _motor.SetVerticalVelocity(_climbSpeed);
        }

        private void BlockedGroundMove()
        {
            _playerFSM.Ground.isActive = false;

            _motor.SetHorizontalVelocity(0f);
        }

        private bool IsLedge()
        {
            Vector3 origin = _playerContext.CharacterController.transform.position;

            Vector3 direction = _playerContext.CharacterController.transform.forward;

            float distance = 1f;

            return !Physics.Raycast(origin, direction, distance);
        }
    }
}
