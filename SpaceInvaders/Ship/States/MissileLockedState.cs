using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissileLockedState : ShipShootState
    {
        public override void Handle(Ship pShip)
        {
        }

        public override void ShootMissile(Ship pShip)
        {
            //Debug.WriteLine("Ship controls are locked");
        }

    }
}