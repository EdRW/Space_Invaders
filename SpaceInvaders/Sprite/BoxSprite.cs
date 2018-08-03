using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class BoxSprite_Link : SpriteBase
    {

    }

    public class BoxSprite : BoxSprite_Link
    {
        public Name name;
        private Azul.Color poLineColor;
        private Azul.SpriteBox poBoxSprite;

        private static Azul.Rect poScreenRect = new Azul.Rect();
        private static Azul.Color poColor = new Azul.Color(1, 1, 1);

        public enum Name
        {
            ShieldBrickBox,
            ShieldColumnBox,
            ShieldBox,
            ShieldZoneBox,

            ShipBox,
            MissileBox,

            SmallInvaderBox,
            MediumInvaderBox,
            LargeInvaderBox,
            InvaderColumnBox,
            InvaderGridBox,
            UFOBox,
            WallBox,
            GameSpaceBox,

            BombBox,

            Uninitialized,
            NullObject
        }

        public BoxSprite()
        {
            this.name = BoxSprite.Name.Uninitialized;

            Debug.Assert(BoxSprite.poScreenRect != null);
            BoxSprite.poScreenRect.Set(0, 0, 1, 1);
            Debug.Assert(BoxSprite.poColor != null);
            BoxSprite.poColor.Set(1, 1, 1);

            // Here is the actual new
            this.poBoxSprite = new Azul.SpriteBox(poScreenRect, poColor);
            Debug.Assert(this.poBoxSprite != null);

            // Here is the actual new
            this.poLineColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poLineColor != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;

            this.speedX = 0;
            this.speedY = 0;
        }

        public void Set(BoxSprite.Name name, Azul.Color pLineColor = null)
        {
            Debug.Assert(this.poBoxSprite != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(poScreenRect != null);

            this.name = name;

            if (pLineColor == null)
            {
                this.poLineColor.Set(1, 1, 1);
            }
            else
            {
                this.poLineColor.Set(pLineColor);
            }

            this.poBoxSprite.SwapColor(this.poLineColor);
            Debug.Assert(this.poBoxSprite != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }

        public void Set(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pLineColor = null)
        {
            Debug.Assert(this.poBoxSprite != null);
            Debug.Assert(this.poLineColor != null);

            Debug.Assert(poScreenRect != null);
            BoxSprite.poScreenRect.Set(x, y, width, height);

            this.name = name;

            if (pLineColor != null)
            {
                this.poLineColor.Set(pLineColor);
                this.poBoxSprite.SwapColor(this.poLineColor);
            }

            this.poBoxSprite.SwapScreenRect(poScreenRect);
            Debug.Assert(this.poBoxSprite != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }

        public void SwapColor(Azul.Color _pColor)
        {
            Debug.Assert(_pColor != null);
            this.poLineColor.Set(_pColor);
            this.poBoxSprite.SwapColor(_pColor);
        }

        public void SwapScreenRect(Azul.Rect pScreenRect)
        {
            Debug.Assert(pScreenRect != null);
            BoxSprite.poScreenRect.Set(pScreenRect);

            this.poBoxSprite.SwapScreenRect(poScreenRect);

            Debug.Assert(this.poBoxSprite != null);

            this.x = poBoxSprite.x;
            this.y = poBoxSprite.y;
            this.sx = poBoxSprite.sx;
            this.sy = poBoxSprite.sy;
            this.angle = poBoxSprite.angle;
        }

        public override void Update()
        {
            this.poBoxSprite.x = this.x;
            this.poBoxSprite.y = this.y;
            this.poBoxSprite.sx = this.sx;
            this.poBoxSprite.sy = this.sy;
            this.poBoxSprite.angle = this.angle;

            this.poBoxSprite.Update();
        }

        public override void Render()
        {
            this.poBoxSprite.Render();
        }

        public override void Wash()
        {
            this.name = BoxSprite.Name.Uninitialized;

            this.poLineColor.Set(1, 1, 1);

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;

            this.speedX = 1;
            this.speedY = 1;
        }

        public override string ToString()
        {
            return "[ " + name + " (" + this.GetHashCode() + ") x = " + x + " y = " + y + " ]";
        }
    }
}
