using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ColObserver : Observer
    {
        // This class isn't really nessesary.
        // It is just here to enforce the rule that children of this class should 
        // treat the subject as a ColSubject
        public override void Update(Subject pSubject)
        {
            derivedUpdate((ColSubject)pSubject);
        }

        protected abstract void derivedUpdate(ColSubject pColSubject);

    }
}
