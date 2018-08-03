using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class MotionHolder : SLink
    {
        public Motion pMotion;

        public MotionHolder(Motion motion)
            : base()
        {
            this.pMotion = motion;
        }

        public override string ToString()
        {
            return this.pMotion.name.ToString();
        }
    }
}
