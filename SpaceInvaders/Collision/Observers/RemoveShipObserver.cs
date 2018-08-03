using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShipObserver : ColObserver
    {
        public RemoveShipObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("RemoveShipObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
            // Missle will always be pObjA
            GameObject pGameObjA = pColSubject.pObjA;
            GameObject pGameObjB = pColSubject.pObjB;
            Debug.Assert(pGameObjA != null);
            Debug.Assert(pGameObjB != null);

            Ship pShip = null;
            if (pGameObjA.name == GameObject.Name.Ship)
            {
                pShip = (Ship)pGameObjA;
            }
            else if (pGameObjB.name == GameObject.Name.Ship)
            {
                pShip = (Ship)pGameObjB;
            }
            else { Debug.Assert(false, "Neither of the objects are Ship"); }

            if (pShip.bMarkForDeath == false)
            {
                pShip.bMarkForDeath = true;

                // Delay - remove object later
                //RemoveShipObserver pObserver = new RemoveShipObserver();
                //DelayedObjectManager.Attach(pObserver);
                //Debug.WriteLine("~~~~ {0} MARKING SHIP FOR DEATH ~~~~~", this);
            }
        }

        public override void Execute()
        {
            // Let the ShipManager deal with this... 
            // Debug.WriteLine("~~~~ {0} KILLING Ship ~~~~~", this);
            //ShipManager.DeactivateMissile();
            //Debug.Assert(false, "Ship is Dead. YOU LOSE!");
            //GameManager.HandleEndOfPlayerTurn();
        }

        public override string ToString()
        {
            return "[ RemoveShipObserver ( " + this.GetHashCode() + " ) ]";
        }

        public override void Wash()
        {
        }
    }
}
