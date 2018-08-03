using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ImageManager_Link : Manager
    {
        public Image_Link poActive = null;
        public Image_Link poReserve = null;

        protected ImageManager_Link(int numNodes = 5, int growthSize = 2)
            : base(numNodes, growthSize)
        {
        }
    }

    public class ImageManager : ImageManager_Link
    {
        private static ImageManager pInstance = null;
        private Image poCompareNode;

        private ImageManager(int numNodes = 5, int growthSize = 2)
            :base(numNodes, growthSize)
        {
            poCompareNode = new Image();
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
                pInstance = new ImageManager(numNodes, growthSize);

                ImageManager.Add(Image.Name.Uninitialized);

                ImageManager.Add(Image.Name.NullObject);
            }
        }

        public static Image Add(Image.Name imageName)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(imageName);

            return pNode;
        }

        public static Image Add(Image.Name imageName, Texture.Name TextureName, float x, float y, float width, float height)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Image pNode = (Image)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            Texture pTexture = TextureManager.Find(TextureName);
            Debug.Assert(pTexture != null);

            pNode.Set(imageName, pTexture, x, y, width, height);

            return pNode;
        }

        public static void Remove(Image pNode)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Image Find(Image.Name name)
        {
            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            Image pData = (Image)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("--------------------------------- Image Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            ImageManager pMan = ImageManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static ImageManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Image();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((Image)node1).name == ((Image)node2).name;
        }
    }
}
