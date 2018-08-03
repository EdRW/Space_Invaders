using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class InputSubject : Subject
    {
        public void Attach(InputObserver observer)
        {
            // protection
            Debug.Assert(observer != null);
            base.baseAttach(observer);
        }

        public void Detach(InputObserver observer)
        {
            base.baseDetach(observer);
        }

        public override void Notify()
        {
            base.Notify();
        }
    }
}
