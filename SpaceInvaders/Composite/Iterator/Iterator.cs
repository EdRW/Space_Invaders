using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Iterator
    {
        protected Component pCurr;
        protected Component pRoot;

        public abstract Component Next();
        public abstract bool IsDone();
        public abstract Component First();
        public abstract Component Current();

    }
}
