using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowAllieHit : State
    {
        private readonly Vector3 _pos;
        public ShowAllieHit(Vector3 position) : base(XNAGame.Instance().Camera)
        {
            _pos = position;
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos.Z = _pos.Z - 150;
            XNAGame.Instance().Camera.pos.X = _pos.X - 30;
            XNAGame.Instance().Camera.look = new Vector3(0,0,1);
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
