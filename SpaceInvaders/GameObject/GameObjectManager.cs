using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class GameObjectMan_MLink : Manager
    {
        public GameObjectNode_Link poActive = null;
        public GameObjectNode_Link poReserve = null;

        protected GameObjectMan_MLink(int numNodes = 3, int growthSize = 1)
            : base(numNodes, growthSize)
        {
        }
    }

    class GameObjectManager : GameObjectMan_MLink
    {
        private static GameObjectManager pInstance = null;
        private GameObjectNode poNodeCompare;
        private NullGameObject poNullGameObject;

        private GameObjectManager(int numNodes = 3, int growthSize = 1)
            :base(numNodes, growthSize)
        {
            poNodeCompare = new GameObjectNode();
            Debug.Assert(poNodeCompare != null);

            this.poNullGameObject = new NullGameObject();
            Debug.Assert(poNullGameObject != null);

            this.poNodeCompare.pGameObj = this.poNullGameObject;
        }

        public static void Create(int numNodes = 3, int growthSize = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(numNodes > 0);
            Debug.Assert(growthSize > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameObjectManager(numNodes, growthSize);
            }
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static void Remove(GameObjectNode pNode)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void PurgeAllNodes()
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // TODO this will not remove  the game objects properly or safely
            pMan.BasePurgeAllNodes();
        }

        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // pNode may be part of a tree. If so, find it's root.
            // The root will be on the GameObjectManager's Linked list
            GameObject pRoot = null;
            GameObject pTmp = pNode;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)pTmp.GetParent();
            }

            Debug.Assert(pRoot != null);

            // Now that we have the root of pNode's tree, or we have pNode if it has no parent
            // lets find that node on the GameObjectManager's Linked list
            GameObjectNode pTree = (GameObjectNode)pMan.BaseGetActive();

            while (pTree != null)
            {
                if (pTree.pGameObj == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.GetNext();
            }

            // pTree should now be holding the node that matches Root
            Debug.Assert(pTree != null);

            // we shouldn't kills nodes with families right?
            // we aren't monsters
            Debug.Assert(pNode.GetFirstChild() == null);

            // check to see if pTree is just holding pNode or if
            // it is holding a tree containing pNode
            GameObject pNodeParent = (GameObject)pNode.GetParent();
            if (pTree.pGameObj == pNode)
            {
                // pNode is not part of a tree so just remove it.
                Debug.Assert(pNodeParent == null);
                Remove(pTree);
            }
            else
            {
                // pNode is a part of a tree so we'll use the node's
                // parent composite remove method to remove it.
                Debug.Assert(pNodeParent != null);
                pNodeParent.Remove(pNode);
            }
        }

        public static void NonTreeRemove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pGameObjNode = (GameObjectNode)pMan.BaseGetActive();

            while (pGameObjNode != null)
            {
                if (pGameObjNode.pGameObj == pNode)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pGameObjNode = (GameObjectNode)pGameObjNode.GetNext();
            }

            Debug.Assert(pGameObjNode != null);
            Remove(pGameObjNode);
        }

        public static GameObject Find(GameObject.Name name)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.name = name;

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.pGameObj;
        }

        public static GameObject UnsafeFind(GameObject.Name name)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.name = name;

            GameObjectNode pNode = (GameObjectNode)pMan.BaseFind(pMan.poNodeCompare);

            GameObject pGameObj = null;
            if (pNode != null) pGameObj = pNode.pGameObj;

            return pGameObj;
        }

        public static void Update()
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.BaseGetActive();

            while (pNode != null)
            {
                // Update the node
                Debug.Assert(pNode.pGameObj != null);

                pNode.pGameObj.Update();

                pNode = (GameObjectNode)pNode.GetNext();
            }
        }

        public static void PushToMemento(ManagerMemento pMemento)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePushToMemento(pMemento);
        }

        public static void PullFromMemento(ManagerMemento pMemento)
        {
            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePullFromMemento(pMemento);
        }

        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("------------------------------ Game Object Manager -----------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            GameObjectManager pMan = GameObjectManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        private static GameObjectManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((GameObjectNode)node1).pGameObj.GetName() == ((GameObjectNode)node2).pGameObj.GetName();
        }
    }
}
