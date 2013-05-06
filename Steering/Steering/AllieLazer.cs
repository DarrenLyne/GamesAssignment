using Microsoft.Xna.Framework;

namespace Steering
{
    class AllieLazer : Lazer
    {
        public override void Draw(GameTime gameTime)
        {
            Line.DrawLine(pos, pos + look * 10, Color.Blue);
        }
    }
}
