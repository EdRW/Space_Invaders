using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RandomlySpawnBomb : RandomEvent
    {

        private BombFactory pBombFactory;

        public RandomlySpawnBomb(float timeRange, BombFactory pBombFactory)
            : base(timeRange)
        {
            this.pBombFactory = pBombFactory;
            Debug.Assert(this.pBombFactory != null);
        }

        protected override void DoAction()
        {
            // Randomly select which of the 4 types of bomb to drop.
            BombFactory.Type type = (BombFactory.Type)this.pRandom.Next(0, 3);

            // Factory may or not create a bomb depending on if an invader is available to drop a bomb
            Bomb pBomb = this.pBombFactory.Create(type);
        }
    }
}
