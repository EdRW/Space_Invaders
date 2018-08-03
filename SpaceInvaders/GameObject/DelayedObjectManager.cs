using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class DelayedObjectManager
    {
        private static DelayedObjectManager instance = null;
        private DLink pHead;

        public DelayedObjectManager()
        {
            this.pHead = null;
        }

        public static void Attach(Observer pObserver)
        {
            // protection
            Debug.Assert(pObserver != null);

            DelayedObjectManager pDelayMan = DelayedObjectManager.privGetInstance();
            DLink.AddToFront(ref pDelayMan.pHead, pObserver);
        }

        public static void PurgeAll()
        {
            DelayedObjectManager pDelayMan = DelayedObjectManager.privGetInstance();
            // remove all from list
            while (pDelayMan.pHead != null)
            {
                DLink.RemoveFront(ref pDelayMan.pHead);
            }
        }

        static public void Process()
        {
            DelayedObjectManager pDelayMan = DelayedObjectManager.privGetInstance();

            Observer pObserver = (Observer)pDelayMan.pHead;

            while (pObserver != null)
            {
                // Fire off remove method
                pObserver.Execute();

                pObserver = (Observer)pObserver.GetNext();
            }

            // remove all from list
            while (pDelayMan.pHead != null)
            {
                DLink.RemoveFront(ref pDelayMan.pHead);
            }       
        }

        private static DelayedObjectManager privGetInstance()
        {
            // Do the initialization
            if (instance == null)
            {
                instance = new DelayedObjectManager();
            }

            return instance;
        }
    }

}

