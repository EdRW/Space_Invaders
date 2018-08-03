using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RightMotionOnlyState : ShipMotionState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetMotionState(ShipManager.MotionState.Free);
        }

        public override void MoveRight(Ship pShip)
        {
            //Debug.WriteLine("Ship Moving Right");
            pShip.x += Math.Abs(pShip.speedX); //Just Incase I set a negative speed
            this.Handle(pShip);
        }

        public override void MoveLeft(Ship pShip)
        {
            //Debug.WriteLine("Ship Cannot Move Left");
        }
    }
}
