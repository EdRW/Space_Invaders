using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MotionLockedState : ShipMotionState
    {
        public override void Handle(Ship pShip)
        {
        }

        public override void MoveRight(Ship pShip)
        {
            Debug.WriteLine("Ship controls are Locked");
        }

        public override void MoveLeft(Ship pShip)
        {
            Debug.WriteLine("Ship controls are Locked");
        }
    }
}