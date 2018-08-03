using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ShipShootState
    {
        // state()
        public abstract void Handle(Ship pShip);

        // strategy()
        public abstract void ShootMissile(Ship pShip);
    }
}
