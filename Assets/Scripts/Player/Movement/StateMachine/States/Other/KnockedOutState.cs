using StateMachine;
using UnityEngine;

namespace Player.Movement.StateMachine.States.Other
{
    public class KnockedOutState : BaseMovementState
    {
        public KnockedOutState(BaseHierarchicalState parentState, float knockOutTime) : base(parentState)
        {
            this.knockOutTime = knockOutTime;
        }
        
        private readonly float knockOutTime;
        private float knockOutTimer;

        public override void Update()
        {
            MovementComponent.AdjustTorque(Quaternion.Euler(0, StateMachine.DesiredRotation, 0));
            
            knockOutTimer += Time.deltaTime;
            
            if (knockOutTimer >= knockOutTime)
                parent.ChangeState(StateFactory.GroundedState(parent));

            base.Update();
        }
    }
}