using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class FontManager : Manager
    {
        private static FontManager pInstance = null;
        private Font poCompareNode;

        private FontManager(int numNodes = 2, int growthSize = 1)
            :base(numNodes, growthSize)
        {
            poCompareNode = new Font();
            Debug.Assert(poCompareNode != null);
        }
        ~FontManager()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~FontMan():{0}", this.GetHashCode());
#endif
            this.poCompareNode = null;
            FontManager.pInstance = null;
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
                pInstance = new FontManager(numNodes, growthSize);
            }
        }
        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontManager pMan = FontManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Font pNode = (Font)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchManager.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.pFontSprite != null);
            pSB.Attach(pNode.pFontSprite);

            return pNode;
        }

        public static void Remove(Font pNode)
        {
            FontManager pMan = FontManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Remove from SpriteBatch
            // Find the SBNode
            Debug.Assert(pNode.pFontSprite != null);
            SpriteBaseNode pSBNode = pNode.pFontSprite.GetSBNode();

            // Remove it from the manager
            Debug.Assert(pSBNode != null);
            SpriteBatchManager.Remove(pSBNode);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Font Find(Font.Name name)
        {
            FontManager pMan = FontManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            Font pData = (Font)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            GlyphManager.AddXml(glyphName, assetName, textName);
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("------------------------------- Font Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            FontManager pMan = FontManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static FontManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((Font)node1).name == ((Font)node2).name;
        }
    }
}
