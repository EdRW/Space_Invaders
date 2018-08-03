using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class ProxySpriteManager_Link : Manager
    {
        public ProxySprite_Link poActive = null;
        public ProxySprite_Link poReserve = null;

        protected ProxySpriteManager_Link(int numNodes = 10, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    class ProxySpriteManager : ProxySpriteManager_Link
    {
        private static ProxySpriteManager pInstance = null;
        private ProxySprite poCompareNode;

        private ProxySpriteManager(int numNodes = 10, int growthSize = 2)
            :base(numNodes, growthSize)
        {
            poCompareNode = new ProxySprite();
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
                pInstance = new ProxySpriteManager(numNodes, growthSize);
            }
        }
        // TODO right now proxies are not being added to the manager
        public static ProxySprite Add(Sprite.Name name)
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            ProxySprite pNode = (ProxySprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }

        public static void Remove(ProxySprite pNode)
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static ProxySprite Find(ProxySprite.Name name)
        {
            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            ProxySprite pData = (ProxySprite)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("----------------------------- ProxySprite Manager ------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            ProxySpriteManager pMan = ProxySpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static ProxySpriteManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new ProxySprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((ProxySprite)node1).name == ((ProxySprite)node2).name;
        }
    }
}
