using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Subject
    {
        public DLink pHead;

        protected void baseAttach(Observer observer)
        {
            Debug.Assert(observer != null);
            DLink.AddToFront(ref this.pHead, observer);
            Debug.Assert(this.pHead != null);
        }

        protected void baseDetach(Observer observer)
        {
            Debug.Assert(observer != null);
            DLink.Remove(ref this.pHead, observer);
        }

        public virtual void Notify()
        {
            DLink pNode = this.pHead;

            while (pNode != null)
            {
                // Fire off listener
                ((Observer)pNode).Update(this);

                pNode = pNode.GetNext();
            }

        }

        public virtual void Print()
        {
            DLink pNode = this.pHead;

            while (pNode != null)
            {
                Debug.Write(pNode + ", ");

                pNode = pNode.GetNext();
            }
            Debug.WriteLine("");
        }

        public virtual void PurgeAll()
        {
            DLink pNode = this.pHead;
            DLink pTmp = null;
            while (pHead != null)
            {
                // hold next, delete current, pass next to current
                pTmp = pNode.GetNext();
                DLink.Remove(ref this.pHead, pNode);
                pNode = pTmp;
            }
        }
    }
}
