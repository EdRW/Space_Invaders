using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Leaf : Component
    {
        public Leaf()
        {

        }
        public override void Add(Component c)
        {
            Debug.Assert(false);
        }

        public override void Remove(Component c)
        {
            Debug.Assert(false);
        }

        
        public override Component GetFirstChild()
        {
            return null;
        }

        public override Component GetLastChild()
        {
            return null;
        }

        public override void Print()
        {
            Debug.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return String.Format("Leaf Component: ({0})", this.GetHashCode());
        }

        public override void Wash()
        {
            base.Wash();
        }
    }
}
