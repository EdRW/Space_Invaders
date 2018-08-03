using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Texture_Link : DLink
    {

    }

    public class Texture : Texture_Link
    {
        private Azul.Texture poTexture;
        public Texture.Name name;

        public enum Name
        {
            Consolas36pt,
            Invader,
            NullObject,
            Uninitialized
        }

        public Texture()
            :this(Texture.Name.Uninitialized) // kick to overloaded ctor
        {          
        }

        public Texture(Texture.Name name)
        {
            Set(name);            
        }

        public void Set (Texture.Name name)
        {
            String assetName = null;

            switch (name)
            {
                case Name.Consolas36pt:
                    assetName = Constants.fontAsset;
                    break;
                case Name.Invader:
                    assetName = Constants.invaderAsset;
                    break;
                case Name.NullObject:
                    assetName = Constants.uninitializedAsset;
                    break;
                case Name.Uninitialized:
                    assetName = Constants.uninitializedAsset;
                    break;
                default:
                    Debug.Assert(false, "Invalid texture name");
                    break;
            }

            this.poTexture = new Azul.Texture(assetName);
            Debug.Assert(poTexture != null);

            this.name = name;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.poTexture;
        }

        public override void Wash()
        {
            this.Set(Name.Uninitialized);
        }

        public override string ToString()
        {
            return "[ " + name + " (" + this.GetHashCode() + ") ]";
        }
    }
}
