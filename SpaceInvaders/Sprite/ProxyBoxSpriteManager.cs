using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ProxyBoxSpriteManager_Link : Manager
    {
        public ProxyBoxSprite_Link poActive = null;
        public ProxyBoxSprite_Link poReserve = null;

        protected ProxyBoxSpriteManager_Link(int numNodes = 10, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    class ProxyBoxSpriteManager : ProxyBoxSpriteManager_Link
    {
        private static ProxyBoxSpriteManager pInstance = null;
        private ProxyBoxSprite poCompareNode;

        private ProxyBoxSpriteManager(int numNodes = 10, int growthSize = 2)
            : base(numNodes, growthSize)
        {
            poCompareNode = new ProxyBoxSprite();
            Debug.Assert(poCompareNode != null);
        }

        public static void Create(int numNodes = 10, int growthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ProxyBoxSpriteManager(numNodes, growthSize);
            }
        }
        // TODO right now proxies are not being added to the manager
        public static ProxyBoxSprite Add(BoxSprite.Name name)
        {
            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            ProxyBoxSprite pNode = (ProxyBoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }

        public static void Remove(ProxyBoxSprite pNode)
        {
            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static ProxyBoxSprite Find(ProxyBoxSprite.Name name)
        {
            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            ProxyBoxSprite pData = (ProxyBoxSprite)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("----------------------------- ProxyBoxSprite Manager ------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            ProxyBoxSpriteManager pMan = ProxyBoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static ProxyBoxSpriteManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new ProxyBoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((ProxyBoxSprite)node1).name == ((ProxyBoxSprite)node2).name;
        }
    }
}
