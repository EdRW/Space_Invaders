using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Manager
    {
        private DLink pReserve;
        private DLink pActive;
        
        private int mNumReserve;
        private int mNumActive;
        private int growthSize;

        protected Manager(int numNodes = 5, int growthSize = 2)
        {
            // Check params
            Debug.Assert(numNodes >= 0);
            Debug.Assert(growthSize > 0);

            mNumActive = 0;
            pActive = null;
            pReserve = null;
            this.growthSize = growthSize;

            for (int i = 0; i < numNodes; i++)
            {
                DLink newNode = this.CreateNode();
                Debug.Assert(newNode != null);

                DLink.AddToFront(ref pReserve, newNode);
                mNumReserve++;
            }
        }

        protected void BaseSetReserve (int numNodes, int growthSize)
        {
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            this.growthSize = growthSize;

            if (numNodes > this.mNumReserve)
            {
                // Preload the reserve
                this.RefreshReserve(numNodes - this.mNumReserve);
            }
        }

        protected DLink BaseAdd()
        {
            DLink newNode = DLink.RemoveFront(ref pReserve);

            if (newNode == null && growthSize > 0)
            {
                RefreshReserve();
                newNode = DLink.RemoveFront(ref pReserve);
                DLink.AddToFront(ref pActive, newNode);
            }
            else if (newNode != null)
            {
                newNode.Wash();
                DLink.AddToFront(ref pActive, newNode);
            }
            else
            {
                Debug.Assert(false, "Cannot add new node because reserve is empty and growth size is " + growthSize);
            }

            mNumActive++;
            mNumReserve--;

            return newNode;
        }

        protected DLink BaseSpecializedAdd()
        {
            DLink newNode = DLink.RemoveFront(ref pReserve);

            if (newNode == null && growthSize > 0)
            {
                RefreshReserve();
                newNode = DLink.RemoveFront(ref pReserve);
                VirtualSpecializedAdd(ref pActive, newNode);
            }
            else if (newNode != null)
            {
                newNode.Wash();
                VirtualSpecializedAdd(ref pActive, newNode);
            }
            else
            {
                Debug.Assert(false, "Cannot add new node because reserve is empty and growth size is " + growthSize);
            }

            mNumActive++;
            mNumReserve--;

            return newNode;
        }

        protected virtual void VirtualSpecializedAdd(ref DLink pHead, DLink pDLink)
        {
            DLink.AddToFront(ref pHead, pDLink);
        }

        protected void BaseRemove(DLink pDLink)
        {
            // check that pDLink is not null
            Debug.Assert(pDLink != null);
            DLink retiredNode = DLink.Remove(ref pActive, pDLink);
            mNumActive--;

            // check that retiredNode is not null
            Debug.Assert(retiredNode != null);
            DLink.AddToFront(ref pReserve, retiredNode);
            mNumReserve++;
        }

        protected DLink BaseFind(DLink refNode)
        {

            DLink tmpNode = pActive;

            while (tmpNode != null)
            {
                if (CompareTo(tmpNode, refNode)) break;
                tmpNode = tmpNode.GetNext();
            }

            return tmpNode;
        }

        protected void BasePurgeAllNodes()
        {
            DLink pNode;
            while (this.pActive != null)
            {
                pNode = DLink.RemoveFront(ref pActive);
                DLink.AddToFront(ref pReserve, pNode);
                mNumActive--;
                mNumReserve++;
            }

            Debug.Assert(pActive == null);
            Debug.Assert(mNumActive == 0);
        }

        public DLink BaseGetActive()
        {
            return this.pActive;
        }

        protected void BasePrintReport()
        {
            Debug.WriteLine("~ ~ Stats ~ ~\nActive: {0}\nReserve: {1}\nGrowth: {2}", mNumActive, mNumReserve, growthSize);

            Debug.WriteLine("- - Active Nodes - -");
            DLink.PrintList(pActive);

            Debug.WriteLine("");

            Debug.WriteLine("- - Reserve Nodes - -");
            DLink.PrintList(pReserve);

            Debug.WriteLine("\n");
        }

        private void RefreshReserve()
        {
            for (int i = 0; i < growthSize; i++)
            {
                DLink.AddToFront(ref pReserve, CreateNode());
                mNumReserve++;
            }
        }

        private void RefreshReserve(int numNodesToAdd)
        {
            for (int i = 0; i < numNodesToAdd; i++)
            {
                DLink.AddToFront(ref pReserve, CreateNode());
                mNumReserve++;
            }
        }

        protected void BasePushToMemento(ManagerMemento pMemento)
        {
            pMemento.pActive = this.pActive;
            this.pActive = null;

            pMemento.mNumActive = this.mNumActive;
            this.mNumActive = 0;
        }

        protected void BasePullFromMemento(ManagerMemento pMemento)
        {
            this.pActive = pMemento.pActive;
            pMemento.pActive = null;

            this.mNumActive = pMemento.mNumActive;
            pMemento.mNumActive = 0;
        }

        protected abstract DLink CreateNode();

        protected abstract Boolean CompareTo(DLink node1, DLink node2);
    }
}
