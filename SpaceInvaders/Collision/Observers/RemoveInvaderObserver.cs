using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveInvaderObserver : ColObserver
    {
        private InvaderCategory pInvader;

        public RemoveInvaderObserver()
        {
            this.pInvader = null;
        }

        public RemoveInvaderObserver(RemoveInvaderObserver pRIObserver)
        {
            this.pInvader = pRIObserver.pInvader;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("RemoveInvaderObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
                        
            if (pColSubject.pObjA.name == GameObject.Name.SmallInvader ||
                pColSubject.pObjA.name == GameObject.Name.MediumInvader ||
                pColSubject.pObjA.name == GameObject.Name.LargeInvader ||
                pColSubject.pObjA.name == GameObject.Name.UFO) 
            {
                this.pInvader = (InvaderCategory)pColSubject.pObjA;
            }
            else if (pColSubject.pObjB.name == GameObject.Name.SmallInvader ||
                pColSubject.pObjB.name == GameObject.Name.MediumInvader ||
                pColSubject.pObjB.name == GameObject.Name.LargeInvader ||
                pColSubject.pObjB.name == GameObject.Name.UFO)
            {
                this.pInvader = (InvaderCategory)pColSubject.pObjB;
            }
            else Debug.Assert(false, "Neither Object is not an Invader!");

            Debug.Assert(this.pInvader != null);
            if (pInvader.bMarkForDeath == false)
            {
                pInvader.bMarkForDeath = true;

                OneTimeAnimation pDeathAnimation = new OneTimeAnimation(Sprite.Name.InvaderDeath, this.pInvader.x, this.pInvader.y);
                pDeathAnimation.Attach(Image.Name.InvaderDeath2);
                pDeathAnimation.Attach(Image.Name.InvaderDeath1);
                TimerManager.Add(TimeEvent.Name.InvaderDeath, pDeathAnimation, 0.05f);

                // Delay - remove object later
                RemoveInvaderObserver pObserver = new RemoveInvaderObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pInvader.Remove();
            SoundManager.PlaySound(Sound.Name.Invaderkilled);
        }

        public override string ToString()
        {
            return "[ RemoveInvaderObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
            this.pInvader = null;
        }
    }
}
