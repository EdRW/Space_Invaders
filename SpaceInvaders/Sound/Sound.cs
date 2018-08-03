using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Sound_Link : DLink
    {

    }

    public class Sound : Sound_Link
    {
        public Sound.Name name;
        public IrrKlang.ISoundSource poSoundSource;
        public float volume;

        public enum Name
        {
            InvaderMarch1,
            InvaderMarch2,
            InvaderMarch3,
            InvaderMarch4,

            Invaderkilled,

            UFOHighPitch,
            UFOLowPitch,

            Shoot,

            Uninitialized
        }

        public Sound()
        {
            this.name = Name.Uninitialized;
            this.poSoundSource = null;
            this.volume = 1;
        }

        public Sound(Sound.Name name, float volume)
        {
            Set(name, volume);
        }

        public void Set(Sound.Name name, float volume)
        {
            String assetName = null;

            switch (name)
            {
                case Name.InvaderMarch1:
                    assetName = Constants.soundInvaderMarch1;
                    break;
                case Name.InvaderMarch2:
                    assetName = Constants.soundInvaderMarch2;
                    break;
                case Name.InvaderMarch3:
                    assetName = Constants.soundInvaderMarch3;
                    break;
                case Name.InvaderMarch4:
                    assetName = Constants.soundInvaderMarch4;
                    break;

                case Name.Invaderkilled:
                    assetName = Constants.soundInvaderkilled;
                    break;

                case Name.UFOHighPitch:
                    assetName = Constants.soundUFOHighPitch;
                    break;
                case Name.UFOLowPitch:
                    assetName = Constants.soundUFOLowPitch;
                    break;

                case Name.Shoot:
                    assetName = Constants.soundShoot;
                    break;

                case Name.Uninitialized:
                    assetName = Constants.soundUninitialized;
                    break;

                default:
                    Debug.Assert(false, "Invalid texture name");
                    break;
            }

            this.poSoundSource = SoundManager.GetEngine().AddSoundSourceFromFile(assetName);
            Debug.Assert(this.poSoundSource != null);

            this.poSoundSource.DefaultVolume = volume;

            this.name = name;
        }

        public IrrKlang.ISoundSource GetSound()
        {
            Debug.Assert(this.poSoundSource != null);
            return this.poSoundSource;
        }

        public override void Wash()
        {
            this.poSoundSource = null;
            this.name = Name.Uninitialized;
            this.volume = 1;
        }

        public override string ToString()
        {
            String soundName = (poSoundSource == null) ? "null" : poSoundSource.Name;
            return "[ " + name + " (" + this.GetHashCode() + ")" + " Sound: " + soundName + " ]";
        }
    }
}
