using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class PriorityDLinkManager : Manager
    {
        // Just here for diagram visualization
        public PriorityDLink poActive = null;
        public PriorityDLink poReserve = null;

        protected PriorityDLinkManager(int numNodes = 5, int growthSize = 2)
            :base(numNodes, growthSize)
        {
        }

        protected override DLink CreateNode()
        {
            // ensures we always create a PriorityDLink
            return DerivedCreatePriorityNode();
        }

        protected override void VirtualSpecializedAdd(ref DLink pHead, DLink pDLink)
        {
            PriorityDLink pPDLink = (PriorityDLink)pDLink;

            // Lets child class set the node's data values so that it can be sorted when added
            DerivedInitializeNode(pPDLink);

            PriorityDLink.SortedAdd(ref pHead, pPDLink);
        }

        protected abstract PriorityDLink DerivedCreatePriorityNode();

        protected abstract void DerivedInitializeNode(PriorityDLink pPDLink);
    }
}
