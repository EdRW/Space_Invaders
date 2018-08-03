using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class TextureManager_Link : Manager
    {
        public Texture_Link poActive = null;
        public Texture_Link poReserve = null;

        protected TextureManager_Link(int numNodes = 2, int growthSize = 1)
            :base(numNodes, growthSize)
        {
        }
    }

    public class TextureManager : TextureManager_Link
    {
        private static TextureManager pInstance = null;
        private Texture poCompareNode;

        private TextureManager(int numNodes = 2, int growthSize = 1)
            :base(numNodes, growthSize)
        {
            poCompareNode = new Texture();
            Debug.Assert(poCompareNode != null);
        }

        public static void Create(int numNodes = 2, int growthSize = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureManager(numNodes, growthSize);

                TextureManager.Add(Texture.Name.NullObject);
                TextureManager.Add(Texture.Name.Uninitialized);
            }
        }

        public static Texture Add(Texture.Name name)
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name);

            return pNode;
        }

        public static void Remove(Texture pNode)
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Texture Find(Texture.Name name)
        {
            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            Texture pData = (Texture)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("------------------------------- Texture Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            TextureManager pMan = TextureManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static TextureManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((Texture)node1).name == ((Texture)node2).name;
        }
    }
}
