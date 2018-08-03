using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridMoveLeft : Motion
    {
        private float speedX = -5.0f;

        public GridMoveLeft(Motion.Name name)
            : base(name)
        {
        }

        public override void ApplyMotion(float x, float y, float width, float height)
        {
            if ((this.speedX + x) <= 0.0f)
            {
                this.motionAdvance = true;
            }

            this.deltaX = speedX;
            this.deltaY = 0;
        }
    }
}
