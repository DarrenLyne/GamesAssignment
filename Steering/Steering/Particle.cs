using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Steering
{
    class Particle:Sphere
    {
        public Vector3 StartPos;
        public Particle(): base(1.0f)
        {
        }
        public override void Update(GameTime gameTime)
        {
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var gravity = new Vector3(0.0f, -9.8f, 0.0f);
            velocity += gravity * timeDelta;
            pos += velocity * timeDelta;

            if (Math.Abs(pos.Length() - StartPos.Length()) >120)
            {
                Alive = false;
            }
        }
    }
}
