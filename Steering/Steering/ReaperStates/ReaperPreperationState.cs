using Microsoft.Xna.Framework;
using Steering.AllyStates;

namespace Steering.ReaperStates
{
    class ReaperPreperationState : State
    {
        public ReaperPreperationState(Entity entity)
            : base(entity)
        {
        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Entity.pos.Z < -1875)
            {
               // M//atrix x = Matrix.CreateRotationX(-0.001f);
               // Entity.look = Vector3.Transform(Entity.look, x);
                Entity.pos = new Vector3(Entity.pos.X, Entity.pos.Y, Entity.pos.Z+0.15f);
            }

            foreach (var i in XNAGame.Instance().Children)
            {
                if (i.GetType() == typeof(AllieLazer))
                {
                    if ((i.pos - Entity.pos).Length() < 100)
                    {
                        var fighter = (AIFighter)Entity;
                        fighter.SwicthState(new ReaperHitState(fighter));
                        i.Alive = false;
                    }

                }
            }
        }
    }
}
