using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SpriteBatchMan_Link : Manager
    {
        public SpriteBatch_Link poActive = null;
        public SpriteBatch_Link poReserve = null;

        protected SpriteBatchMan_Link(int numNodes = 5, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    class SpriteBatchManager : SpriteBatchMan_Link
    {
        private static SpriteBatchManager pInstance = null;
        private SpriteBatch poCompareNode;

        private SpriteBatchManager(int numNodes = 5, int growthSize = 2)
            :base(numNodes, growthSize)
        {
            poCompareNode = new SpriteBatch();
            Debug.Assert(poCompareNode != null);
        }

        public static void Create(int numNodes = 5, int growthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SpriteBatchManager(numNodes, growthSize);
            }
        }

        public static SpriteBatch Add(SpriteBatch.Name name, int numNodes = 5, int growthSize = 2)
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            SpriteBatch pNode = (SpriteBatch)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, numNodes, growthSize);
            return pNode;
        }

        public static void Remove(SpriteBatch pNode)
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void Remove(SpriteBaseNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteBaseNodeManager pSBNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSBNodeMan != null);
            pSBNodeMan.Remove(pSpriteBatchNode);
        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            SpriteBatch pData = (SpriteBatch)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void Draw()
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSpriteBatch != null)
            {
                if (pSpriteBatch.enabled)
                {
                    SpriteBaseNodeManager pSBNodeMan = pSpriteBatch.GetSBNodeManager();
                    Debug.Assert(pSBNodeMan != null);

                    pSBNodeMan.Draw();
                }
                pSpriteBatch = (SpriteBatch)pSpriteBatch.GetNext();
            }

        }

        // TODO probably remove this method because no longer used. Update handled in game objects
        public static void Update()
        {
            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // walk through the list and render
            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.BaseGetActive();

            while (pSpriteBatch != null)
            {

                SpriteBaseNodeManager pSBNodeMan = pSpriteBatch.GetSBNodeManager();
                Debug.Assert(pSBNodeMan != null);

                SpriteBaseNode pNode = (SpriteBaseNode)pSBNodeMan.BaseGetActive();

                while (pNode != null)
                {
                    pNode.pSpriteBase.Update();

                    pNode = (SpriteBaseNode)pNode.GetNext();
                }

                pSpriteBatch = (SpriteBatch)pSpriteBatch.GetNext();
            }

        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("----------------------------- Sprite Batch Manager -----------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            SpriteBatchManager pMan = SpriteBatchManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static SpriteBatchManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((SpriteBatch)node1).name == ((SpriteBatch)node2).name;
        }
  
    }
}
