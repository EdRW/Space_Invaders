using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class TimedSoundEffects : Command
    {
        private SLink poFirstSound;
        private SLink pCurrentSound;

        public TimedSoundEffects()
        {
            // initialize references
            this.pCurrentSound = null;
            this.poFirstSound = null;
        }

        public void Attach(Sound.Name soundName)
        {
            // Create a new holder
            SoundNameHolder pSoundHolder = new SoundNameHolder(soundName);
            Debug.Assert(pSoundHolder != null);

            // Attach it to the TimedSoundEffect ( Push to front )
            SLink.AddToFront(ref this.poFirstSound, pSoundHolder);

            // Set the first one to this Sound
            this.pCurrentSound = pSoundHolder;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next Sound 
            SoundNameHolder pSoundHolder = (SoundNameHolder)this.pCurrentSound.pNext;

            // if at end of list, set to first
            if (pSoundHolder == null)
            {
                pSoundHolder = (SoundNameHolder)poFirstSound;
            }

            // squirrel away for next timer event
            this.pCurrentSound = pSoundHolder;

            // Play Sound
            SoundManager.PlaySound(pSoundHolder.soundName);

            // Add itself back to timer
            TimerManager.Add(this.name, this, deltaTime);
        }

        public void PrintReport()
        {
            Debug.WriteLine("[ TimedSoundEffects ]");
            Debug.Write("Sound List: ");
            SLink.PrintList(poFirstSound);
            Debug.WriteLine("\n");
        }
    }
}
