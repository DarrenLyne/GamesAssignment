using System;
using Microsoft.Xna.Framework;
using Steering.Camera_States;

namespace Steering.ReaperStates
{
    class ReaperHitState : State
    {
        public ReaperHitState(Entity entity)
            : base(entity)
        { }
        public override void Enter()
        {
            XNAGame.Instance().Camera.SwicthState(new ShowReaperHit());
        }

        public override void Exit()
        {
        }

        float _lastEmmitted = 1.0f;
        private float timetoshoot;
        public override void Update(GameTime gameTime)
        {
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_lastEmmitted > 0.01f)
            {
                var p = new Particle();
                p.pos = Entity.pos;
                p.pos.Y += 80;
                p.StartPos = Entity.pos;
                p.StartPos.Y += 80;
                p.StartPos.Z -= 5;
                p.Color = new Vector3(204, 0, 0);
                var r = new Random();
                p.velocity = new Vector3((float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f, (float)r.NextDouble() - 0.5f);
                p.velocity.Normalize();
                p.velocity.Y = Math.Abs(p.velocity.Y);
                p.velocity *= (float)r.NextDouble() * 50.0f;
                p.LoadContent();
                XNAGame.Instance().Children.Add(p);
                _lastEmmitted = 0.0f;
            }
            _lastEmmitted += timeDelta;
            timetoshoot += timeDelta;
            if(timetoshoot>10)
            {
                var fighter = (AIFighter) Entity;
                fighter.SwicthState(new ReaperAttackingState(Entity));
            }
           // if(Entity.look.Z > -1.6)
           // {
            //    Matrix x = Matrix.CreateRotationX(-0.001f);
            ////    Entity.look = Vector3.Transform(Entity.look, x);
           // }
        }
    }

}
