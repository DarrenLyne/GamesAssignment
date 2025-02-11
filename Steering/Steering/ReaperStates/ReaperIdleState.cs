﻿using Steering.Camera_States;

namespace Steering.ReaperStates
{
    class ReaperIdleState : State
    {

        public ReaperIdleState(Entity entity):base(entity)//While Waiting To Notice Allie ships
        {
        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var leader = (AIFighter)XNAGame.Instance().Leader;
            float range = leader.pos.Length() - Entity.pos.Length();

            if(range > -1100)//checks that leader(Normandy) is getting closer to a point
            {
                var fighter = (AIFighter)Entity;
                fighter.SwicthState(new ReaperPreperationState(fighter));
                XNAGame.Instance().Camera.SwicthState(new ShowingReaperPrepeartion());
            }


            foreach (var i in XNAGame.Instance().Children)
            {
                if (i.GetType() == typeof(AllieLazer))
                {
                    if ((i.pos - Entity.pos).Length() < 10)
                    {
                        var fighter = (AIFighter)Entity;
                        fighter.SwicthState(new ReaperHitState(fighter));
                        i.Alive = false;
                    }

                }
            }
        }
    }
}
