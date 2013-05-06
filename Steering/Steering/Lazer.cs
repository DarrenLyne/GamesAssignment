using Microsoft.Xna.Framework;

namespace Steering
{
    class Lazer:Entity
    {

        public override void LoadContent()
        {
        }
        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            const float speed = 3.5f;
            pos += look * speed;

            if (pos.Z < -3000)
            {
                Alive = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Line.DrawLine(pos, pos + look * 10, Color.Red);
        }

    }
}
