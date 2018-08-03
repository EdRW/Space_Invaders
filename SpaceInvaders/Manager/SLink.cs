using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SLink
    {
        public SLink pNext;

        protected SLink()
        {
            this.pNext = null;
        }

        public static void AddToFront(ref SLink pHead, SLink pSLink)
        {
            if (pHead == null)
            {
                // first node in empty list
                pHead = pSLink;
                pSLink.pNext = null;
            }
            else
            {
                // add to front of non-empty list
                pSLink.pNext = pHead;
                pHead = pSLink;
            }
        }

        public static void PrintList(SLink pHead)
        {
            SLink tmpNode = pHead;

            while (tmpNode != null)
            {
                Debug.Write(tmpNode + " , ");
                tmpNode = tmpNode.pNext;
            }
        }

        public abstract override string ToString();
    }
}
