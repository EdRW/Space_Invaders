using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Command
    {
        public TimeEvent.Name name;
        abstract public void Execute(float deltaTime);
    }
}
