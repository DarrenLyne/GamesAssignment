using System;
using Microsoft.Xna.Framework;

namespace Steering.AllyStates
{
    class AllieHitState : State
    {
        public AllieHitState(Entity entity)
            : base(entity)
        {

        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        float _lastEmmitted = 1.0f;
        float alive = 0.0f;

        public override void Update(GameTime gameTime)
        {
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_lastEmmitted > 0.01f)
            {
                var p = new Particle();
                p.pos = Entity.pos;
                p.pos.X -= 40;
                p.StartPos = Entity.pos;
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
            alive += timeDelta;
            if (alive > 1)
            {
                Entity.Alive = false;
            }

        }
    }
}
