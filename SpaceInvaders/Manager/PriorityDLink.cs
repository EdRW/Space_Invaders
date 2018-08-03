using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class PriorityDLink : DLink
    {
        public static void SortedAdd(ref DLink pHead, PriorityDLink pPDLink)
        {
            Debug.Assert(pPDLink != null);

            if (pHead == null)
            {
                // first node in empty list
                pHead = pPDLink;
                pPDLink.pNext = null;
                pPDLink.pPrev = null;
            }
            else
            {
                // add to correct sorted position in list
                PriorityDLink tmpNode = (PriorityDLink)pHead;

                while (tmpNode != null)
                {
                    if (pPDLink.CompareTo(tmpNode) <= 0)
                    {
                        // Add PDLink into list before tmpNode

                        if (tmpNode.pPrev == null)
                        {
                            //tmpNode is the first node, insert pPDLink in front of tmpNode to become the new first node.
                            pPDLink.pNext = tmpNode;
                            pPDLink.pPrev = null;
                            tmpNode.pPrev = pPDLink;
                            pHead = pPDLink;
                        }
                        else if(tmpNode.pPrev != null)
                        {
                            //TmpNode has a node before it, insert pPDLink as a middle node in between TmpNode and the node before it.
                            pPDLink.pNext = tmpNode;
                            pPDLink.pPrev = tmpNode.pPrev;
                            ((PriorityDLink)tmpNode.pPrev).pNext = pPDLink;
                            tmpNode.pPrev = pPDLink;
                        }
                        else
                        {
                            // should never get to this case
                            Debug.Assert(false);
                        }
                        break;
                    }

                    if (tmpNode.pNext == null)
                    {
                        // Add to the end of the list
                        pPDLink.pNext = null;
                        pPDLink.pPrev = tmpNode;
                        tmpNode.pNext = pPDLink;
                        break;
                    }
                    tmpNode = (PriorityDLink)tmpNode.pNext;
                }
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }


        /// <summary>
        /// Returns less than zero if this is less than pPDLink
        /// Returns zero if this equals pPDLink
        /// Returns greater than zero if this greater than pPDLink
        /// </summary>
        /// <returns></returns>
        protected abstract int CompareTo(PriorityDLink pPDLink);
    }
}
