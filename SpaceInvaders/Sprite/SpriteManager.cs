using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SpriteManager_Link : Manager
    {
        public Sprite_Link poActive = null;
        public Sprite_Link poReserve = null;

        protected SpriteManager_Link(int numNodes = 10, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    class SpriteManager : SpriteManager_Link
    {
        private static SpriteManager pInstance = null;
        private Sprite poCompareNode;

        private SpriteManager(int numNodes = 10, int growthSize = 2)
            :base(numNodes, growthSize)
        {
            poCompareNode = new Sprite();
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
                pInstance = new SpriteManager(numNodes, growthSize);

                SpriteManager.Add(Sprite.Name.Uninitialized, Image.Name.Uninitialized, 0, 0, Constants.uninitializedWidth, Constants.uninitializedHeight);
                SpriteManager.Add(Sprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            }
        }


        public static Sprite Add(Sprite.Name spriteName, Image.Name imageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            SpriteManager pMan = SpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Sprite pNode = (Sprite)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            Image pImage = ImageManager.Find(imageName);
            Debug.Assert(pImage != null);

            pNode.Set(spriteName, pImage, x, y, width, height, pColor);

            return pNode;
        }

        public static void Remove(Sprite pNode)
        {
            SpriteManager pMan = SpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Sprite Find(Sprite.Name name)
        {
            SpriteManager pMan = SpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            Sprite pData = (Sprite)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("-------------------------------- Sprite Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            SpriteManager pMan = SpriteManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static SpriteManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Sprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((Sprite)node1).name == ((Sprite)node2).name;
        }
    }
}
