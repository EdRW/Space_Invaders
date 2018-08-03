using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Image_Link : DLink
    {

    }

    public class Image : Image_Link
    {
        public Image.Name name;
        private Texture pTexture;
        private Azul.Rect poImageRect;        

        public enum Name
        {
            ShieldBrick,

            Ship,
            Missile,

            ShipDeath1,
            ShipDeath2,

            SmallInvader1,
            SmallInvader2,
            MediumInvader1,
            MediumInvader2,
            LargeInvader1,
            LargeInvader2,
            UFO,

            InvaderDeath1,
            InvaderDeath2,
            UFODeath,

            BombPlain,

            BombZigZag1,
            BombZigZag2,
            BombZigZag3,
            BombZigZag4,

            BombDagger1,
            BombDagger2,
            BombDagger3,
            BombDagger4,

            BombRolling1,
            BombRolling2,
            BombRolling3,

            BombDeath,

            Uninitialized,
            NullObject
        }

        public Image()
        {
            this.name = Name.Uninitialized;
            this.pTexture = null;
            this.poImageRect = new Azul.Rect();
            Debug.Assert(poImageRect != null);
        }

        public Image(Image.Name name)
        {
            Set(name);
        }

        public Image(Image.Name name, Texture texture, float x, float y, float width, float height)
        {
            Debug.Assert(texture != null);
            
            this.pTexture = texture;
            this.name = name;

            this.poImageRect = new Azul.Rect(x, y, width, height);
            Debug.Assert(poImageRect != null);
        }

        public Image(Image.Name name, Texture texture, Azul.Rect imageRect)
        {
            Debug.Assert(texture != null);
            Debug.Assert(imageRect != null);

            this.pTexture = texture;
            this.name = name;

            this.poImageRect = new Azul.Rect(imageRect);
            Debug.Assert(imageRect != null);
        }

        public void Set(Name name, Texture texture, float x, float y, float width, float height)
        {
            this.name = name;

            Debug.Assert(texture != null);
            this.pTexture = texture;

            this.poImageRect.Set(x, y, width, height);
        }

        public void Set(Name name)
        {
            Azul.Rect imageRect = null;
            Texture texture = null;

            switch (name)
            {
                case Name.ShieldBrick:
                    imageRect = Constants.shieldBrickRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.Ship:
                    imageRect = Constants.shipRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.Missile:
                    imageRect = Constants.missileRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.ShipDeath1:
                    imageRect = Constants.shipDeathRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.ShipDeath2:
                    imageRect = Constants.shipDeathRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.SmallInvader1:
                    imageRect = Constants.smallInvaderRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.SmallInvader2:
                    imageRect = Constants.smallInvaderRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.MediumInvader1:
                    imageRect = Constants.mediumInvaderRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.MediumInvader2:
                    imageRect = Constants.mediumInvaderRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.LargeInvader1:
                    imageRect = Constants.largeInvaderRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.LargeInvader2:
                    imageRect = Constants.largeInvaderRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.UFO:
                    imageRect = Constants.UFORect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.InvaderDeath1:
                    imageRect = Constants.invaderDeathRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.InvaderDeath2:
                    imageRect = Constants.invaderDeathRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.UFODeath:
                    imageRect = Constants.UFODeathRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                    
                case Name.BombPlain:
                    imageRect = Constants.bombPlainRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombZigZag1:
                    imageRect = Constants.bombZigZagRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombZigZag2:
                    imageRect = Constants.bombZigZagRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombZigZag3:
                    imageRect = Constants.bombZigZagRect3;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombZigZag4:
                    imageRect = Constants.bombZigZagRect4;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.BombDagger1:
                    imageRect = Constants.bombDaggerRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombDagger2:
                    imageRect = Constants.bombDaggerRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombDagger3:
                    imageRect = Constants.bombDaggerRect3;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombDagger4:
                    imageRect = Constants.bombDaggerRect4;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.BombRolling1:
                    imageRect = Constants.bombRollingRect1;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombRolling2:
                    imageRect = Constants.bombRollingRect2;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;
                case Name.BombRolling3:
                    imageRect = Constants.bombRollingRect3;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.BombDeath:
                    imageRect = Constants.bombDeathRect;
                    texture = TextureManager.Find(Texture.Name.Invader);
                    break;

                case Name.Uninitialized:
                    imageRect = Constants.uninitializedRect;
                    texture = TextureManager.Find(Texture.Name.Uninitialized);
                    break;
                case Name.NullObject:
                    imageRect = Constants.uninitializedRect;
                    texture = TextureManager.Find(Texture.Name.NullObject);
                    break;
                default:
                    Debug.Assert(false, "Invalid texture name");
                    break;
            }

            Debug.Assert(texture != null);
            this.pTexture = texture;

            Debug.Assert(imageRect != null);
            this.poImageRect = new Azul.Rect(imageRect);
            Debug.Assert(poImageRect != null);

            this.name = name;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public Azul.Rect GetAzulRect()
        {
            return this.poImageRect;
        }

        public override void Wash()
        {
            this.pTexture = null;
            this.name = Name.Uninitialized;
            this.poImageRect.Clear();
        }

        public override string ToString()
        {
            String textureName = (pTexture == null) ? "null" : pTexture.name.ToString();
            return "[ " + name + " (" + this.GetHashCode() + ")" + " Texture: "+ textureName + " ]";
        }
    }
}
