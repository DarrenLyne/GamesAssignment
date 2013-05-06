using System.Linq;
using Microsoft.Xna.Framework;
using Steering.Camera_States;

namespace Steering.AllyStates
{
    class AllyAttackState : State
    {
        float _timeShot = 0.25f;
        public static bool Test;
        public AllyAttackState(Entity entity)
            : base(entity)
        { }


        public override void Enter()
        {
            var fighter = (AIFighter)Entity;
            fighter.SteeringBehaviours.turnOffAll();
            XNAGame.Instance().Camera.SwicthState(new ShowAlliesShooting());
        }

        public override void Exit()
        {
        }

        Vector3 _targetPos = Vector3.Zero;
        public override void Update(GameTime gameTime)
        {
            var fighter = (AIFighter)Entity;
            foreach (AIFighter entity in XNAGame.Instance().Children.Where(x => x.GetType() == typeof(AIFighter)))
            {
                if (entity.ModelName == "ReaperBossFBX" || entity.ModelName == "ReaperSovFBX")
                {
                    if (_targetPos == Vector3.Zero)
                    {
                        _targetPos = entity.pos - Entity.pos;
                    }
                    else
                    {
                        if ((entity.pos - Entity.pos).Length() < _targetPos.Length())
                        {
                            _targetPos = entity.pos - Entity.pos;
                        }
                    }
                }
            }

            var lazer = new AllieLazer();
            lazer.pos = Entity.pos;
            _targetPos.Y += 80f;
            lazer.look = Vector3.Normalize(_targetPos);       
            var timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timeShot += timeDelta;
            if (_timeShot > 0.25f)
            {
                XNAGame.Instance().Children.Add(lazer);
                _timeShot = 0.0f;
            }

            if (!XNAGame.Instance().followNormandy)
            {
                if (_targetPos.Length() > 500)
                {
                    Entity.pos += new Vector3(0, 0, -0.2f);
                }

                foreach (var i in XNAGame.Instance().Children)
                {
                    if (i.GetType() == typeof (ReaperLazer))
                    {
                        if ((i.pos - Entity.pos).Length() < 100)
                        {
                            fighter.SwicthState(new HitState(fighter));
                            i.Alive = false;
                            XNAGame.Instance().Camera.SwicthState(new ShowAllieHit(fighter.pos));
                        }

                    }
                }
                
            }
        }
    }
}
