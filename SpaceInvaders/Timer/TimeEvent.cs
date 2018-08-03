using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimeEvent : PriorityDLink
    {
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;

        public enum Name
        {
            TimedGridMover,
            GridMarchSoundEffects,
            SmInvaderMarchAnimation,
            MedInvaderMarchAnimation,
            LgInvaderMarchAnimation,
            BombAnimation,
            RandomBombSpawn,
            RandomlySpawnUFO,
            MotionComposite,
            InvaderDeath,
            Uninitialized
        }

        public TimeEvent()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        public void Set(TimeEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            Debug.Assert(pCommand != null);

            this.name = eventName;
            this.pCommand = pCommand;
            this.pCommand.name = eventName;
            this.deltaTime = deltaTimeToTrigger;

            // set the trigger time
            this.triggerTime = TimerManager.GetCurrTime() + deltaTimeToTrigger;
        }

        public void Process()
        {
            // make sure the command is valid
            Debug.Assert(this.pCommand != null);
            // fire off command
            this.pCommand.Execute(deltaTime);
        }

        protected override int CompareTo(PriorityDLink pPDLink)
        {
            float comparisonVal = this.triggerTime - ((TimeEvent)pPDLink).triggerTime;

            if (comparisonVal > 0.0f) return 1;
            else if (comparisonVal < 0.0f) return -1;
            else return 0;
        }

        public override string ToString()
        {
            return "[ " + name + " (" + this.GetHashCode() + ") TriggerTime: " + triggerTime + " DeltaTime: " + deltaTime + " ]";
        }

        public override void Wash()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }
    }
}
