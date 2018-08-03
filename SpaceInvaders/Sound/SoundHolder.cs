using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundNameHolder : SLink
    {
        public Sound.Name soundName;

        public SoundNameHolder(Sound.Name soundName)
            : base()
        {
            this.soundName = soundName;
        }

        public override string ToString()
        {
            return soundName.ToString();
        }
    }
}

