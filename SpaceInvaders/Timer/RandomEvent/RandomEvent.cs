using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class RandomEvent : Command
    {
        protected float timeRange;
        protected Random pRandom;

        public RandomEvent(float timeRange)
        {
            this.timeRange = timeRange;
            this.pRandom = new Random();
        }

        public override void Execute(float deltaTime)
        {
            DoAction();

            float multiplier = (float)this.pRandom.NextDouble();
            // Add itself back to timer
            TimerManager.Add(this.name, this, timeRange * multiplier);
        }

        protected abstract void DoAction();
    }
}
