using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Sprite_Link : SpriteBase
    {

    }

    public class Sprite : Sprite_Link
    {
        public Name name;
        private Azul.Sprite poSprite;
        private Image pImage;
        private Azul.Color poColor;

        //private static Azul.Rect poScreenRect = new Azul.Rect();
        private Azul.Rect poScreenRect;


        public enum Name
        {
            ShieldBrick,

            Ship,
            Missile,

            SmallInvader,
            MediumInvader,
            LargeInvader,
            UFO,

            InvaderDeath,
            BombDeath,

            BombPlain,
            BombZigZag,
            BombDagger,
            BombRolling,

            Uninitialized,
            NullObject
        }

        public Sprite()
        {
            this.name = Sprite.Name.Uninitialized;

            this.pImage = ImageManager.Find(Image.Name.Uninitialized);
            Debug.Assert(this.pImage != null);

            this.poScreenRect = new Azul.Rect();
            Debug.Assert(this.poScreenRect != null);
            this.poScreenRect.Clear();

            this.poColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poColor != null);

            this.poSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), poScreenRect, poColor);
            Debug.Assert(this.poSprite != null);

            this.x = poSprite.x;
            this.y = poSprite.y;
            this.sx = poSprite.sx;
            this.sy = poSprite.sy;
            this.angle = poSprite.angle;

            this.speedX = 0;
            this.speedY = 0;
        }


        public void Set(Sprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            Debug.Assert(pImage != null);

            this.pImage = pImage;
            this.name = name;

            Debug.Assert(this.poScreenRect != null);
            this.poScreenRect.Set(x, y, width, height);

            if (pColor == null)
            {
                Debug.Assert(this.poColor != null);
                this.poColor.Set(1, 1, 1);
            }
            else
            {
                this.poColor.Set(pColor);
            }


            this.poSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), poScreenRect, poColor);
            Debug.Assert(this.poSprite != null);

            this.x = poSprite.x;
            this.y = poSprite.y;
            this.sx = poSprite.sx;
            this.sy = poSprite.sy;
            this.angle = poSprite.angle;
            this.speedX = 0;
            this.speedY = 0;
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poSprite != null);
            Debug.Assert(pNewImage != null);
            this.pImage = pNewImage;

            this.poSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        public override void Update()
        {
            this.poSprite.x = this.x;
            this.poSprite.y = this.y;
            this.poSprite.sx = this.sx;
            this.poSprite.sy = this.sy;
            this.poSprite.angle = this.angle;

            this.poSprite.Update();
        }

        public override void Render()
        {
            this.poSprite.Render();
        }

        public override void Wash()
        {
            this.pImage = null;
            this.name = Sprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 0.0f;
            this.sy = 0.0f;
            this.angle = 0.0f;

            this.speedX = 0;
            this.speedY = 0;
        }

        public override string ToString()
        {
            String imageName = (pImage == null) ? "null" : pImage.name.ToString();
            return "[ " + name + " (" + this.GetHashCode() + ") x = "+ x + " y = " + y + " Image: "+ imageName + " ]";
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.poScreenRect != null);
            return this.poScreenRect;
        }
    }
}
