using Combat;
using General;
using Player.Movement;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private MovementComponent movementComponent;
        private MovementStateMachine movementStateMachine;
        
        private TimedEnabler kicker;

        private float timeOfJumpRequestEnd;
        
        //Input
        public Vector2 MoveInput { get; private set; }
        public float DesiredRotation { get; private set; }
        public bool IsJumpRequested { get; private set; }

        private void Awake()
        {
            movementComponent = GetComponent<MovementComponent>();
            
            movementStateMachine = new MovementStateMachine(this);
            movementStateMachine.Enter();
            
            kicker = GetComponentInChildren<TimedEnabler>();

            DesiredRotation = transform.rotation.eulerAngles.y;
        }

        private void Update()
        {
            MoveInput = InputHandler.MoveInput;
            
            DesiredRotation += InputHandler.LookInput.Value.x * movementComponent.RotationSpeed * Time.deltaTime;
            
            if (InputHandler.JumpInput)
            {
                IsJumpRequested = InputHandler.JumpInput.Consume();
                timeOfJumpRequestEnd = Time.realtimeSinceStartup + movementComponent.JumpInputBufferDuration;
            }
            else if (IsJumpRequested && Time.realtimeSinceStartup >= timeOfJumpRequestEnd)
            {
                IsJumpRequested = false;
            }
            
            if (InputHandler.KickInput)
            {
                kicker.EnableForTime();
                InputHandler.KickInput.Consume();
            }
        }

        private void FixedUpdate()
        {
            movementStateMachine.Update();
        }

        private void OnDestroy()
        {
            movementStateMachine.Exit();
        }
    }
}