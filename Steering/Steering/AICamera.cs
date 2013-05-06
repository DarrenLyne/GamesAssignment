using Microsoft.Xna.Framework;

namespace Steering
{
    public class AICamera : Camera
    {
         public State CurrentState;

        public override void Update(GameTime gameTime)
        {
            if (CurrentState != null)
            {
                CurrentState.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void SwicthState(State newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            
            CurrentState = newState;
            if (newState != null)
            {
                CurrentState.Enter();
            }
        }
    }
}
