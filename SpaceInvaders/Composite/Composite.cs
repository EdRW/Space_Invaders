using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Composite : Component
    {
        public DLink poHeadChild;

        public Composite()
        {
            this.poHeadChild = null;
        }

        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.AddToFront(ref this.poHeadChild, pComponent);
        }

        override public void Remove(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.Remove(ref this.poHeadChild, pComponent);
        }

        
        public override Component GetFirstChild()
        {
            return (Component)this.poHeadChild;
        }

        public override Component GetLastChild()
        {
            DLink pNode = this.poHeadChild;
            DLink pTmp = null;
            while (pNode != null)
            {
                pTmp = pNode;
                pNode = pNode.GetNext();
            }

            return (Component)pTmp;
        }

        public override void Print()
        {
            Debug.Write(this.ToString());
        }

        public override void Wash()
        {
            Debug.Assert(false);

            while (poHeadChild != null)
            {
                DLink pNode = DLink.RemoveFront(ref poHeadChild);
                pNode.Wash();
            }
            base.Wash();
        }

        public override string ToString()
        {
            String returnVal = String.Format("[Composite Component: ({0})", this.GetHashCode());

            DLink pNode = this.poHeadChild;

            while (pNode != null)
            {
                returnVal += pNode.ToString();
                returnVal += ", ";
                pNode = pNode.GetNext();
            }

            return returnVal + " ]\n";
        }

    }
}
