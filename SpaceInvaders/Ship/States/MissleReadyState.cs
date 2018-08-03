using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MissleReadyState : ShipShootState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetShootState(ShipManager.ShootState.MissileFlying);
        }

        public override void ShootMissile(Ship pShip)
        {
            //Debug.WriteLine("Shooting Missile!!!");
            ShipManager.ActivateMissile();
            SoundManager.PlaySound(Sound.Name.Shoot);

            this.Handle(pShip);
        }

    }
}