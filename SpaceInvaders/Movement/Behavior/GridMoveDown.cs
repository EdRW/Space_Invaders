using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class GridMoveDown : Motion
    {
        private float speedY = -20.0f;

        public GridMoveDown(Motion.Name name)
            : base(name)
        {
        }

        public override void ApplyMotion(float x, float y, float width, float height)
        {
            this.deltaX = 0.0f;
            this.deltaY = speedY;

            this.motionAdvance = true;
        }
    }
}
