using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveShieldBrickObserver : ColObserver
    {
        private GameObject pShieldBrick;

        public RemoveShieldBrickObserver()
        {

        }

        public RemoveShieldBrickObserver(RemoveShieldBrickObserver pRSObserver)
        {
            this.pShieldBrick = pRSObserver.pShieldBrick;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("RemoveShieldBrickObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
            // No Clue which is which so just check the names
            GameObject pGameObjA = pColSubject.pObjA;
            GameObject pGameObjB = pColSubject.pObjB;
            Debug.Assert(pGameObjA != null);
            Debug.Assert(pGameObjB != null);

            if (pGameObjA.name == GameObject.Name.ShieldBrick)
            {
                this.pShieldBrick = pGameObjA;
            }
            else if(pGameObjB.name == GameObject.Name.ShieldBrick)
            {
                this.pShieldBrick = pGameObjB;
            }
            else { Debug.Assert(false, "Neither of the objects are shields"); }
            

            if (this.pShieldBrick.bMarkForDeath == false)
            {
                this.pShieldBrick.bMarkForDeath = true;

                // Delay - remove object later
                RemoveShieldBrickObserver pObserver = new RemoveShieldBrickObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            // Debug.WriteLine("Removing Brick");
            this.pShieldBrick.Remove();
        }

        public override string ToString()
        {
            return "[ ShipRemoveMissileObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }
    }
}
