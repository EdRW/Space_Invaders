using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Ship_WallLeftObserver : ColObserver
    {
        public Ship_WallLeftObserver()
        {

        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("Ship_WallLeftObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);

            // Obj B will always be ship
            Ship pShip = (Ship)pColSubject.pObjB;
            pShip.SetMotionState(ShipManager.MotionState.RightOnly);
        }

        public override string ToString()
        {
            return "[ Ship_WallLeftObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }

    }
}