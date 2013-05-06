using System;
using Microsoft.Xna.Framework;

namespace Steering.AllyStates
{
    class HitState : State
    {
        public HitState(Entity entity)
            : base(entity)
        {

        }

        public override void Enter()
        {
            //Entity.Alive = false;
        }

        public override void Exit()
        {
        }
        float _lastEmmitted = 1.0f;
        public override void Update(GameTime gameTime)
        {
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_lastEmmitted > 0.01f)
            {
                var p = new Particle();
                p.pos = Entity.pos;
                p.pos.X -= 30;
                p.StartPos = Entity.pos;
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

        }
    }
}
