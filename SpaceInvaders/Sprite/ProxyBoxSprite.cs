using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ProxyBoxSprite_Link : SpriteBase
    {

    }
    public class ProxyBoxSprite : ProxyBoxSprite_Link
    {
        public ProxyBoxSprite.Name name;
        public BoxSprite pBoxSprite;

        public float width;
        public float height;

        public enum Name
        {
            Proxy,
            Uninitialized
        }

        public ProxyBoxSprite()
            : base()
        {
            this.name = ProxyBoxSprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pBoxSprite = null;
        }
        // TODO Prolly delete this
        public ProxyBoxSprite(BoxSprite.Name name)
        {
            this.name = ProxyBoxSprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pBoxSprite = BoxSpriteManager.Find(name);
            Debug.Assert(this.pBoxSprite != null);
        }

        public void Set(BoxSprite.Name name)
        {
            this.name = ProxyBoxSprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;

            this.pBoxSprite = BoxSpriteManager.Find(name);
            Debug.Assert(this.pBoxSprite != null);
        }

        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.PrivPushToReal();
            this.pBoxSprite.Update();
        }

        public override void Render()
        {
            // move the values over to Real GameSprite
            this.PrivPushToReal();

            // update and draw realBoxSprite
            // Seems redundant - RealBoxSpritemight be stale
            this.pBoxSprite.Update();
            this.pBoxSprite.Render();
        }

        public void SetName(Name inName)
        {
            this.name = inName;
        }

        // TODO Probably delete
        public void SwapScreenRect(Azul.Rect pScreenRect)
        {
            this.pBoxSprite.SwapScreenRect(pScreenRect);
        }
        public void SetScreenRect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Name GetName()
        {
            return this.name;
        }

        public override void Wash()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.name = Name.Uninitialized;
            this.pBoxSprite = null;
        }

        private void PrivPushToReal()
        {
            // push the data from proxy to Real Sprite
            Debug.Assert(this.pBoxSprite != null);

            this.pBoxSprite.Set(this.pBoxSprite.name, this.x, this.y, this.width, this.height);
        }

        public override string ToString()
        {
            String spriteName = (pBoxSprite == null) ? "null" : pBoxSprite.name.ToString();
            return "[ " + name + " (" + this.GetHashCode() + ") x = " + x + " y = " + y + " Sprite: " + spriteName + " ]";
        }
    }
}
