using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Steering.AllyStates
{
    class LeaderIdleState : State
    {
        AIFighter _fighter;
        public LeaderIdleState(Entity entity)
            : base(entity)
        {
        }
        public override void Enter()
        {
            _fighter = (AIFighter)Entity;
            XNAGame.Instance().Leader = _fighter;
            _fighter.targetPos = new Vector3(150, 20, -4000);
            _fighter.pos = new Vector3(-80, 100,  0);
            _fighter.SteeringBehaviours.turnOn(SteeringBehaviours.behaviour_type.seek);
            XNAGame.Instance().Camera.SwicthState(new Camera_States.ShowingFleet(XNAGame.Instance().Camera));
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var i in XNAGame.Instance().Children)
            {
                if (i.GetType() == typeof(ReaperLazer))
                {
                    if ((i.pos - Entity.pos).Length() < 10)
                    {
                        _fighter.SwicthState(new HitState(_fighter));
                    }

                }
            }

            foreach (var entity in XNAGame.Instance().Children.Where(x => x.GetType() == typeof(AIFighter)))
            {
                var fighter = (AIFighter)entity;
                if (fighter.ModelName == "ReaperBossFBX" || fighter.ModelName == "ReaperSovFBX")
                {
                    if ((fighter.pos - Entity.pos).Length() < 771.0f)//770
                    {
                        fighter.SwicthState(new NormandyHideState(Entity));
                        XNAGame.Instance().Leader = fighter;
                        break;
                    }
                }
            }
        }
    }
}
