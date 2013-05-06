using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowAlliesShooting : State
    {
        public ShowAlliesShooting() : base(XNAGame.Instance().Camera)
        {
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos = new Vector3(-380,100,-950);
            XNAGame.Instance().Camera.look = new Vector3(1, 0, 0);
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
