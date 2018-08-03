using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        protected DLink pNext;
        protected DLink pPrev;

        protected DLink()
        {
            pNext = null;
            pPrev = null;
        }

        public DLink GetNext()
        {
            return pNext;
        }

        public DLink GetPrev()
        {
            return pPrev;
        }

        public static void AddToFront(ref DLink pHead, DLink pDLink)
        {
            Debug.Assert(pDLink != null);

            if (pHead == null)
            {
                // first node in empty list
                pHead = pDLink;
                pDLink.pNext = null;
                pDLink.pPrev = null;
            }
            else
            {
                // add to front of non-empty list
                pDLink.pPrev = null;
                pDLink.pNext = pHead;
                pHead.pPrev = pDLink;
                pHead = pDLink;

            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }


        public static DLink RemoveFront(ref DLink pHead)
        {
            if (pHead == null)
            {
                return null;
            }
            else
            {
                if (pHead.pNext != null)
                {
                    pHead.pNext.pPrev = null;
                }
                DLink tmp = pHead;
                pHead = pHead.pNext;

                tmp.pNext = null;
                tmp.pPrev = null;
                return tmp;
            }
        }

        public static DLink Remove(ref DLink pHead, DLink pDLink)
        {
            Debug.Assert(pDLink != null);


            if (pDLink.pNext == null && pDLink.pPrev == null && pHead == pDLink)
            {
                // only node in the list
                pHead = null;
            }
            else if (pDLink.pNext != null && pDLink.pPrev == null && pHead == pDLink)
            {
                // first node in a list with more than 1 node
                pDLink.pNext.pPrev = null;
                pHead = pDLink.pNext;
                pDLink.pNext = null;
            }
            else if (pDLink.pNext == null && pDLink.pPrev != null)
            {
                // last node in a list of more than 1 node
                pDLink.pPrev.pNext = null;
                pDLink.pPrev = null;
            }
            else if (pDLink.pNext != null && pDLink.pPrev != null)
            {
                // node is somewhere in the middle of list
                pDLink.pPrev.pNext = pDLink.pNext;
                pDLink.pNext.pPrev = pDLink.pPrev;
                pDLink.pPrev = null;
                pDLink.pNext = null;
            }
            else
            {
                // should never get to this case
                PrintList(pDLink);
                Debug.Assert(false, "Node may not be part of the List. " + pDLink);
            }

            return pDLink;
        }

        public static void PrintList(DLink pHead)
        {
            DLink tmpNode = pHead;

            while (tmpNode != null)
            {
                Debug.Write(tmpNode + " , ");
                tmpNode = tmpNode.pNext;
            }
        }

        public abstract void Wash();

        public abstract override string ToString();
    }
}
