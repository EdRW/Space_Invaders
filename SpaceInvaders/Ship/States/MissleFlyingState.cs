using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissleFlyingState : ShipShootState
    {
        public override void Handle(Ship pShip)
        {
        }

        public override void ShootMissile(Ship pShip)
        {
            //Debug.WriteLine("Missile Already Flying. Cannot Shoot.");
        }

    }
}