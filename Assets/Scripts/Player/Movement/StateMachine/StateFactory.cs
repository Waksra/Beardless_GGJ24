using Player.Movement.StateMachine.States.Airborne;
using Player.Movement.StateMachine.States.General;
using Player.Movement.StateMachine.States.Grounded;
using Player.Movement.StateMachine.States.Other;
using StateMachine;

namespace Player.Movement.StateMachine
{
    public static class StateFactory
    {
        //Grounded
        public static GroundedState GroundedState(BaseHierarchicalState parent)
        {
            return new GroundedState(parent);
        }

        //Airborne
        public static AirborneState AirborneState(BaseHierarchicalState parent)
        {
            return new AirborneState(parent);
        }
        
        //General
        public static IdleState IdleState(BaseHierarchicalState parent)
        {
            return new IdleState(parent);
        }
        
        public static MoveState MoveState(BaseHierarchicalState parent)
        {
            return new MoveState(parent);
        }
        
        //Other
        public static KnockedOutState KnockedOutState(BaseHierarchicalState parent, float knockOutTime)
        {
            return new KnockedOutState(parent, knockOutTime);
        }
    }
}