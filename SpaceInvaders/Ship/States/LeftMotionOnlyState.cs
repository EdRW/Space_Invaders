using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class LeftMotionOnlyState : ShipMotionState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMotionState(ShipManager.MotionState.Free);
        }

        public override void MoveRight(Ship pShip)
        {
            //Debug.WriteLine("Ship Cannot MoveRight");
        }

        public override void MoveLeft(Ship pShip)
        {
            //Debug.WriteLine("Ship Moving Left");
            pShip.x -= Math.Abs(pShip.speedX); //Just Incase I set a negative speed
            this.Handle(pShip);
        }
    }
}