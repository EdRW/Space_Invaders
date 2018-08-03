using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Component : DLink
    {
        public Component pParent = null;

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Print();
        public abstract Component GetFirstChild();
        public abstract Component GetLastChild();

        public virtual Component GetParent()
        {
            return this.pParent;
        }

        public virtual Component GetNextSibling()
        {
            return (Component)this.pNext;
        }

        public virtual Component GetPrevSibling()
        {
            return (Component)this.pPrev;
        }

        public override void Wash()
        {
            Debug.Assert(false);
        }
    }
}
