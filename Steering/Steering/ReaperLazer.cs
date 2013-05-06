using Microsoft.Xna.Framework;

namespace Steering
{
    class ReaperLazer : Lazer
    {
        public override void Draw(GameTime gameTime)
        {
            Line.DrawLine(pos, pos + look * 10, Color.Red);
        }
    }
}
