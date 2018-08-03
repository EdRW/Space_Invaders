using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class TimerManager : PriorityDLinkManager
    {
        private static TimerManager pInstance = null;
        private TimeEvent poNodeCompare;
        protected float mCurrTime;

        // temporary data holders used to pass data from BaseSortedAdd into SpecializedAdd
        private static TimeEvent.Name pTmpTimeName;
        private static Command pTmpCommand;
        private static float pTmpDeltaTimeToTrigger;

        private TimerManager(int numNodes = 3, int growthSize = 1)
            :base(numNodes, growthSize)
        {
            poNodeCompare = new TimeEvent();
            Debug.Assert(poNodeCompare != null);
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
                pInstance = new TimerManager(numNodes, growthSize);
            }
        }

        public static TimeEvent Add(TimeEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            // store these parameters to be used when BaseSpecializedAdd calls DerivedInitializeNode
            TimerManager.pTmpTimeName = timeName;
            TimerManager.pTmpCommand = pCommand;
            TimerManager.pTmpDeltaTimeToTrigger = deltaTimeToTrigger;

            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            TimeEvent pNode = (TimeEvent)pMan.BaseSpecializedAdd();
            Debug.Assert(pNode != null);

            return pNode;
        }

        public static void Remove(TimeEvent pNode)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }

        public static void PurgeAllNodes()
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.BasePurgeAllNodes();
        }

        public static TimeEvent Find(TimeEvent.Name name)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            TimeEvent pData = (TimeEvent)pMan.BaseFind(pMan.poNodeCompare);
            Debug.Assert(pData != null);
            return pData;
        }

        public static void Update(float totalTime)
        {
            // Get the instance
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // List needs to be sorted by trigger time
            while (pEvent != null && (pMan.mCurrTime >= pEvent.triggerTime))
            {
                // Difficult to walk a list and remove itself from the list
                // so squirrel away the next event now, use it at bottom of while
                pNextEvent = (TimeEvent)pEvent.GetNext();

                if (pMan.mCurrTime >= pEvent.triggerTime)
                {
                    // call it
                    pEvent.Process();

                    // remove from list
                    pMan.BaseRemove(pEvent);
                }

                // advance the pointer
                pEvent = pNextEvent;
            }
        }

        public static float GetCurrTime()
        {
            // Get the instance
            TimerManager pTimerMan = TimerManager.PrivGetInstance();

            // return time
            return pTimerMan.mCurrTime;
        }

        public static void PushToMemento(TimerMemento pMemento, float currTime)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePushToMemento(pMemento);
            pMemento.mCurrTime = currTime;
        }

        public static void PullFromMemento(TimerMemento pMemento, float currTime)
        {
            float elapsedTime = currTime - pMemento.mCurrTime;
            Debug.Assert(elapsedTime >= 0);

            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePullFromMemento(pMemento);
            
            // update all of the times on the event chain
            TimeEvent pNode = (TimeEvent)pMan.BaseGetActive();
            while(pNode != null)
            {
                pNode.triggerTime += elapsedTime;
                pNode = (TimeEvent)pNode.GetNext();
            }
        }



        public static void PrintReport()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine("--------------------------------- Timer Manager --------------------------------");
            Debug.WriteLine("--------------------------------------------------------------------------------");

            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrintReport();
        }

        

        private static TimerManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        protected override PriorityDLink DerivedCreatePriorityNode()
        {
            PriorityDLink pNode = new TimeEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedInitializeNode(PriorityDLink pPDLink)
        {
            ((TimeEvent)pPDLink).Set(pTmpTimeName, pTmpCommand, pTmpDeltaTimeToTrigger);
        }

        protected override bool CompareTo(DLink node1, DLink node2)
        {
            Debug.Assert(node1 != null);
            Debug.Assert(node2 != null);

            return ((TimeEvent)node1).name == ((TimeEvent)node2).name;
        }
    }
}
