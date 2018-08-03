using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SetGameModeObserver : InputObserver
    {
        GameManager.Mode pMode;

        public SetGameModeObserver(GameManager.Mode mode)
        {
            this.pMode = mode;
        }

        protected override void derivedUpdate(InputSubject pInputSubject)
        {
            GameManager.SetActiveGameMode(this.pMode);
        }

        public override string ToString()
        {
            return "[ SetGameModeObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
            throw new NotImplementedException();
        }
    }
}
