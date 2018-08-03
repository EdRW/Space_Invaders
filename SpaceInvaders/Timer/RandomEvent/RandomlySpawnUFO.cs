using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RandomlySpawnUFO : RandomEvent
    {
        private InvaderFactory poUfoIF;
        private UFO poUfo;
        private Missile pMissile;
        private WallManager pWallMan;

        public RandomlySpawnUFO(float timeRange, Missile pMissile, WallManager pWallMan)
            : base(timeRange)
        {
            this.poUfo = null;
            this.poUfoIF = new InvaderFactory(SpriteBatch.Name.Sprites, SpriteBatch.Name.Boxes);
            this.pMissile = pMissile;
            this.pWallMan = pWallMan;
        }

        protected override void DoAction()
        {
            //Debug.WriteLine("Attempting to Spawen an Alien!!!");
            if (this.poUfo == null || this.poUfo.bMarkForDeath == true)
            {
                int UfoDirection = this.pRandom.Next(0, 2);
                //Debug.WriteLine("Random direction is: " + UfoDirection);
                this.poUfo = (UFO)this.poUfoIF.ActiveCreate(GameObject.Type.UFO, Constants.UFOPosXPos, Constants.UFOYPos);
                if (UfoDirection == 0)
                {
                    this.poUfo.speedX *= -1.0f;
                    this.poUfo.x = Constants.UFONegXPos;
                }
                IrrKlang.ISound pHardwareSound = SoundManager.PlaySound(Sound.Name.UFOLowPitch, true);

                ColPair pMissile_UFOColPair = ColPairManager.Add(ColPair.Name.Missile_UFO, this.pMissile, this.poUfo);
                Debug.Assert(pMissile_UFOColPair != null);

                ColPair pUFO_WallLeftColPair = ColPairManager.Add(ColPair.Name.UFO_WallLeft, this.poUfo, this.pWallMan.GetWallLeft());
                Debug.Assert(pUFO_WallLeftColPair != null);

                ColPair pUFO_WallRightColPair = ColPairManager.Add(ColPair.Name.UFO_WallRight, this.poUfo, this.pWallMan.GetWallRight());
                Debug.Assert(pUFO_WallRightColPair != null);

                // Set up missile UFO collsions
                pMissile_UFOColPair.Attach(new RemoveCollisionPairObserver(pMissile_UFOColPair));
                pMissile_UFOColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallLeftColPair));
                pMissile_UFOColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallRightColPair));
                pMissile_UFOColPair.Attach(new ShipMissileReadyObserver());
                pMissile_UFOColPair.Attach(new ShipRemoveMissileObserver());
                pMissile_UFOColPair.Attach(new RemoveUFOObserver(pHardwareSound));
                pMissile_UFOColPair.Attach(new AwardPointsObserver());

                // Set up UFO left wall collision
                pUFO_WallLeftColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallLeftColPair));
                pUFO_WallLeftColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallRightColPair));
                pUFO_WallLeftColPair.Attach(new RemoveCollisionPairObserver(pMissile_UFOColPair));
                pUFO_WallLeftColPair.Attach(new RemoveUFOObserver(pHardwareSound));
                
                // Set up UFO Right wall collision
                pUFO_WallRightColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallRightColPair));
                pUFO_WallRightColPair.Attach(new RemoveCollisionPairObserver(pUFO_WallLeftColPair));
                pUFO_WallRightColPair.Attach(new RemoveCollisionPairObserver(pMissile_UFOColPair));
                pUFO_WallRightColPair.Attach(new RemoveUFOObserver(pHardwareSound));
            } 
        }
    }
}
