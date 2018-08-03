using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Ship_WallRightObserver : ColObserver
    {
        public Ship_WallRightObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("Ship_WallRightObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);

            // Obj B will always be ship
            Ship pShip = (Ship)pColSubject.pObjB;
            pShip.SetMotionState(ShipManager.MotionState.LeftOnly);
        }

        public override string ToString()
        {
            return "[ Ship_WallRightObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }

    }
}