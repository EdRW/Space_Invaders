using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    // TODO this will be used for creating behaviors. TimedMotion will be used for basic object movement 
    public abstract class Motion
    {
        public Motion.Name name;

        public float deltaX;
        public float deltaY;
        public Boolean motionAdvance;

        public enum Name
        {
            UFOMoveRight,
            GridMoveRight,
            GridMoveLeft,
            GridMoveDown,
            Uninitialized
        }

        public Motion(Motion.Name name)
        {
            this.name = name;
            this.deltaX = 0;
            this.deltaX = 0;
            this.motionAdvance = false;
        }

        public abstract void ApplyMotion(float x, float y, float width, float height);
    }
}
