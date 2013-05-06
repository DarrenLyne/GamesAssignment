using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowingReaperPrepeartion : State
    {
        public ShowingReaperPrepeartion()
            : base(XNAGame.Instance().Camera)
        {
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos = new Vector3(-350, 25, -1750);
            XNAGame.Instance().Camera.look = new Vector3(1, 0, -0.8f);
        }

        public override void Exit()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
