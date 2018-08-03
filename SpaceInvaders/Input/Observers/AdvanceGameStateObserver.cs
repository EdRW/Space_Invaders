using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AdvanceGameStateObserver : InputObserver
    {
        protected override void derivedUpdate(InputSubject pInputSubject)
        {
            AdvanceGameStateObserver pObserver = new AdvanceGameStateObserver();
            DelayedObjectManager.Attach(pObserver);
        }

        public override string ToString()
        {
            return "[ AdvanceGameStateObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            GameManager.CleanUp();
        }
    }
}
