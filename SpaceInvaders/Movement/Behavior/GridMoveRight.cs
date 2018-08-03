using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridMoveRight : Motion
    {
        private float speedX = 5.0f;

        public GridMoveRight(Motion.Name name)
            : base(name)
        {
        }

        public override void ApplyMotion(float x, float y, float width, float height)
        {
            if ((this.speedX + x + width) >= Constants.screenWidth)
            {
                this.motionAdvance = true;
            }

            this.deltaX = speedX;
            this.deltaY = 0;
        }
    }
}
