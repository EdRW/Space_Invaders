using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GridState
    {
        public abstract void Handle(InvaderGrid pGrid);
        public abstract void Move(InvaderGrid pGrid);
    }
}
