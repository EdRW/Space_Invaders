using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOMoveRight : Motion
    {
        private float speedX = 15.0f;

        public UFOMoveRight(Motion.Name name)
            : base(name)
        {
        }

        public override void ApplyMotion(float x, float y, float width, float height)
        {
            if (x >= Constants.screenWidth)
            {
                this.deltaX = -1 * Constants.screenWidth;
                this.deltaY = 0;
            }
            else
            {
                this.deltaX = speedX;
                this.deltaY = 0;
            }
        }
    }
}
