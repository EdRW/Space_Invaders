using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SBNodeMan_Link : Manager
    {
        public SpriteBaseNode_Link poActive = null;
        public SpriteBaseNode_Link poReserve = null;

        protected SBNodeMan_Link(int numNodes = 3, int growthSize = 1)
            : base(numNodes, growthSize)
        {
        }
    }

    public class SpriteBaseNodeManager : SBNodeMan_Link
    {
        private SpriteBatch.Name name;
        private SpriteBaseNode poCompareNode;

        private SpriteBatch pBackSpriteBatch;

        public SpriteBaseNodeManager(int numNodes = 3, int growthSize = 1)
            :base(numNodes, growthSize)
        {
            poCompareNode = new SpriteBaseNode();
            Debug.Assert(poCompareNode != null);
        }

        public void Set(SpriteBatch.Name name, int numNodes, int growthSize)
        {
            this.name = name;

            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            this.BaseSetReserve(numNodes, growthSize);
        }

        public SpriteBaseNode Attach(SpriteBase pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SpriteBaseNode pSBNode = (SpriteBaseNode)this.BaseAdd();
            Debug.Assert(pSBNode != null);

            // Initialize SpriteBatchNode
            pSBNode.Set(pNode, this);

            return pSBNode;
        }

        public void Draw()
        {
            // walk through the list and render
            SpriteBaseNode pNode = (SpriteBaseNode)this.BaseGetActive();

            while (pNode != null)
            {
                // Assumes someone before here called update() on each sprite
                // Draw me.
                pNode.GetSpriteBase().Render();

                pNode = (SpriteBaseNode)pNode.GetNext();
            }
        }

        public void Remove(SpriteBaseNode pNode)
        {
            Debug.Assert(pNode != null);
            this.BaseRemove(pNode);
        }

        public void PushToMemento(ManagerMemento pMemento)
        {
            BasePushToMemento(pMemento);
        }

        public void PullFromMemento(ManagerMemento pMemento)
        {
            BasePullFromMemento(pMemento);
        }

        public void PurgeAllNodes()
        {
            BasePurgeAllNodes();
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }

        public void SetSpriteBatch(SpriteBatch pSpriteBatch)
        {
            this.pBackSpriteBatch = pSpriteBatch;
        }

        public void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("-------------------------------- SBNode Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            this.BasePrintReport();
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new SpriteBaseNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            // TODO make a proper comparison
            return ((SpriteBaseNode)node1) == ((SpriteBaseNode)node2);
        }
  
    }
}
