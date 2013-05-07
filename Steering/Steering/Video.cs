using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;


namespace Steering
{
    public class Video2 : Entity
    {
        Video video;
        VideoPlayer player;

        public override void LoadContent()
        {
            video = XNAGame.Instance().Content.Load<Video>("video");
            player = new VideoPlayer();
            player.IsLooped = false;
        }

        public override void Update(GameTime gameTime)
        {
            player.Play(video);
        }

        public override void Draw(GameTime gameTime)
        {
            XNAGame.Instance().SpriteBatch.Draw(player.GetTexture(), new Rectangle(0, 0, video.Width, video.Height), Color.CornflowerBlue);
        }

        public override void UnloadContent()
        {
        }
    }
}
