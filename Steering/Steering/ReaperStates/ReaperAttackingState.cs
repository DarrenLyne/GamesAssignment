using System.Linq;
using Microsoft.Xna.Framework;

namespace Steering.ReaperStates
{
    class ReaperAttackingState:State
    {
        float timeShot = 0.25f;
        public ReaperAttackingState(Entity entity):base(entity)
        {
        }

        public override void Enter()
        {
            var fighter = (AIFighter)Entity;
            fighter.Leader = XNAGame.Instance().Leader;
        }

        public override void Exit()
        {
        }

        public override void Update(GameTime gameTime)
        {
            var fighter= (AIFighter)Entity;

            var targetPos = Vector3.Zero;
            foreach (AIFighter entity in XNAGame.Instance().Children.Where(x => x.GetType() == typeof(AIFighter)))
            {
                if (entity.ModelName == "cerberus" || entity.ModelName == "GethDread" ||
                    entity.ModelName == "Everest Class Dreadnaught" || entity.ModelName == "AllianceFighter")// check each AI Fighter for allie ships
                {
                    if (targetPos == Vector3.Zero)
                    {
                        targetPos = entity.pos - Entity.pos;
                    }
                    else
                    {
                        if ((entity.pos - entity.pos).Length() < targetPos.Length())
                        {   //get closest target pos
                            targetPos = entity.pos - Entity.pos;
                        }
                    }
                }
            }
            
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeShot += timeDelta;
            if (timeShot > 0.25f)
            {
                var lazer = new ReaperLazer();
                lazer.pos = Entity.pos;
                lazer.pos.Y += 40;
                lazer.look = Vector3.Normalize(targetPos);
                lazer.speed = 4.2f;
                XNAGame.Instance().Children.Add(lazer);
                timeShot = 0.0f;
            }
        }

    }
}
