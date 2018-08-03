using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveUFOObserver : ColObserver
    {
        private InvaderCategory pInvader;
        IrrKlang.ISound pHardwareSound;

        public RemoveUFOObserver(IrrKlang.ISound pHardwareSound)
        {
            this.pInvader = null;
            this.pHardwareSound = pHardwareSound;
        }

        public RemoveUFOObserver(RemoveUFOObserver pRUObserver)
        {
            this.pInvader = pRUObserver.pInvader;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("RemoveInvaderObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);
                        
            if (pColSubject.pObjA.name == GameObject.Name.UFO) 
            {
                this.pInvader = (InvaderCategory)pColSubject.pObjA;
            }
            else if (pColSubject.pObjB.name == GameObject.Name.UFO)
            {
                this.pInvader = (InvaderCategory)pColSubject.pObjB;
            }
            else Debug.Assert(false, "Neither Object is not a UFO!");

            Debug.Assert(this.pInvader != null);
            if (pInvader.bMarkForDeath == false)
            {
                pInvader.bMarkForDeath = true;

                if (pColSubject.pObjA.name == GameObject.Name.Missle || pColSubject.pObjB.name == GameObject.Name.Missle)
                {
                    OneTimeAnimation pDeathAnimation = new OneTimeAnimation(Sprite.Name.InvaderDeath, this.pInvader.x, this.pInvader.y);
                    pDeathAnimation.Attach(Image.Name.InvaderDeath2);
                    pDeathAnimation.Attach(Image.Name.InvaderDeath1);
                    TimerManager.Add(TimeEvent.Name.InvaderDeath, pDeathAnimation, 0.05f);
                }

                pHardwareSound.Stop();
                // Delay - remove object later
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pInvader.Remove();
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
