using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowReaperHit : State
    {
        public ShowReaperHit() : base(XNAGame.Instance().Camera)
        {
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos = new Vector3(-80, 80, -1500);
            XNAGame.Instance().Camera.look = new Vector3(0, 0,-1);
        }

        public override void Exit()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
    }
}
