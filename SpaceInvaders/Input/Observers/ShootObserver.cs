using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShootObserver : InputObserver
    {

        protected override void derivedUpdate(InputSubject pInputSubject)
        {
            Ship pShip = ShipManager.GetShip();
            pShip.ShootMissile();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override void Wash()
        {
            throw new NotImplementedException();
        }
    }
}
