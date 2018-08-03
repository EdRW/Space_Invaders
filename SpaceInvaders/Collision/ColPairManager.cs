using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColPairMan_MLink : Manager
    {
        public ColPair_Link poActive;
        public ColPair_Link poReserve;
        
        protected ColPairMan_MLink(int numNodes = 13, int growthSize = 3)
            : base(numNodes, growthSize)
        {
        }
    }
    public class ColPairManager : ColPairMan_MLink
    {
        private static ColPairManager pInstance = null;
        private ColPair poCompareNode;

        private ColPair pActiveColPair;

        private ColPairManager(int numNodes = 3, int growthSize = 1)
        : base(numNodes, growthSize)
        {
            this.pActiveColPair = null;
            this.poCompareNode = new ColPair();
            Debug.Assert(poCompareNode != null);
        }

        ~ColPairManager()
        {
            this.pActiveColPair = null;
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new ColPairManager(reserveNum, reserveGrow);
            }

        }


        public static ColPair Add(ColPair.Name colpairName, GameObject treeRootA, GameObject treeRootB)
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            ColPair pColPair = (ColPair)pMan.BaseAdd();
            Debug.Assert(pColPair != null);

            pColPair.Set(colpairName, treeRootA, treeRootB);

            return pColPair;
        }

        public static void Remove(ColPair pNode)
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);

            pMan.BaseRemove(pNode);
        }

        public static void PurgeAllNodes()
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BasePurgeAllNodes();
        }

        public static ColPair Find(ColPair.Name name)
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poCompareNode.name = name;

            ColPair pData = (ColPair)pMan.BaseFind(pMan.poCompareNode);
            Debug.Assert(pData != null);

            return pData;
        }

        public static void Process()
        {
            // get the singleton
            ColPairManager pColPairMan = ColPairManager.PrivGetInstance();

            ColPair pColPair = (ColPair)pColPairMan.BaseGetActive();

            while (pColPair != null)
            {
                // set the current active
                pColPairMan.pActiveColPair = pColPair;

                // do the check for a single pair
                pColPair.Process();

                // advance to next
                pColPair = (ColPair)pColPair.GetNext();
            }
        }

        static public ColPair GetActiveColPair()
        {
            // get the singleton
            ColPairManager pMan = ColPairManager.PrivGetInstance();

            return pMan.pActiveColPair;
        }

        public static void PushToMemento(ManagerMemento pMemento)
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePushToMemento(pMemento);
        }

        public static void PullFromMemento(ManagerMemento pMemento)
        {
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePullFromMemento(pMemento);
        }


        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("-------------------------------- ColPair Manager -------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");
            
            ColPairManager pMan = ColPairManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.WriteLine("ACTIVE COLPAIR: {0}", pMan.pActiveColPair);

            pMan.BasePrintReport();
        }

        private static ColPairManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override DLink CreateNode()
        {
            DLink pNode = new ColPair();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((ColPair)node1).name == ((ColPair)node2).name;
        }   

    }
}
