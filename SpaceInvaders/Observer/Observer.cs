using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Observer : DLink
    {
        public abstract void Update(Subject pSubject);

        public virtual void Execute()
        {
            Debug.Assert(false);
        }
    }
}
