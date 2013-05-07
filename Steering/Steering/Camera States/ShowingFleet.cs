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

        bool shownfullNormandy;
        private bool shownAllies;

        public override void Update(GameTime gameTime)
        {
            if (XNAGame.Instance().Leader.pos.Length() > new Vector3(-10, 20, -330).Length())//show allies until Normandy reaches certain point
            {
                if (!shownfullNormandy)
                {
                    XNAGame.Instance().Camera.pos = new Vector3(120, 150, XNAGame.Instance().Leader.pos.Z);
                    XNAGame.Instance().Camera.look = new Vector3(0, 0, 1);
                    shownfullNormandy = true;
                }
                else
                {
                    if (XNAGame.Instance().Camera.pos.X > -190)//Show allies until completed
                    {
                        if (!shownAllies)
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(XNAGame.Instance().Camera.pos.X - 0.2f, 120,
                                                                        XNAGame.Instance().Leader.pos.Z);
                        }
                        else
                        {
                            //show allies and reapers
                            XNAGame.Instance().Camera.pos = new Vector3(-80, 150, XNAGame.Instance().Camera.pos.Z - 0.3f);
                        }
                    }
                    else
                    {
                        if (!shownAllies)
                        {
                            XNAGame.Instance().Camera.pos = new Vector3(-80, 150, XNAGame.Instance().Leader.pos.Z + 500);
                            XNAGame.Instance().Camera.look = new Vector3(0, 0, -1);
                            shownAllies = true;
                        }
                    }

                }
            }
        }
    }
}
