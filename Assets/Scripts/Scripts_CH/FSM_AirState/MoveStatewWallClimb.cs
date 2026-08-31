using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState
{
    public class MoveStatewWallClimb : FSM_Air_State
    {
        private float _climbSpeed;

        public MoveStatewWallClimb(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
        {
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
            _playerContext.StateMachine.Ground.isActive = true;
        }

        public override void UpdateState()
        {
            if (_playerContext.Input.jumpPressed)
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
                _playerContext.Motor.SetVerticalVelocity(0);
                Debug.Log("ДА");
                return;
            }


            
            _climbSpeed = Mathf.MoveTowards(_climbSpeed, 0, 7 * Time.deltaTime);

            if (_playerContext.Motor._currentVelocity.y > 0)
                _playerContext.Motor.SetVerticalVelocity(_climbSpeed);
        }

        private void BlockedGroundMove()
        {
            _playerContext.StateMachine.Ground.isActive = false;

            _playerContext.Motor.SetHorizontalVelocity(0f);
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
