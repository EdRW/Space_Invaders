﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MoveLeftObserver : InputObserver
    {

        protected override void derivedUpdate(InputSubject pInputSubject)
        {
            Ship pShip = ShipManager.GetShip();
            pShip.MoveLeft();
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
