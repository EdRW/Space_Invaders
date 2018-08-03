using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class BoxSpriteManager_Link : Manager
    {
        public BoxSprite_Link poActive = null;
        public BoxSprite_Link poReserve = null;

        protected BoxSpriteManager_Link(int numNodes = 5, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    class BoxSpriteManager : BoxSpriteManager_Link
    {
        private static BoxSpriteManager pInstance = null;
        private BoxSprite poNodeCompare;

        private BoxSpriteManager(int numNodes = 5, int growthSize = 2)
            :base(numNodes, growthSize)
        {
            poNodeCompare = new BoxSprite();
            Debug.Assert(poNodeCompare != null);
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
                pInstance = new BoxSpriteManager(numNodes, growthSize);

                BoxSpriteManager.Add(BoxSprite.Name.Uninitialized, 0, 0, Constants.uninitializedWidth, Constants.uninitializedHeight);

                BoxSpriteManager.Add(BoxSprite.Name.NullObject, 0, 0, 0, 0);
            }
        }

        public static BoxSprite Add(BoxSprite.Name name, Azul.Color pColor = null)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pColor);

            return pNode;
        }

        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, x, y, width, height, pColor);

            return pNode;
        }

        public static void Remove(BoxSprite pNode)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            BoxSprite pData = (BoxSprite)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("------------------------------ BoxSprite Manager -------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            BoxSpriteManager pMan = BoxSpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static BoxSpriteManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((BoxSprite)node1).name == ((BoxSprite)node2).name;
        }
    }
}
