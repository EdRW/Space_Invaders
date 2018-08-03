using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class InputObserver : Observer
    {
        // This class isn't really nessesary.
        // It is just here to enforce the rule that children of this class should 
        // treat the subject as a ColSubject
        public override void Update(Subject pSubject)
        {
            derivedUpdate((InputSubject)pSubject);
        }

        protected abstract void derivedUpdate(InputSubject pInputSubject);
    }
}
