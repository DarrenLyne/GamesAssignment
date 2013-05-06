using Microsoft.Xna.Framework;

namespace Steering.Camera_States
{
    class ShowingFleet : State
    {
        public ShowingFleet(Entity entity) : base(entity)
        {
        }

        public override void Enter()
        {
            XNAGame.Instance().Camera.pos = new Vector3(-10, 100, -240);
            XNAGame.Instance().Camera.look = new Vector3(-1, 0, 0);
        }

        public override void Exit()
        {
        }

        bool _done;
        private bool _tester;

        public override void Update(GameTime gameTime)
        {
            if (XNAGame.Instance().Leader.pos.Length() > new Vector3(-10, 20, -330).Length())
            {
                if (!_done)
                {
                    XNAGame.Instance().Camera.pos = new Vector3(120, 150, XNAGame.Instance().Leader.pos.Z);
                    XNAGame.Instance().Camera.look = new Vector3(0, 0, 1);
                    _done = true;
                }
                else
                {
                    if (XNAGame.Instance().Camera.pos.X > -190)
                    {
                        if (!_tester)
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(XNAGame.Instance().Camera.pos.X - 0.2f, 120,
                                                                        XNAGame.Instance().Leader.pos.Z);
                        }
                        else
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(-80, 150, XNAGame.Instance().Camera.pos.Z - 0.3f);
                        }
                    }
                    else
                    {
                        if (!_tester)
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(-80, 150, XNAGame.Instance().Leader.pos.Z + 500);
                            XNAGame.Instance().Camera.look = new Vector3(0, 0, -1);
                            _tester = true;
                        }
                        else
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(-80, 150, XNAGame.Instance().Camera.pos.Z + 0.5f);
                        }
                    }

                }
            }
        }
    }
}
