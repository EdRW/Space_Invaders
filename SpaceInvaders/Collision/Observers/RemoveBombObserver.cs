using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBombObserver : ColObserver
    {
        private Bomb pBomb;

        public RemoveBombObserver()
        {

        }
        public RemoveBombObserver(RemoveBombObserver pRBbserver)
        {
            this.pBomb = pRBbserver.pBomb;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine(this.GetHashCode() + " RemoveBombObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
            // Bomb will always be pObjB
            GameObject pGameObjA = pColSubject.pObjA;
            GameObject pGameObjB = pColSubject.pObjB;
            Debug.Assert(pGameObjA != null);
            Debug.Assert(pGameObjB != null);

            if (pGameObjA.name == GameObject.Name.Bomb)
            {
                this.pBomb = (Bomb)pGameObjA;
            }
            else if (pGameObjB.name == GameObject.Name.Bomb)
            {
                this.pBomb = (Bomb)pGameObjB;
            }
            else { Debug.Assert(false, "Neither of the objects are Bombs"); }


            //Debug.WriteLine("Set state of Invader to true" + this.pBomb.pInvaderWhoDroppedMe);
            if (this.pBomb.bMarkForDeath == false)
            {
                this.pBomb.pInvaderWhoDroppedMe.canLaunchBomb = true;
                pBomb.bMarkForDeath = true;

                OneTimeAnimation pDeathAnimation = new OneTimeAnimation(Sprite.Name.BombDeath, this.pBomb.x, this.pBomb.y);
                pDeathAnimation.Attach(Image.Name.BombDeath);
                TimerManager.Add(TimeEvent.Name.InvaderDeath, pDeathAnimation, 0.1f);

                // Delay - remove object later
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
             this.pBomb.Remove();
        }

        public override string ToString()
        {
            return "[ RemoveBombObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }
    }
}
