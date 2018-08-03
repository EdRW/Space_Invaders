using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FreeMotionState : ShipMotionState
    {
        public override void Handle(Ship pShip)
        {
        }

        public override void MoveRight(Ship pShip)
        {
            //Debug.WriteLine("Ship Moving Right");
            pShip.x += Math.Abs(pShip.speedX); //Just Incase I set a negative speed
        }

        public override void MoveLeft(Ship pShip)
        {
            //Debug.WriteLine("Ship Moving Left");
            pShip.x -= Math.Abs(pShip.speedX); //Just Incase I set a negative speed
        }
    }
}