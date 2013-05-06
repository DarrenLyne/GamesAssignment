using Steering.Camera_States;

namespace Steering.AllyStates
{
    class NormandyHideState : State
    {
        float timeShot;
        public NormandyHideState(Entity entity) : base(entity) { }
        public override void Enter()
        {
            var fighter = (AIFighter)Entity;
            fighter.SteeringBehaviours.turnOffAll();
        }

        public override void Exit()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeShot += timeDelta;
            if (timeShot >19f)
            {
                if(!XNAGame.Instance().followNormandy)
                {
                    timeShot = 0.0f;
                    var fighter = (AIFighter)Entity;
                    fighter.SwicthState(new NormandyDescentState(Entity));
                    XNAGame.Instance().Camera.SwicthState(new ShowNormandyDescent(Entity.pos));
                }
            }

        }
    }
}
