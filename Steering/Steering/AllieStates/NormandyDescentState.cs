using Microsoft.Xna.Framework;

namespace Steering.AllyStates
{
    class NormandyDescentState: State
    {
        //Normandy Descents at end
        public NormandyDescentState(Entity entity):base(entity)
        {
        }

        public override void Enter()
        {
            var fighter = (AIFighter)Entity;
            fighter.SteeringBehaviours.turnOffAll();
            fighter.targetPos = new Vector3(-50, -400, -4000);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.arrive);
            fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.EnforceNonPenetrationConstraint);
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
