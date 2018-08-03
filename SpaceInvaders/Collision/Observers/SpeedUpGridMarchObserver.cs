using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpeedUpGridMarchObserver : ColObserver
    {
        public float speedUpMultiplier;

        public SpeedUpGridMarchObserver(float speedUpMultiplier)
        {
            this.speedUpMultiplier = speedUpMultiplier;
        }

        protected override void derivedUpdate(ColSubject pColSubject)
        {
            SpeedUpGridMarchObserver pObserver = new SpeedUpGridMarchObserver(this.speedUpMultiplier);
            DelayedObjectManager.Attach(pObserver);
        }

        public override void Execute()
        {
            TimeEvent pGridTimedMover = TimerManager.Find(TimeEvent.Name.TimedGridMover);
            TimeEvent pSmMarchAnim = TimerManager.Find(TimeEvent.Name.SmInvaderMarchAnimation);
            TimeEvent pMedMarchAnim = TimerManager.Find(TimeEvent.Name.MedInvaderMarchAnimation);
            TimeEvent pLgMarchAnim = TimerManager.Find(TimeEvent.Name.LgInvaderMarchAnimation);
            TimeEvent pGridMarchSnd = TimerManager.Find(TimeEvent.Name.GridMarchSoundEffects);

            pGridTimedMover.deltaTime *= speedUpMultiplier;
            pSmMarchAnim.deltaTime *= speedUpMultiplier;
            pMedMarchAnim.deltaTime *= speedUpMultiplier;
            pLgMarchAnim.deltaTime *= speedUpMultiplier;
            pGridMarchSnd.deltaTime *= speedUpMultiplier;
        }

        public override string ToString()
        {
            return "[ SpeedUpGridMarchObserver ({0}) ]" + this.GetHashCode();
        }

        public override void Wash()
        {
        }
    }
}

