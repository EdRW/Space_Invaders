using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveCollisionPairObserver : ColObserver
    {
        public ColPair pColPair;

        public RemoveCollisionPairObserver(ColPair pColPair)
        {
            this.pColPair = pColPair;
        }

        public RemoveCollisionPairObserver(RemoveCollisionPairObserver pRCPObserver)
        {
            this.pColPair = pRCPObserver.pColPair;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("[Observer({4})] ColPair {5}({3}) - RemoveCollisionPairObserver: {0} vs {1}({2})", pColSubject.pObjA.name, pColSubject.pObjB.name, pColSubject.pObjB.GetHashCode(), this.pColPair.GetHashCode(), this.GetHashCode(), this.pColPair.name);
            //Debug.WriteLine(this.pColPair + "MARKED FOR DEATH");
            
            if (this.pColPair.bMarkForDeath == false)
            {
                //ColPairManager.PrintReport();
                this.pColPair.bMarkForDeath = true;

                //Delay - remove object later
                RemoveCollisionPairObserver pObserver = new RemoveCollisionPairObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            //Debug.WriteLine("*** KILLING " + this.pColPair + " ***");
            ColPairManager.Remove(this.pColPair);
            //ColPairManager.PrintReport();
        }

        public override string ToString()
        {
            return "[ RemoveCollisionPairObserver ({" + this.GetHashCode() + "}) ]";
        }

        public override void Wash()
        {
        }
    }
}
