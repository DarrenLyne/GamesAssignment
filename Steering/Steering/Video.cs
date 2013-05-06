using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;


namespace Steering
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Video2 : Entity
    {
        Video _video;
        VideoPlayer _player;

        public override void LoadContent()
        {
            _video = XNAGame.Instance().Content.Load<Video>("video");
            _player = new VideoPlayer(); 
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            _player.Play(_video);
        }

        public override void Draw(GameTime gameTime)
        {
            XNAGame.Instance().SpriteBatch.Draw(_player.GetTexture(), new Rectangle(0, 0, _video.Width, _video.Height), Color.CornflowerBlue);
            //XNAGame.Instance().SpriteBatch.End(); 
        }

        public override void UnloadContent()
        {
        }
    }
}
