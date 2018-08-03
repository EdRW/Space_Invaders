using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipRemoveMissileObserver : ColObserver
    {
        public ShipRemoveMissileObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("ShipRemoveMissileObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
            // Missle will always be pObjA
            GameObject pGameObjA = pColSubject.pObjA;
            GameObject pGameObjB = pColSubject.pObjB;
            Debug.Assert(pGameObjA != null);
            Debug.Assert(pGameObjB != null);

            Missile pMissile = null;
            if (pGameObjA.name == GameObject.Name.Missle)
            {
                pMissile = (Missile)pGameObjA;
            }
            else if (pGameObjB.name == GameObject.Name.Missle)
            {
                pMissile = (Missile)pGameObjB;
            }
            else { Debug.Assert(false, "Neither of the objects are Missle"); }

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver();
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the ShipManager deal with this... 
            // Debug.WriteLine("~~~~ {0} KILLING Missile ~~~~~", this);
            ShipManager.DeactivateMissile();
        }

        public override string ToString()
        {
            return "[ ShipRemoveMissileObserver ( " + this.GetHashCode() + " ) ]";
        }

        public override void Wash()
        {
        }
    }
}
