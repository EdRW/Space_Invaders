using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GlyphManager : Manager
    {
        private static GlyphManager pInstance = null;
        private Glyph pRefNode;

        private GlyphManager(int numNodes = 26, int growthSize = 1)
            : base(numNodes, growthSize)
        {
            this.pRefNode = new Glyph();
        }
        ~GlyphManager()
        {
            #if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~GlyphMan():{0}", this.GetHashCode());
            #endif
            this.pRefNode = null;
            GlyphManager.pInstance = null;
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
                pInstance = new GlyphManager(numNodes, growthSize);
            }
        }

        public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Glyph pNode = (Glyph)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, key, textName, x, y, width, height);

            return pNode;
        }


        public static void Remove(Glyph pNode)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static Glyph Find(Glyph.Name name, int key)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.pRefNode.name = name;
            pMan.pRefNode.key = key;

            Glyph pData = (Glyph)pMan.BaseFind(pMan.pRefNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // I'm sure there is a better way to do this... but this works for now
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // have all the data... so now create a glyph
                            //Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            GlyphManager.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }

        private static GlyphManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null, "Create hasn't been called yet");

            return pInstance;
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("------------------------------- Glyph Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new Glyph();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return (((Glyph)node1).name == ((Glyph)node2).name && ((Glyph)node1).key == ((Glyph)node2).key);
        }
    }
}
