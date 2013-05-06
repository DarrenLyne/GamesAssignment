using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowNormandyDescent: State
    {
        private readonly Vector3 _cameraPos;
        public ShowNormandyDescent(Vector3 pos)
            : base(XNAGame.Instance().Camera)
        {
            _cameraPos = pos;
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos = _cameraPos;
            XNAGame.Instance().Camera.pos.Z+= 150;
            XNAGame.Instance().Camera.look = new Vector3(0,0,-1);
            XNAGame.Instance().followNormandy = true;
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            XNAGame.Instance().Camera.pos.Z -= 0.2f;
        }
    }
}
