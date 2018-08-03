using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColSubject : Subject
    {
        
        public GameObject pObjA;
        public GameObject pObjB;

        // TODO pointer back to colpair so colpair can be removed when no longer needed
        public ColPair pColpair;

        public ColSubject(ColPair pColpair)
        {
            this.pColpair = pColpair;
            this.pObjB = null;
            this.pObjA = null;
            this.pHead = null;
        }
        ~ColSubject()
        {
            this.pObjB = null;
            this.pObjA = null;
            // TODO  Need to walk and nuke the list
            this.pHead = null;
        }

        public void Attach(ColObserver observer)
        {
            // protection
            Debug.Assert(observer != null);
            base.baseAttach(observer);
        }

        public void Detach(ColObserver observer)
        {
            base.baseDetach(observer);
        }

        public override void Notify()
        {
            base.Notify();
        }

        public override void PurgeAll()
        {
            pObjA = null;
            pObjA = null;
            base.PurgeAll();
        }
    }
}
