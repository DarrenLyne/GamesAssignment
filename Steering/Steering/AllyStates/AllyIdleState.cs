namespace Steering.AllyStates
{
    class AllyIdleState : State
    {
        public AllyIdleState(Entity entity)
            : base(entity)
        {
        }

        public override void Enter()
        {
            var fighter = (AIFighter)Entity;
            fighter.Leader = XNAGame.Instance().Leader;
            fighter.offset = fighter.pos - XNAGame.Instance().Leader.pos;
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.offset_pursuit);
        }

        public override void Exit()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var fighter = (AIFighter)Entity;
            var leader = (AIFighter)XNAGame.Instance().Leader;
            if (leader.currentState.GetType() == typeof(NormandyHideState))
            {
                fighter.SwicthState(new AllyAttackState(Entity));
            }
        }
    }
}
