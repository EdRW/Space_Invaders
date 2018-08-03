using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMissileReadyObserver : ColObserver
    {
        public ShipMissileReadyObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("ShipMissileReadyObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);

            Ship pShip = ShipManager.GetShip();
            pShip.SetShootState(ShipManager.ShootState.MissileReady);
        }

        public override string ToString()
        {
            return "[ ShipMissileReadyObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }

    }
}
