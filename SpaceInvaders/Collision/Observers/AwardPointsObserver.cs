using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AwardPointsObserver : ColObserver
    {
        private InvaderCategory pInvader;

        public AwardPointsObserver()
        {

        }

        public AwardPointsObserver(AwardPointsObserver pRIObserver)
        {
            this.pInvader = pRIObserver.pInvader;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            //Debug.WriteLine("AwardPointsObserver: {0} vs {1}", pColSubject.pObjA.name, pColSubject.pObjB.name);

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

            // Delay - Awards points later
            //AwardPointsObserver pObserver = new AwardPointsObserver(this);
            //DelayedObjectManager.Attach(pObserver);
            //Debug.WriteLine("{0} awarding {1} Points", this, this.pInvader.value);
            GameManager.AwardPoints(this.pInvader.value);
        }

        public override void Execute()
        {
            // Let the GameManager deal with this... 
        }

        public override string ToString()
        {
            return "[ AwardPointsObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
            this.pInvader = null;
        }
    }
}
