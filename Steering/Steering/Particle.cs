using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Steering
{
    class Particle:Sphere
    {
        public Vector3 StartPos;
        Stopwatch watch = new Stopwatch();
        public Particle(): base(1.0f)
        {
            watch.Start();
        }
        public override void Update(GameTime gameTime)
        {
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var gravity = new Vector3(0.0f, -9.8f, 0.0f);
            velocity += gravity * timeDelta;
            pos += velocity * timeDelta;

            if (Math.Abs(pos.Length() - StartPos.Length()) > 20)
            {
                Alive = false;
            }

            if (watch.ElapsedMilliseconds > 500)
            {
                watch.Stop();
                Alive = false;
            }
        }
    }
}
